// ****************************************************************************
// <copyright file="IEventAreaContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IEventAreaContract
    {
        [OperationContract]
        IEnumerable<EventArea> GetEventAreas();

        [OperationContract]
        EventArea GetEventArea(int id);

        [OperationContract]
        void UpdateEventArea(EventArea entity);

        [OperationContract]
        decimal GetEventAreaCost(int seatId);

        [OperationContract]
        IEnumerable<EventArea> GetEventAreasByEventSeatIds(int[] eventSeatIdArray);

        [OperationContract]
        IEnumerable<EventArea> GetEventAreasByEventId(int eventId);
    }
}
