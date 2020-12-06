// ****************************************************************************
// <copyright file="IEventAreaContract.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Runtime.Serialization;
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

    [DataContract]
    public class EventArea
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CoordinateX { get; set; }

        [DataMember]
        public int CoordinateY { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int EventId { get; set; }

        public TicketManagement.DAL.Models.EventArea ConvertToBllEventArea()
        {
            return new TicketManagement.DAL.Models.EventArea()
            {
                Id = this.Id,
                Description = this.Description,
                CoordinateX = this.CoordinateX,
                CoordinateY = this.CoordinateY,
                Price = this.Price,
                EventId = this.EventId,
            };
        }
    }
}
