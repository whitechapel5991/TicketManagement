// ****************************************************************************
// <copyright file="Layout.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class Layout : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(120)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int VenueId { get; set; }
    }
}
