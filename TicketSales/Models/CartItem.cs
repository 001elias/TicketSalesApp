namespace TicketSales.Models
{
    public class CartItem
    {
    
        public int TicketID { get; set; }
        public int EventID { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string TicketType { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Venue { get; set; }
    }
}
