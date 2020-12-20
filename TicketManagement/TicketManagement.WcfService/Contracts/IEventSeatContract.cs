// ****************************************************************************
// <copyright file="IEventSeatContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IEventSeatContract
    {
        [OperationContract]
        IEnumerable<EventSeat> GetEventSeats();

        [OperationContract]
        EventSeat GetEventSeat(int id);

        [OperationContract]
        void UpdateEventSeat(EventSeat entity);

        [OperationContract]
        IEnumerable<EventSeat> GetEventSeatsByEventSeatIds(int[] idArray);

        [OperationContract]
        IEnumerable<EventSeat> GetEventSeatsByEventAreaId(int eventAreaId);
    }
}
