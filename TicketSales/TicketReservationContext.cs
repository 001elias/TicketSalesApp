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

    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //{
    //    // Disable cascade delete for Event-Tickets relationship
    //    modelBuilder.Entity<Event>()
    //        .HasMany(e => e.Tickets)
    //        .WithRequired(t => t.Event)
    //        .HasForeignKey(t => t.EventId)
    //        .WillCascadeOnDelete(false);

    //    // Assuming Reservation has foreign keys to both Event and Ticket
    //    // Disable cascade delete for Ticket-Reservations relationship
    //    modelBuilder.Entity<Ticket>()
    //        .HasMany(t => t.Reservations)
    //        .WithRequired(r => r.Ticket)
    //        .HasForeignKey(r => r.TicketId)
    //        .WillCascadeOnDelete(false);

    //    // Disable cascade delete for Event-Reservations relationship
    //    // This configuration depends on how your Reservation entity is set up
    //    modelBuilder.Entity<Event>()
    //        .HasMany(e => e.Reservations)
    //        .WithRequired(r => r.Event) // Adjust according to your actual navigation property
    //        .HasForeignKey(r => r.EventId)
    //        .WillCascadeOnDelete(false);

    //    // Repeat the process for other entities and their relationships
    //}

}