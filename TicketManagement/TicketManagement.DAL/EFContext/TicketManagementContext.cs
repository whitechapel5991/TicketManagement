using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.DAL.Models;

namespace TicketManagement.DAL.EFContext
{
    public class TicketManagementContext : DbContext
    {
        public TicketManagementContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<EventArea> EventAreas { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventSeat> EventSeats { get; set; }

        public DbSet<Layout> Layouts { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Venue> Venues { get; set; }
    }
}
