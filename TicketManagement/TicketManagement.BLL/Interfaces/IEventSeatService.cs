﻿// ****************************************************************************
// <copyright file="IEventSeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.DTO;

namespace TicketManagement.BLL.Interfaces
{
    internal interface IEventSeatService
    {
        IEnumerable<EventSeatDto> GetEventSeats();

        EventSeatDto GetEventSeat(int id);

        void UpdateEventSeat(EventSeatDto entity);
    }
}