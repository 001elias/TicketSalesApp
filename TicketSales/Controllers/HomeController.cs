using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            ////db.Events.Add(new Event {  Location = "Montreal", Description = "Dude", Title = "Test", EventType="Concert", EventDateTime = DateTime.Now});
            ////var result = db.SaveChanges();
            var evt = db.Events.First();


            return View(evt);
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
