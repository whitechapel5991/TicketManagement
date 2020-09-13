// ****************************************************************************
// <copyright file="EventArea.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class EventArea : Entity
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CoordX { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CoordY { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int EventId { get; set; }
    }
}
