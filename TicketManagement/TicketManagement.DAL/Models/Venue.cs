// ****************************************************************************
// <copyright file="Venue.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class Venue : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(120)]
        public string Description { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        //public ICollection<Layout> Layouts { get; set; }
    }
}
