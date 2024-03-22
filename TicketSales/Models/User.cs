using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketSales.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property for the reservations made by the user
        public virtual ICollection<Reservation> Reservations { get; set; }

        public User()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
