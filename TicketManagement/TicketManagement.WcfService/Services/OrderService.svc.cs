// ****************************************************************************
// <copyright file="OrderService.svc.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.WcfService.Contracts;
using TicketManagement.WcfService.Extensions;

namespace TicketManagement.WcfService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderService.svc or OrderService.svc.cs at the Solution Explorer and start debugging.
    public class OrderService : IOrderContract
    {
        private readonly IOrderService orderService;

        public OrderService(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public void AddToCart(int eventSeatId, int userId)
        {
            this.orderService.AddToCart(eventSeatId, userId);
        }

        public void Buy(int orderId)
        {
            this.orderService.Buy(orderId);
        }

        public void DeleteFromCart(int orderId)
        {
            this.orderService.DeleteFromCart(orderId);
        }

        public IEnumerable<Order> GetCartOrdersById(int userId)
        {
            return this.orderService.GetCartOrdersById(userId).Select(entity => entity.ConvertToWcfOrder());
        }

        public IEnumerable<Order> GetCartOrdersByName(string userName)
        {
            return this.orderService.GetCartOrdersByName(userName).Select(entity => entity.ConvertToWcfOrder());
        }

        public IEnumerable<Order> GetHistoryOrdersById(int userId)
        {
            return this.orderService.GetHistoryOrdersById(userId).Select(entity => entity.ConvertToWcfOrder());
        }

        public IEnumerable<Order> GetHistoryOrdersByName(string userName)
        {
            return this.orderService.GetHistoryOrdersByName(userName).Select(entity => entity.ConvertToWcfOrder());
        }
    }
}
