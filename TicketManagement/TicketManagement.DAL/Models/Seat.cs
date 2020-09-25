// ****************************************************************************
// <copyright file="Seat.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    [Table("Seats")]
    public class Seat : Entity
    {
        [Required]
        [Range(0, int.MaxValue)]
        [Column("Row")]
        public int Row { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("Number")]
        public int Number { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Column("AreaId")]
        public int AreaId { get; set; }
    }
}
