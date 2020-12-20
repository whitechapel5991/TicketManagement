// ****************************************************************************
// <copyright file="IEventContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.ServiceModel;
using EntityDoesNotExistException = TicketManagement.WcfService.Exceptions.EntityDoesNotExistException;

namespace TicketManagement.WcfService.Contracts
{
    [ServiceContract]
    public interface IEventContract
    {
        [OperationContract]
        IEnumerable<Event> GetEvents();

        [OperationContract]
        Event GetEvent(int id);

        [OperationContract]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(EventInPastException))]
        [FaultContract(typeof(BeginDateLongerThenEndDateException))]
        [FaultContract(typeof(EventExistInTheLayoutInThisTimeException))]
        [FaultContract(typeof(LayoutHasNotAreaException))]
        [FaultContract(typeof(LayoutHasNotSeatException))]
        int AddEvent(Event entity);

        [OperationContract]
        [FaultContract(typeof(LayoutHasSoldSeatAndCouldNotBeChangeException))]
        [FaultContract(typeof(EntityDoesNotExistException))]
        [FaultContract(typeof(EventInPastException))]
        [FaultContract(typeof(BeginDateLongerThenEndDateException))]
        [FaultContract(typeof(EventExistInTheLayoutInThisTimeException))]
        [FaultContract(typeof(LayoutHasNotAreaException))]
        [FaultContract(typeof(LayoutHasNotSeatException))]
        void UpdateEvent(Event entity);

        [OperationContract]
        [FaultContract(typeof(LayoutHasSoldSeatAndCouldNotBeChangeException))]
        void DeleteEvent(int id);

        [OperationContract]
        [FaultContract(typeof(EventAlreadyPublishedException))]
        [FaultContract(typeof(SomeAreaHasNotPriceException))]
        void PublishEvent(int id);

        [OperationContract]
        IEnumerable<Event> GetPublishEvents();

        [OperationContract]
        int GetAvailableSeatCount(int eventId);

        [OperationContract]
        Event GetEventByEventSeatId(int eventSeatId);

        [OperationContract]
        IEnumerable<Event> GetEventsByEventSeatIds(int[] eventSeatIdArray);
    }
}
