// ****************************************************************************
// <copyright file="Area.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("Areas")]
    public class Area : Entity
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

        [Required]
        [Range(0, int.MaxValue)]
        [Column("LayoutId")]
        public int LayoutId { get; set; }
    }
}
