// ****************************************************************************
// <copyright file="IEventSeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    public interface IEventSeatService
    {
        IEnumerable<EventSeat> GetEventSeats();

        EventSeat GetEventSeat(int id);

        void UpdateEventSeat(EventSeat entity);

        IEnumerable<EventSeat> GetEventSeatsByEventSeatIds(int[] idArray);

        IEnumerable<EventSeat> GetEventSeatsByEventAreaId(int eventAreaId);
    }
}
