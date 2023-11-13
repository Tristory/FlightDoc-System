using FlightDocs.Data;
using FlightDocs.File_Handler.Code;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlightDocs.Controllers
{
    //[Authorize(Policy = "Someone and Admin")]
    public class DeveloperController : Controller
    {
        //private HttpContext _httpContext;
        //private FileHandler _fileHandler;
        private readonly ApplicationDbContext _context;
        private RoleDM roleDM;
        private FlightDM flightDM;        
        private DocumentDM documentDM;
        private PermissionDM permissionDM;
        private DocumentTypeDM documentTypeDM;        

        public DeveloperController(ApplicationDbContext context)
        {
            //_httpContext = httpContext;
            _context = context;
            roleDM = new RoleDM(_context);
            flightDM = new FlightDM(_context);            
            documentDM =  new DocumentDM(_context);
            permissionDM = new PermissionDM(_context);
            documentTypeDM = new DocumentTypeDM(_context);            
        }

        //Role CRUD operation
        //[Authorize(Policy = "For Admin")]
        /*[HttpPost]
        [Route("AddRole")]
        public string AddRoles(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Add success!";
        }

        [HttpPost]
        [Route("UpdateRole")]
        public string UpdateRole(Role role)
        {
            return roleDM.UpdateRole(role);
        }*/

        //Flight CRUD operation
        [HttpPost]
        [Route("Add Flight")]
        public string AddFlight(Flight flight)
        {
            flightDM.AddFlight(flight);

            return "Add success!";
        }

        [HttpGet]
        [Route("Get Today")]
        public DateTime GetToday()
        {
            return DateTime.Now;
        }

        //Document type CRUD operation
        [HttpPost]
        [Route("Add Document type")]
        public int AddDocumentType(DocumentTypeInfo documentTypeInfo) 
        {
            var documentType = new DocumentType();
            documentType.Name = documentTypeInfo.Name;
            documentType.Customer_roleId = documentTypeInfo.Customer_roleId;
            documentType.CreatorId = documentTypeInfo.CreatorId;

            return documentTypeDM.AddDocumentType(documentType);
        }

        //Get Token
        /*[HttpGet]
        [Route("Get Token")]
        public async Task<ActionResult> GetToken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        [HttpGet]
        [Route("Get Token 2")]
        public string GetToken2()
        {
            var token  = HttpContext.GetTokenAsync("access_token");

            return token.Result;
        }

        [HttpGet]
        [Route("Get Name")]
        public string GetName(string claimName)
        {
            var name = User.FindFirst(claimName)?.Value;

            return name;
        }*/

        //File handler
        /*[HttpPost]
        [Route("Upload file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected!");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Handler", "Main Folder", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(path);
        }
                
        [HttpPost]
        [Route("Upload file 2")]
        public async Task<IActionResult> UploadFile2(IFormFile file)
        {
            string result = await FileHandler.UploadFile(file, "Hey don't use me.pdf");

            if(result == null)
            {
                return Content("No File selected!");
            }

            return Ok("Add success!");

        }

        [HttpGet]
        [Route("Show file")]
        public async Task<IActionResult> ShowFile(string filename)
        {
            if (filename == null)
                return Content("File name is not present!");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Handler", "Main Folder", filename);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/pdf");

            //var stream = new FileStream(path, FileMode.Open);

            //return new FileStreamResult(stream, "application/pdf");
        }*/

        [HttpPost]
        [Route("Upload Pic")]
        public async Task<string> UploadPic(string docName, float version, IFormFile pic)
        {
            return await documentDM.PicNameHandlerAsync(docName, version, pic);
        }


            //Document input
            [HttpPost]
        [Route("Input document")]
        public async Task<int> AddDocumentAsync(DocumentInfo documentInfo, IFormFile file)
        {
            var document = new Document();
            document.Name = documentInfo.Name;
            document.Note = documentInfo.Note;
            document.DocumentTypeId = documentInfo.DocumentTypeId;
            document.FlightId = documentInfo.FlightId;
            document.CreatorId = documentInfo.CreatorId;

            return await documentDM.AddDocumentAsync(document, file);
        }

        [HttpPost]
        [Route("Update document")]
        public async Task<string> UpdateDocumentAsync(int docId, string note, IFormFile file)
        {
            var document = documentDM.GetDocument(docId);
            document.Note = note;

            if (file == null || file.Length == 0)
                return documentDM.UpdateDocument(document);

            return await documentDM.UpdateDocumentAsync(document, file);
        }

        //Permission CRUD operation
        [HttpPost]
        [Route("Add permission DG")]
        public async Task<string> AddPermissionDG(PermissionDGInfo permissionDGInfo)
        {
            var permissionDG = new PermissionDG();
            permissionDG.GroupId = permissionDGInfo.GroupId;
            permissionDG.DocumentId = permissionDGInfo.DocumentId;

            return permissionDM.AddPermissionDG(permissionDG);
        }

        [HttpPost]
        [Route("Add permission DTG")]
        public async Task<string> AddPermissionDTG(PermissionDTGInfo permissionDTGInfo)
        {
            var permissionDTG =  new PermissionDTG();
            permissionDTG.GroupId = permissionDTGInfo.GroupId;
            permissionDTG.DocumentTypeId = permissionDTGInfo.DocumentTypeId;
            permissionDTG.Access_Level = permissionDTGInfo.Access_Level;

            return permissionDM.AddPermissionDTG(permissionDTG);
        }

        /*[HttpPost]
        [Route("Check file name")]
        public async Task<string> ProcessTheName(string name, float version, IFormFile file) 
        {
            return await documentDM.FileNameHandlerAsync(name, version, file);
        }*/

        //System extra
        public IActionResult Index()
        {
            return View();
        }
    }
}
