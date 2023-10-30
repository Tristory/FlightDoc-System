using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Controllers
{
    public class CMSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CMSController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles() 
        {
            return _context.Roles.ToList();
        }

        [HttpPost]
        [Route("AddRole")]
        public string AddRoles(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateRoles(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return "Update success!";
        }

        public string DeleteRoles(Role role) 
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return "Delete success!";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
