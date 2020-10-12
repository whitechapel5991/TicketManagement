// ****************************************************************************
// <copyright file="EventArea.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("EventAreas")]
    public class EventArea : Entity
    {
        [Required]
        [StringLength(200)]
        [Column("Description")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("CoordX")]
        public int CoordinateX { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("CoordY")]
        public int CoordinateY { get; set; }

        [DataType(DataType.Currency)]
        [Column("Price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("EventId")]
        public int EventId { get; set; }
    }
}
