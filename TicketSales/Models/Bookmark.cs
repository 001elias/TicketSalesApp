namespace TicketSales.Models
{
    public partial class Bookmark
    {
        public int BookmarkId { get; set; }
        public int UserID { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }

}
