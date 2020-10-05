// ****************************************************************************
// <copyright file="IOrderService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddToCart(int eventSeatId, int userId);

        void Buy(int orderId);

        void DeleteFromCart(int orderId);

        List<Order> GetHistoryOrdersById(int userId);

        List<Order> GetHistoryOrdersByName(string userName);

        List<Order> GetCartOrdersById(int userId);

        List<Order> GetCartOrdersByName(string userName);
    }
}
