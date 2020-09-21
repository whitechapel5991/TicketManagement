// ****************************************************************************
// <copyright file="EventSeat.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class EventSeat : Entity
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Row { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Column("State")]
        public EventSeatState State { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int EventAreaId { get; set; }
    }
}
