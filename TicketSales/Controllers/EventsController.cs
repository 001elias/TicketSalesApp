using Microsoft.AspNetCore.Mvc;

namespace TicketSales.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int eventId)
        {
            return View();
        }
    }
}
