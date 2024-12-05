// ****************************************************************************
// <copyright file="Event.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Models
{
    public class Event : Entity
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int LayoutId { get; set; }
    }
}
