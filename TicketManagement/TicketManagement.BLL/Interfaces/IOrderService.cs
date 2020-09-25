// ****************************************************************************
// <copyright file="IOrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddToCart(int eventSeatId, TicketManagementUser user);

        List<Order> GetHistoryOrdersById(TicketManagementUser user);
    }
}
