using System.ComponentModel.DataAnnotations;

namespace TicketSales.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int QuantityAvailable { get; set; }

        // Navigation property
        public virtual Event Event { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public Ticket()
        {
            Reservations = new HashSet<Reservation>();
        }
    }

}
