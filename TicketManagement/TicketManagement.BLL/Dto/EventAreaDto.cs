// ****************************************************************************
// <copyright file="EventAreaDto.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.BLL.Dto
{
    public class EventAreaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }

        public decimal Price { get; set; }

        public EventDto Event { get; set; }

        public IEnumerable<EventSeatDto> EventSeats { get; set; }
    }
}
