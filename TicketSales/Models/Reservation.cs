namespace TicketSales.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
        public virtual Ticket Ticket { get; set; }
    }

}
