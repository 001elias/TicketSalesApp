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
        // Removed Location
        public DateTime EventDateTime { get; set; }
        public int VenueId { get; set; } // Add VenueId as a foreign key

        // Navigation properties
        public virtual Venue Venue { get; set; } // Add Venue navigation property
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public Event()
        {
            Tickets = new HashSet<Ticket>();
            Reservations = new HashSet<Reservation>();
        }
    }
}
