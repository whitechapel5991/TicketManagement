// ****************************************************************************
// <copyright file="CartService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Cart;

namespace TicketManagement.Web.Services
{
    internal class CartService : ICartService
    {
        private readonly IOrderService orderService;
        private readonly IEventSeatService eventSeatService;
        private readonly IEventAreaService eventAreaService;
        private readonly BLL.Interfaces.IEventService eventService;

        public CartService(
            IOrderService orderService,
            IEventSeatService eventSeatService,
            IEventAreaService eventAreaService,
            BLL.Interfaces.IEventService eventService)
        {
            this.orderService = orderService;
            this.eventSeatService = eventSeatService;
            this.eventAreaService = eventAreaService;
            this.eventService = eventService;
        }

        public void Buy(int orderId)
        {
            this.orderService.Buy(orderId);
        }

        public void Delete(int orderId)
        {
            this.orderService.DeleteFromCart(orderId);
        }

        public CartViewModel GetCartViewModelByUserName(string userName)
        {
            return this.MapToCartViewModel(this.orderService.GetCartOrdersByName(userName).ToList());
        }

        private CartViewModel MapToCartViewModel(List<Order> userCartOrders)
        {
            var purchaseHistoryVm = new CartViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            var eventSeatIdArray = userCartOrders.Select(x => x.EventSeatId).Distinct().ToArray();

            var eventSeats = this.eventSeatService.GetEventSeatsByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            var eventAreas = this.eventAreaService.GetEventAreasByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            // Event Area dictionary, Key is event seats array in cart which belong to the event area.
            var eventAreasDictionary = eventAreas.ToDictionary(x => eventSeats.Where(y => y.EventAreaId == x.Id).Select(z => z.Id), x => x);

            var events = this.eventService.GetEventsByEventSeatIds(eventSeatIdArray).Distinct().ToList();

            // Event dictionary, Key is event seats array in cart which belong to the event.
            var eventDictionary = events.ToDictionary(
                x =>
                        eventSeats.Where(y => eventAreas.Any(z => z.EventId == x.Id && y.EventAreaId == z.Id))
                        .Select(z => z.Id), x => x);

            foreach (var order in userCartOrders)
            {
                var eventKey = eventDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));
                var eventAreaKey = eventAreasDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));

                var key = eventKey as int[] ?? eventKey.ToArray();
                var orderVm = new OrderViewModel
                {
                    OrderId = order.Id,
                    TicketCost = eventAreasDictionary[eventAreaKey].Price,
                    EventName = eventDictionary[key].Name,
                    EventDescription = eventDictionary[key].Description,
                };

                purchaseHistoryVm.Orders.Add(orderVm);
            }

            return purchaseHistoryVm;
        }
    }
}