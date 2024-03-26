using Microsoft.AspNetCore.Mvc;
using TicketSales.Models;
using System.Linq;
using System.Data.Entity;

namespace TicketSales.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly TicketReservationContext db = new TicketReservationContext();
        public IActionResult Index()
        {
            var reservations = db.Reservations
                                .Include("Event")
                                .Include("Ticket")
                                .OrderBy(r => r.ReservationDate)
                                .ToList();

            return View(reservations);
        }

        public IActionResult Cancel(int reservationId)
        {
            var reservation = db.Reservations.FirstOrDefault(e => e.ReservationId == reservationId);

            if (reservation != null)
            {
                // Remove the entity from DbSet
                db.Reservations.Remove(reservation);      
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
