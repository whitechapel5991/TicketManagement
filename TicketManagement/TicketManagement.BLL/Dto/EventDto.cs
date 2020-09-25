// ****************************************************************************
// <copyright file="EventDto.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Dto
{
    public class EventDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool Published { get; set; }

        [Required]
        public Layout Layout { get; set; }

        public IEnumerable<EventAreaDto> EventAreas { get; set; }
    }
}
