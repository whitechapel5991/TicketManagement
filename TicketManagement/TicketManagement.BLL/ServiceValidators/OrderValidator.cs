// ****************************************************************************
// <copyright file="OrderValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using TicketManagement.BLL.Exceptions.OrderExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.ServiceValidators
{
    public class OrderValidator : IOrderValidator
    {
        public void SeatIsBlocked(EventSeat eventSeat)
        {
            if (eventSeat.State == EventSeatState.InBasket || eventSeat.State == EventSeatState.Sold)
            {
                throw new SeatCurrentlySoldOrBlocked($"Seat with row {eventSeat.Row} and number {eventSeat.Number} currently is not free.");
            }
        }

        public void Validate(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
