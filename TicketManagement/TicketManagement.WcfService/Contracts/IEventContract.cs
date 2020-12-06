// ****************************************************************************
// <copyright file="IEventContract.cs" company="EPAM Systems">
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
    public interface IEventContract
    {
        [OperationContract]
        IEnumerable<Event> GetEvents();

        [OperationContract]
        Event GetEvent(int id);

        [OperationContract]
        int AddEvent(Event entity);

        [OperationContract]
        void UpdateEvent(Event entity);

        [OperationContract]
        void DeleteEvent(int id);

        [OperationContract]
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

    [DataContract]
    public class Event
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime BeginDateUtc { get; set; }

        [DataMember]
        public DateTime EndDateUtc { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public int LayoutId { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        public TicketManagement.DAL.Models.Event ConvertToBllEvent()
        {
            return new TicketManagement.DAL.Models.Event()
            {
                Id = this.Id,
                Name = this.Name,
                BeginDateUtc = this.BeginDateUtc,
                EndDateUtc = this.EndDateUtc,
                Description = this.Description,
                Published = this.Published,
                LayoutId = this.LayoutId,
                ImageUrl = this.ImageUrl,
            };
        }
    }
}
