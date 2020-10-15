// ****************************************************************************
// <copyright file="OrderValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Linq;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.OrderExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.BLL.ServiceValidators
{
    public class OrderValidator : IOrderValidator
    {
        private readonly IRepository<EventSeat> eventSeatRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IUserRepository userRepository;
        private readonly IRepository<Order> orderRepository;

        public OrderValidator(
            IRepository<EventSeat> eventSeatRepository,
            IUserRepository userRepository,
            IRepository<Order> orderRepository,
            IRepository<EventArea> eventAreaRepository)
        {
            this.eventSeatRepository = eventSeatRepository;
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.eventAreaRepository = eventAreaRepository;
        }

        public void AddToCartValidation(Order newOrder)
        {
            var eventSeat = this.eventSeatRepository.GetById(newOrder.EventSeatId);
            if (eventSeat == default(EventSeat))
            {
                throw new EntityDoesNotExistException("EventSeat doesn't exist.");
            }

            var user = this.userRepository.GetById(newOrder.UserId);
            if (user == default(TicketManagementUser))
            {
                throw new EntityDoesNotExistException("User doesn't exist.");
            }

            if (eventSeat.State != EventSeatState.Free)
            {
                throw new SeatCurrentlySoldOrBlockedException($"Seat with row {eventSeat.Row} and number {eventSeat.Number} currently is not free.");
            }
        }

        public void BuyValidation(int orderId)
        {
            this.BuyAndDeleteFromCartValidation(orderId);

            var order = this.orderRepository.GetById(orderId);
            var eventSeat = this.eventSeatRepository.GetById(order.EventSeatId);
            var eventAreaPrice = this.eventAreaRepository.GetById(eventSeat.EventAreaId).Price;
            var userBalance = this.userRepository.GetById(order.UserId).Balance;

            if (userBalance - eventAreaPrice < 0)
            {
                throw new NotEnoughMoneyException("It isn't enough funds, top up the balance.");
            }
        }

        public void DeleteFromCartValidation(int orderId)
        {
            this.BuyAndDeleteFromCartValidation(orderId);
        }

        private void BuyAndDeleteFromCartValidation(int orderId)
        {
            var order = this.orderRepository.GetById(orderId);
            if (order == default(Order))
            {
                throw new EntityDoesNotExistException("Order doesn't exist.");
            }

            if (this.eventSeatRepository.GetById(order.EventSeatId).State != EventSeatState.InBasket)
            {
                throw new SeatIsNotInTheBasketException("Seat must be have state in basket.");
            }
        }
    }
}
