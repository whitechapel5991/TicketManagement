// ****************************************************************************
// <copyright file="IOrderContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;
//using TicketManagement.BLL.Exceptions.Base;
//using TicketManagement.BLL.Exceptions.OrderExceptions;
using EntityDoesNotExistException = TicketManagement.WcfService.Exceptions.EntityDoesNotExistException;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IOrderContract
    {
        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(SeatCurrentlySoldOrBlockedException))]
        void AddToCart(int eventSeatId, int userId);

        [OperationContract]
        [FaultContract(typeof(NotEnoughMoneyException))]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(SeatIsNotInTheBasketException))]
        void Buy(int orderId);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(SeatIsNotInTheBasketException))]
        void DeleteFromCart(int orderId);

        [OperationContract]
        IEnumerable<Order> GetHistoryOrdersById(int userId);

        [OperationContract]
        IEnumerable<Order> GetHistoryOrdersByName(string userName);

        [OperationContract]
        IEnumerable<Order> GetCartOrdersById(int userId);

        [OperationContract]
        IEnumerable<Order> GetCartOrdersByName(string userName);
    }
}
