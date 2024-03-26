using Microsoft.AspNetCore.Mvc; // Include this for EF Core functionality
using System.Linq;
using TicketSales.Models;
using System.Data.Entity;
namespace TicketSales.Controllers
{
    public class EventsController : Controller
    {
        private readonly TicketReservationContext db = new TicketReservationContext(); // Assuming your DbContext

        public async Task<ActionResult> Details(int eventId)
        {
            // Fetch the event including its Venue using EF6
            var evt = await db.Events.Include("Venue")
                                     .Include("Tickets")
                                     .SingleOrDefaultAsync(e => e.EventId == eventId);

            if (evt == null)
            {
                return NotFound();
            }

            return View(evt);
        }
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return View(new List<Event>()); // Return an empty list or however you wish to handle empty queries
            }

            var searchResults = await db.Events
                .Where(e => e.Title.ToLower().Contains(searchQuery.ToLower()))
                .ToListAsync();

            return View(searchResults);
        }

        public async Task<ActionResult> Bookmarks()
        {
         
            return View();
        }        
    }
}
