using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using EventWcfService.Extension;

namespace EventWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EventService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EventService.svc or EventService.svc.cs at the Solution Explorer and start debugging.
    public class EventService : IEventContract
    {
        private readonly IEventService eventService;

        public EventService(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public int AddEvent(Event entity)
        {
            return this.eventService.AddEvent(entity.ConvertToBllEvent());
        }

        public void DeleteEvent(int id)
        {
            this.eventService.DeleteEvent(id);
        }

        public int GetAvailableSeatCount(int eventId)
        {
            return this.eventService.GetAvailableSeatCount(eventId);
        }

        public Event GetEvent(int id)
        {
            return this.eventService.GetEvent(id).ConvertToWcfEvent();
        }

        public Event GetEventByEventSeatId(int eventSeatId)
        {
            return this.eventService.GetEventByEventSeatId(eventSeatId).ConvertToWcfEvent();
        }

        public IEnumerable<Event> GetEvents()
        {
            return this.eventService.GetEvents().Select(entity => entity.ConvertToWcfEvent());
        }

        public IEnumerable<Event> GetEventsByEventSeatIds(int[] eventSeatIdArray)
        {
            return this.eventService.GetEventsByEventSeatIds(eventSeatIdArray).Select(entity => entity.ConvertToWcfEvent());
        }

        public IEnumerable<Event> GetPublishEvents()
        {
            return this.eventService.GetPublishEvents().Select(entity => entity.ConvertToWcfEvent());
        }

        public void PublishEvent(int id)
        {
            this.eventService.PublishEvent(id);
        }

        public void UpdateEvent(Event entity)
        {
            this.eventService.UpdateEvent(entity.ConvertToBllEvent());
        }
    }
}
