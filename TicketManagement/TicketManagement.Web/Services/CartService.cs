// ****************************************************************************
// <copyright file="CartService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.Web.EventAreaService;
using TicketManagement.Web.EventSeatService;
using TicketManagement.Web.EventService;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Cart;
using TicketManagement.Web.OrderService;
using TicketManagement.Web.WcfInfrastructure;

namespace TicketManagement.Web.Services
{
    internal class CartService : ICartService
    {
        public void Buy(int orderId)
        {
            using (var client = new OrderContractClient())
            {
                client.AddClientCredentials();
                client.Buy(orderId);
            }
        }

        public void Delete(int orderId)
        {
            using (var client = new OrderContractClient())
            {
                client.AddClientCredentials();
                client.DeleteFromCart(orderId);
            }
        }

        public CartViewModel GetCartViewModelByUserName(string userName)
        {
            using (var client = new OrderContractClient())
            {
                client.AddClientCredentials();
                return this.MapToCartViewModel(client.GetCartOrdersByName(userName).ToList());
            }
        }

        private CartViewModel MapToCartViewModel(List<OrderService.Order> userCartOrders)
        {
            var purchaseHistoryVm = new CartViewModel()
            {
                Orders = new List<OrderViewModel>(),
            };

            var eventSeatIdArray = userCartOrders.Select(x => x.EventSeatId).Distinct().ToArray();

            List<EventSeat> eventSeats = null;
            List<EventArea> eventAreas = null;
            List<Event> events = null;
            using (var client = new EventSeatContractClient())
            {
                client.AddClientCredentials();
                eventSeats = client.GetEventSeatsByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                eventAreas = client.GetEventAreasByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            // Event Area dictionary, Key is event seats array in cart which belong to the event area.
            var eventAreasDictionary = eventAreas.ToDictionary(x => eventSeats.Where(y => y.EventAreaId == x.Id).Select(z => z.Id), x => x);

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                events = client.GetEventsByEventSeatIds(eventSeatIdArray).Distinct().ToList();
            }

            // Event dictionary, Key is event seats array in cart which belong to the event.
            var eventDictionary = events.ToDictionary(
                x =>
                        eventSeats.Where(y => eventAreas.Any(z => z.EventId == x.Id && y.EventAreaId == z.Id))
                        .Select(z => z.Id).ToArray(), x => x);

            foreach (var order in userCartOrders)
            {
                var eventKey = eventDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));
                var eventAreaKey = eventAreasDictionary.Keys.First(x => x.Any(z => z == order.EventSeatId));

                var key = eventKey ?? Array.Empty<int>().ToArray();
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