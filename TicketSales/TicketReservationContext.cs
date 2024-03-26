using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Sockets;
using TicketSales.Models;

public class TicketReservationContext : DbContext
{
    public TicketReservationContext() : base(@"Server=eventappserver.database.windows.net;Database=eventappdb;User Id=sqladmin;Password=Sophia0204!;")        
    {
        // If your database schema is fully compatible with your models and you don't want EF to modify the database schema, consider disabling database initialization strategies:
        Database.SetInitializer<TicketReservationContext>(null);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Reservation> Reservations { get; set; }


}