using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FlightDocs.Controllers
{
    [Authorize(Policy = "Staff and Admin")]
    public class CMSController : Controller
    {
        private RoleDM roleDM;
        private GroupDM groupDM;
        private FlightDM flightDM;
        private AccountDM accountDM;
        private DocumentDM documentDM;
        private PermissionDM permissionDM;
        private DocumentTypeDM documentTypeDM;

        public CMSController(ApplicationDbContext context)
        {
            roleDM = new RoleDM(context);
            groupDM = new GroupDM(context);
            flightDM = new FlightDM(context);
            accountDM = new AccountDM(context);
            documentDM = new DocumentDM(context);
            permissionDM = new PermissionDM(context);
            documentTypeDM = new DocumentTypeDM(context);
        }

        //Mostly home page
        [HttpGet]
        [Route("Home Page")]
        public IActionResult HomePage()
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            if (curUserId == 0)
                return BadRequest();

            ViewData["RecentActivity"] = documentDM.GetUserRecentActivity(curUserId);
            ViewData["CurrentFlights"] = flightDM.GetCurrentFlights();

            return View();
        }

        /*[HttpGet]
        //[Route("Get Document")]
        public IActionResult GetDocument(int docId)
        {
            ViewData["Document"] = documentDM.GetDocument(docId);

            return View();
        }*/

        [HttpGet]
        [Route("Get All Flight")]
        public IActionResult GetAllFlight()
        {
            ViewData["AllFlights"] = flightDM.GetFlights();

            return View();
        }

        [HttpGet]
        public IActionResult GetFlightDoc(int flightId)
        {
            ViewData["FlightDocs"] = documentDM.GetFlightDocuments(flightId);

            return View();
        }

        //Add document
        [HttpGet]
        public IActionResult AddDocument()
        {
            ViewData["DocTypes"] = documentTypeDM.GetDocumentTypes();
            ViewData["Groups"] = groupDM.GetGroups();

            return View();
        }

        [HttpPost]
        [Route("Add Document")]
        public async Task<IActionResult> AddDocumentAsync(int flightId, DocumentInfo documentInfo, IFormFile file, List<int> groupIds)
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            var document = new Document();
            document.Name = documentInfo.Name;
            document.Note = documentInfo.Note;
            document.DocumentTypeId = documentInfo.DocumentTypeId;
            document.FlightId = flightId;
            document.CreatorId = curUserId;

            var docId = await documentDM.AddDocumentAsync(document, file);

            foreach (var groupId in groupIds) 
            {
                AddDocumentPermission(docId, groupId);
            }

            return Ok();
        }

        [HttpPost]
        public string AddDocumentPermission(int docId, int groupId)
        {
            var permissionDG = new PermissionDG();
            permissionDG.DocumentId = docId;
            permissionDG.GroupId = groupId;

            return permissionDM.AddPermissionDG(permissionDG);
        }

        //Display document
        [HttpGet]
        [Route("Document detail")]
        public IActionResult GetDocument(int docId)
        {
            ViewData["Document"] = documentDM.GetDocument(docId);
            ViewData["Permissions"] = permissionDM.GetPermissionDTGs(docId);
            ViewData["Oldversions"] = documentDM.GetOldVersions(docId);

            return View();
        }

        [HttpGet]
        [Route("Get all user document")]
        public IActionResult GetAllDocument()
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            ViewData["Documents"] = documentDM.GetUserDocuments(curUserId);

            return View();
        }

        //Document type
        [HttpGet]
        public IActionResult GetDocumentTypes()
        {
            ViewData["DocTypes"] = documentTypeDM.GetDocumentTypes();

            return View();
        }

        [HttpGet]
        public IActionResult GetDocumentType(int dtId)
        {
            ViewData["DocType"] = documentTypeDM.GetDocumentType(dtId);
            ViewData["Roles"] = roleDM.GetRoles();
            ViewData["Groups"] = groupDM.GetGroups();

            return View();
        }

        [HttpGet]
        public IActionResult AddDocumentTypePage()
        {
            ViewData["Roles"] = roleDM.GetRoles();
            ViewData["Groups"] = groupDM.GetGroups();

            return View();
        }

        [HttpPost]
        [Route("Add document")]
        public IActionResult AddDocumentType(DocumentTypeInfo documentTypeInfo, List<PermissionDTGInfo> permissionDTGInfos)
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            var documentType = new DocumentType();
            documentType.Name = documentTypeInfo.Name;
            documentType.CreatorId = curUserId;
            documentType.Customer_roleId = documentTypeInfo.Customer_roleId;

            var dtId = documentTypeDM.AddDocumentType(documentType);

            foreach (var permissionDTGInfo in permissionDTGInfos)
            {
                if (permissionDTGInfo.Access_Level != 0) 
                {
                    var permissionDTG = new PermissionDTG();
                    permissionDTG.GroupId = permissionDTGInfo.GroupId;
                    permissionDTG.DocumentTypeId = dtId;
                    permissionDTG.Access_Level = permissionDTGInfo.Access_Level;

                    permissionDM.AddPermissionDTG(permissionDTG);
                }
                
            }

            return Ok();
        }

        [HttpGet]
        public int GetAccessLevel(int dtId, int groupId)
        {
            var permissionDTG = permissionDM.GetPermissionDTG(dtId, groupId);

            if (permissionDTG != null)
                return permissionDTG.Access_Level;

            return 0; 
        }

        [HttpPost]
        public IActionResult UpdateAccessLevel(int chosenAL, int currentAL, int dtId, int groupId)
        {
            if (chosenAL == currentAL)
                return Content("Doesn't seem different to me");

            var permissionDTG = permissionDM.GetPermissionDTG(dtId, groupId);

            if (permissionDTG != null)
            {
                permissionDTG.Access_Level = chosenAL;

                permissionDM.UpdatePermissionDTG(permissionDTG);
            }
            else
            {
                permissionDTG = new PermissionDTG();
                permissionDTG.GroupId = groupId;
                permissionDTG.DocumentTypeId = dtId;
                permissionDTG.Access_Level = chosenAL;

                permissionDM.AddPermissionDTG(permissionDTG);
            }

            return Ok();
        }


        //Group operation
        [HttpGet]
        [Route("Show group list")]
        public IActionResult GetGroups()
        {
            ViewData["Groups"] = groupDM.GetGroups();

            return View();
        }

        [HttpPost]
        [Route("Create Group")]
        public IActionResult CreateGroup(GroupInfo groupInfo)
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            var group = new Models.Group();
            group.Name = groupInfo.Name;
            group.Note = groupInfo.Note;
            group.CreatorId = curUserId;

            groupDM.AddGroup(group);

            return Ok();
        }

        [HttpGet]
        [Route("Get group detail")]
        public IActionResult GetGroup(int groupId)
        {
            ViewData["Group"] = groupDM.GetGroup(groupId);
            ViewData["GroupMember"] = accountDM.GetGroupMembers(groupId);

            return View();
        }

        [HttpGet]
        [Route("Get account for add")]
        public IActionResult GetAccountNotInGroup(int groupId)
        {
            ViewData["NotGroupMember"] = accountDM.GetNotGroupMembers(groupId);

            return View();
        }

        [HttpPatch]
        [Route("Add account to group")]
        public IActionResult AddAccountToGroup(int acId, int groupId)
        {
            var account = accountDM.GetAccount(acId);

            account.GroupId = groupId;

            accountDM.UpdateAccount(account);

            return Ok();
        }

        [HttpPatch]
        [Route("Block account")]
        public IActionResult LockAndUnlockAccount(int acId)
        {
            var account = accountDM.GetAccount(acId);

            accountDM.UpdateAccountStatus(account);

            return Ok();
        }

        //Account
        [HttpGet]
        [Route("Display accounts")]
        public IActionResult AccountSetting()
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            ViewData["Account"] = accountDM.GetUserAccount(curUserId);
            ViewData["OtherUser"] = accountDM.GetNotCurrentUser(curUserId);
            ViewData["AllAccounts"] = accountDM.GetAccounts();

            return View();
        }

        [HttpPatch]
        [Route("Change account owner")]
        public string ChangeAccountOwner(int acId, int newUserId, string password)
        {
            return accountDM.UpdateAccountUser(acId, newUserId, password);
        }

        [HttpGet]
        [Route("Display account detail")]
        public IActionResult AccountDetail()
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            ViewData["Account"] = accountDM.GetAccountWithUser(curUserId);

            return View();
        }

        //[Bind("Id,Name,Created_Date,Note,CreatorId")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
