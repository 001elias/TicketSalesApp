using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TicketSales.Models;


namespace TicketSales.Controllers
{
    public class UsersController : Controller
    {
        private TicketReservationContext db = new TicketReservationContext();

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user, string PasswordHashConfirm)
        {
            if (ModelState.IsValid)
            {
                if (user.PasswordHash != PasswordHashConfirm)
                {
                    ModelState.AddModelError("ValidationError", "Passwords do not match.");
                    return View(user);
                }
                // Check if user already exists
                var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("ValidationError", "An account with this email already exists.");
                    return View(user);
                }
                
                user.CreatedAt = DateTime.Now;
                // Hash the password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                // Save the user
                db.Users.Add(user);
                db.SaveChanges();


                return RedirectToAction("Login");
            }
            else
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.PasswordHash))
                {
                    ModelState.AddModelError("ValidationError", "All fields are required.");
                    return View(user);
                }                
            }

            return View(user);
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            using (TicketReservationContext db = new TicketReservationContext())
            {

                var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                
                if (existingUser != null && VerifyPassword(existingUser.PasswordHash, user.PasswordHash))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, existingUser.Name),
                        new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
                        new Claim(ClaimTypes.Email, existingUser.Email),
                    };  

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Set IsPersistent to true if you want the cookie to persist after the browser is closed
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Redirect after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Email or Password is incorrect");
                }
            }
            return View(user);
        }

        private bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // This is where you verify the password. Replace with your password verification logic.
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page or home page after logout
            return RedirectToAction("Login", "Users");
        }


        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
