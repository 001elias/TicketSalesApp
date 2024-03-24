using System.ComponentModel.DataAnnotations;

namespace TicketSales.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Province { get; set; }

        [Required]
        public int Capacity { get; set; }

        // Navigation property to relate Venue with Events
        public virtual ICollection<Event> Events { get; set; }

        // Constructor to initialize the collection property
        public Venue()
        {
            Events = new HashSet<Event>();
        }
    }
}
