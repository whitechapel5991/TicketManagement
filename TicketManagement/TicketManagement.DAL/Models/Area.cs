// ****************************************************************************
// <copyright file="Area.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class Area : Entity
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0, 10000)]
        public int CoordX { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CoordY { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int LayoutId { get; set; }
    }
}
