using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TicketSales.Models;
using TicketSales.Extensions;
using System.Security.Claims;
using TicketSales.Services;

namespace TicketSales.Controllers
{
    public class CartController : Controller
    {
        private readonly TicketReservationContext db = new TicketReservationContext();

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }
     
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Retrieve the message from TempData
            string message = TempData["Message"] as string;

            // Pass the message to the view
            ViewBag.Message = message;
            return View(cart);
        }

        public IActionResult AddToCart(CartItem cartItem)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(item => item.TicketID == cartItem.TicketID);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.Add(cartItem);
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartCount = cart.Sum(item => item.Quantity);
            return Ok(cartCount);
        }

        public IActionResult RemoveFromCart(int ticketId)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(item => item.TicketID == ticketId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Book()
        {

            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int loggedUserId);
           
            foreach (CartItem item in cart)
            {            
                db.Reservations.Add(new Reservation
                {
                    EventId = item.EventID,
                    TicketId = item.TicketID,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Price,
                    ReservationDate = DateTime.Now,
                    UserId = loggedUserId
                });
            }
            db.SaveChanges();
            //
            SendEmail(cart);

             TempData["Message"] = "Reservations made successfully.";
            // clean the session
            HttpContext.Session.Set("Cart", new List<CartItem>());
            return RedirectToAction("Index");
        }

        private void SendEmail(List<CartItem> cart)
        {
            string ticketSummary = string.Empty;
            foreach (var item in cart)
            {
                ticketSummary += $"{item.EventDescription} Date: {item.EventDate} " +
                                 $"Venue: {item.Venue} Quantity: {item.Quantity} " +
                                 $"Price: {item.Price} $\n\n";
            }
            string loggedUserMail = User.FindFirst(ClaimTypes.Email)?.Value;
            string mailBody = $"Dear Customer, \n\n Thank you for booking your tickets with us!\n\n" +
                             "We're excited to confirm your order and provide you with the following summary\n\n:" + ticketSummary +
                             "Please review the details above and ensure everything is correct. If you have any questions or need further assistance," +
                             "feel free to reach out to our customer support team. \n\nWe hope you have a fantastic experience!\n\n" +
                             "Best regards, \n\nTicket Rush team";


            EmailService.SendEmail("ticketrushinfo@gmail.com", loggedUserMail, "Your ticket reservations", mailBody);
        }
    }

}
