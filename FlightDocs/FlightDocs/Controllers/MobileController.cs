using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Controllers
{
    [Authorize(Policy = "Fly Attendant and Admin")]
    public class MobileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
