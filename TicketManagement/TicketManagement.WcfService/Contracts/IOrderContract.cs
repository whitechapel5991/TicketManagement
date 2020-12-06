// ****************************************************************************
// <copyright file="IOrderContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IOrderContract
    {
        [OperationContract]
        void AddToCart(int eventSeatId, int userId);

        [OperationContract]
        void Buy(int orderId);

        [OperationContract]
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

    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int EventSeatId { get; set; }

        [DataMember]
        public DateTime DateUtc { get; set; }

        public TicketManagement.DAL.Models.Order ConvertToBllOrder()
        {
            return new TicketManagement.DAL.Models.Order()
            {
                Id = this.Id,
                UserId = this.UserId,
                EventSeatId = this.EventSeatId,
                DateUtc = this.DateUtc,
            };
        }
    }
}
