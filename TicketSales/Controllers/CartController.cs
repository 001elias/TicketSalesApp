using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TicketSales.Models;
using TicketSales.Extensions;

namespace TicketSales.Controllers
{
    public class CartController : Controller
    {
        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }
     
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int ticketId, 
                                       int eventId, 
                                       DateTime eventDate,
                                       string ticketType, 
                                       string description, 
                                       int quantity, 
                                       decimal price)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(item => item.TicketID == ticketId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                   EventDescription = description,
                   TicketID = ticketId,
                   EventID = eventId,
                   EventDate = eventDate,
                   TicketType = ticketType,
                   Quantity = quantity,                  
                   Price = price
                });
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
            HttpContext.Session.Set("Cart", new List<CartItem>());
            // TODO : add items to Reservation table

            
            return RedirectToAction("Index");
        }
    }

}
