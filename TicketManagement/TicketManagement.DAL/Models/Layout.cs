// ****************************************************************************
// <copyright file="Layout.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("Layouts")]
    public class Layout : Entity
    {
        [StringLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(120)]
        [Column("Description")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("VenueId")]
        public int VenueId { get; set; }
    }
}
