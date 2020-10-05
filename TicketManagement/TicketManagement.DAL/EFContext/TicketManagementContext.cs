// ****************************************************************************
// <copyright file="TicketManagementContext.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data.Entity;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;

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

        public DbSet<Order> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TicketManagementUser> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
