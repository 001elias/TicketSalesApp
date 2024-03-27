using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TicketSales.Models;
using Microsoft.AspNetCore.Http;


namespace TicketSales.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly TicketReservationContext db = new TicketReservationContext();

       
        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }
        public ActionResult Index()
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int loggedUserId);
            var boomarks = db.Bookmarks.Where(b => b.UserID == loggedUserId);

            return View(boomarks.ToList());
        }

        // POST: Bookmarks/Add
        [HttpPost]
        public ActionResult Add(int eventId)
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int loggedUserId);
            var userId = loggedUserId;
            if (db.Bookmarks.Any(b => b.EventId == eventId && b.UserID == userId))
            {
                // The event is already bookmarked by this user
                return StatusCode(StatusCodes.Status409Conflict);
            }

            var bookmark = new Bookmark { EventId = eventId, UserID = userId };
            db.Bookmarks.Add(bookmark);
            db.SaveChanges();

            // Redirect back to the event list or wherever appropriate
            return RedirectToAction("Index", "Home");
        }

        // POST: Bookmarks/Remove
        [HttpPost]
        public ActionResult Remove(int bookmarkId)
        {
            var bookmark = db.Bookmarks                          
                             .SingleOrDefault(b => b.BookmarkId == bookmarkId);

            if (bookmark != null)
            {
                db.Bookmarks.Remove(bookmark);
                db.SaveChanges();
            }

            // Redirect back to the bookmarks view or wherever appropriate
            return RedirectToAction("Index");
        }
    }

}
