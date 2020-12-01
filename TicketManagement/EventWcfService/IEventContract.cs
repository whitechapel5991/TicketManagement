using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EventWcfService
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
                Id = Id,
                Name = Name,
                BeginDateUtc = BeginDateUtc,
                EndDateUtc = EndDateUtc,
                Description = Description,
                Published = Published,
                LayoutId = LayoutId,
                ImageUrl = ImageUrl,
            };
        }
    }
}
