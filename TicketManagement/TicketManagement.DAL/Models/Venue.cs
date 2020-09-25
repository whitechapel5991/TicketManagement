// ****************************************************************************
// <copyright file="Venue.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("Venues")]
    public class Venue : Entity
    {
        [StringLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(120)]
        [Column("Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Address")]
        public string Address { get; set; }

        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        [Column("Phone")]
        public string Phone { get; set; }
    }
}
