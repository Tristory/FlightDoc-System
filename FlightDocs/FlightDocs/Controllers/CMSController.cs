using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace FlightDocs.Controllers
{
    //[Authorize(Policy = "Staff and Admin")]
    public class CMSController : Controller
    {
        private GroupDM groupDM;

        public CMSController(ApplicationDbContext context)
        {
            groupDM = new GroupDM(context);
        }

        [HttpPost]
        [Route("Create Group")]
        public IActionResult CreateGroup(GroupInfo groupInfo)
        {
            Group group = new Group();
            group.Name = groupInfo.Name;
            group.CreatorId = groupInfo.CreatorId;

            groupDM.AddGroup(group);

            return Ok();
        }

        //[Bind("Id,Name,Created_Date,Note,CreatorId")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
