using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using TicketSales.Models;

namespace TicketSales.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TicketReservationContext db = new TicketReservationContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index(string sortOrder = "dateAsc", string eventFilter = "")
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = eventFilter;

            // Using .Include() for eager loading of the Venue navigation property
            var events = db.Events.Include("Venue")
                                  .Where(evt => evt.EventDateTime >= DateTime.Today);
                                 

            switch (sortOrder)
            {
                case "dateAsc":
                    events = events.OrderBy(e => e.EventDateTime);
                    break;
                case "dateDesc":
                    events = events.OrderByDescending(e => e.EventDateTime);
                    break;
                case "eventType":
                    events = events.OrderBy(e => e.EventType);
                    break;
                case "venueCity":
                    events = events.OrderBy(e => e.Venue.City);
                    break;
                default:
                    break;
            }

            switch(eventFilter)
            {
                case "concert":
                    events = events.Where(e => e.EventType =="Concert");
                    break;
                case "other":
                    events = events.Where(e => e.EventType == "Other");
                    break;
                case "theater":
                    events = events.Where(e => e.EventType == "Theatre");
                    break;
            }
            var list = events.Take(30).ToList();
            return View(list);
        }

     
        public IActionResult Privacy()
        {
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
