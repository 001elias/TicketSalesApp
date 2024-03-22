using System.ComponentModel.DataAnnotations;

namespace TicketSales.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }

        // Navigation properties
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public Event()
        {
            Tickets = new HashSet<Ticket>();
            Reservations = new HashSet<Reservation>();
        }
    }
}
