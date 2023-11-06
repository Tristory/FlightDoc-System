using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Controllers
{
    public class CMSController : Controller
    {
        private readonly ApplicationDbContext _context;
        private FlightDM flightDM;

        public CMSController(ApplicationDbContext context)
        {
            _context = context;
            flightDM = new FlightDM(_context);
        }

        [Authorize(Policy = "For Admin")]
        [HttpPost]
        [Route("AddRole")]
        public string AddRoles(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Add success!";
        }

        [HttpPost]
        [Route("AddFlight")]
        public void AddFlight(Flight flight)
        {
            flightDM.AddFlight(flight);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
