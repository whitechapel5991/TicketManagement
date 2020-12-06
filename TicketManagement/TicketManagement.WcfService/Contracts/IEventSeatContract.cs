// ****************************************************************************
// <copyright file="IEventSeatContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using TicketManagement.DAL.Constants;

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

    [DataContract]
    public class EventSeat
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public EventSeatState State { get; set; }

        [DataMember]
        public int EventAreaId { get; set; }

        public TicketManagement.DAL.Models.EventSeat ConvertToBllEventSeat()
        {
            return new TicketManagement.DAL.Models.EventSeat()
            {
                Id = this.Id,
                Row = this.Row,
                Number = this.Number,
                State = this.State,
                EventAreaId = this.EventAreaId,
            };
        }
    }
}
