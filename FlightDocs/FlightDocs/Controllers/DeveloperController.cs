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
        private readonly ApplicationDbContext _context;
        //private FileHandler _fileHandler;
        private FlightDM flightDM;
        private RoleDM roleDM;

        public DeveloperController(ApplicationDbContext context)
        {
            //_httpContext = httpContext;
            _context = context;
            flightDM = new FlightDM(_context);
            roleDM = new RoleDM(_context);
        }

        //[Authorize(Policy = "For Admin")]
        [HttpPost]
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
        }

        [HttpPost]
        [Route("AddFlight")]
        public void AddFlight(Flight flight)
        {
            flightDM.AddFlight(flight);            
        }

        [HttpGet]
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

        //For in program usage
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
        }

        [HttpPost]
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
            string result = await FileHandler.UploadFile(file);

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
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
