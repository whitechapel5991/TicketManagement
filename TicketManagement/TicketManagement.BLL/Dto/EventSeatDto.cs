// ****************************************************************************
// <copyright file="EventSeatDto.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;
using TicketManagement.DAL.Constants;

namespace TicketManagement.BLL.Dto
{
    public class EventSeatDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        public EventSeatState State { get; set; }

        public EventAreaDto EventArea { get; set; }
    }
}