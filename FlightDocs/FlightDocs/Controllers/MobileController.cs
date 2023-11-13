using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs.Controllers
{
    [Authorize(Policy = "Fly Attendant and Admin")]
    public class MobileController : Controller
    {
        private FlightDM flightDM;
        private DocumentDM documentDM;

        public MobileController(ApplicationDbContext context) 
        {
            flightDM = new FlightDM(context);
            documentDM = new DocumentDM(context);
        }

        [HttpGet]
        [Route("Today flight")]
        public IActionResult TodayFlights() 
        {
            ViewData["TodayFLights"] = flightDM.GetTodayFlights();

            return View();
        }

        [HttpGet]
        [Route("flight document")]
        public IActionResult FlightDocuments(int flightId)
        {
            var curUserId = int.Parse(User.FindFirst("Id")?.Value);

            ViewData["Flight"] = flightDM.GetFlight(flightId);
            ViewData["DocFromSystem"] = documentDM.GetOtherFlightDocuments(flightId, curUserId);
            ViewData["DocFromUser"] = documentDM.GetUserFlightDocuments(flightId, curUserId);

            return View();
        }

        [HttpGet]
        //[Route("Update document")]
        public IActionResult FlightDocument(int docId)
        {
            ViewData["Document"] = documentDM.GetDocument(docId);

            return View();
        }

        [HttpPatch]
        public IActionResult ConfirmUpdate(Document document)
        {
            ViewData["Document"] = document;

            return View();
        }

        [HttpPatch]
        [Route("Update document")]
        public async Task<IActionResult> UpdateFlightDoc(Document document, IFormFile file, IFormFile pic)
        {
            await documentDM.UpdateDocumentAsync(document, file, pic);

            return Ok();
        }

        [HttpGet]
        public IActionResult ConfirmedDocument(int docId)
        {
            ViewData["Document"] = documentDM.GetDocument(docId);

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
