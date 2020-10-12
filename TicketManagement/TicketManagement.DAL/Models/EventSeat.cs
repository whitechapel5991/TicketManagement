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
    [Table("EventSeats")]
    public class EventSeat : Entity
    {
        [Required]
        [Range(0, int.MaxValue)]
        [Column("Row")]
        public int Row { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("Number")]
        public int Number { get; set; }

        [Column("State")]
        [EnumDataType(typeof(EventSeatState))]
        public EventSeatState State { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("EventAreaId")]
        public int EventAreaId { get; set; }
    }
}
