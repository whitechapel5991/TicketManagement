// ****************************************************************************
// <copyright file="Event.cs" company="EPAM Systems">
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
    [Table("Events")]
    public class Event : Entity
    {
        [Required]
        [StringLength(120)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("BeginDate")]
        [DataType(DataType.DateTime)]
        public DateTime BeginDateUtc { get; set; }

        [Required]
        [Column("EndDate")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateUtc { get; set; }

        [Required]
        [Column("Description")]
        public string Description { get; set; }

        [Required]
        [Column("Published")]
        public bool Published { get; set; }

        [Required]
        [Column("LayoutId")]
        public int LayoutId { get; set; }
    }
}
