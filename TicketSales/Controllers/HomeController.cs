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

        public ActionResult Index()
        {
            // Using .Include() for eager loading of the Venue navigation property
            var events = db.Events.Include("Venue") // Use string include for EF6
                                  .OrderBy(e => e.EventDateTime)
                                  .Take(30)
                                  .ToList(); // Synchronous query, but you can use .ToListAsync() with EF6 if async is needed
            return View(events);
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
