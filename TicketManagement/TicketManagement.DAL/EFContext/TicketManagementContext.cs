// ****************************************************************************
// <copyright file="TicketManagementContext.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.EFContext
{
    public class TicketManagementContext : IdentityDbContext<TicketManagementUser, Role, int,
        UserLogin, UserRole, UserClaim>
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
    }
}
