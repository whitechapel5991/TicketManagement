// ****************************************************************************
// <copyright file="EventSeatDto.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BLL.DTO
{
    public class EventSeatDto
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public int State { get; set; }

        public int EventAreaId { get; set; }
    }
}
