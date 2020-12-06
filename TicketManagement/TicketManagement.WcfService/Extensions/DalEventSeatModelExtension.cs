// ****************************************************************************
// <copyright file="DalEventSeatModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalEventSeatModelExtension
    {
        public static EventSeat ConvertToWcfEventSeat(this TicketManagement.DAL.Models.EventSeat bllEventSeat)
        {
            return new EventSeat()
            {
                Id = bllEventSeat.Id,
                Row = bllEventSeat.Row,
                Number = bllEventSeat.Number,
                State = bllEventSeat.State,
                EventAreaId = bllEventSeat.EventAreaId,
            };
        }
    }
}