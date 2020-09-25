// ****************************************************************************
// <copyright file="Order.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("Orders")]
    public class Order : Entity
    {
        [Required]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [Column("EventSeatId")]
        public int EventSeatId { get; set; }

        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }
    }
}
