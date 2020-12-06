// ****************************************************************************
// <copyright file="EventSeatService.svc.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.WcfService.Contracts;
using TicketManagement.WcfService.Extensions;

namespace TicketManagement.WcfService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EventSeatService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EventSeatService.svc or EventSeatService.svc.cs at the Solution Explorer and start debugging.
    public class EventSeatService : IEventSeatContract
    {
        private readonly IEventSeatService eventSeatService;

        public EventSeatService(IEventSeatService eventSeatService)
        {
            this.eventSeatService = eventSeatService;
        }

        public EventSeat GetEventSeat(int id)
        {
            return this.eventSeatService.GetEventSeat(id).ConvertToWcfEventSeat();
        }

        public IEnumerable<EventSeat> GetEventSeats()
        {
            return this.eventSeatService.GetEventSeats().Select(entity => entity.ConvertToWcfEventSeat());
        }

        public IEnumerable<EventSeat> GetEventSeatsByEventAreaId(int eventAreaId)
        {
            return this.eventSeatService.GetEventSeatsByEventAreaId(eventAreaId).Select(entity => entity.ConvertToWcfEventSeat());
        }

        public IEnumerable<EventSeat> GetEventSeatsByEventSeatIds(int[] idArray)
        {
            return this.eventSeatService.GetEventSeatsByEventSeatIds(idArray).Select(entity => entity.ConvertToWcfEventSeat());
        }

        public void UpdateEventSeat(EventSeat entity)
        {
            this.eventSeatService.UpdateEventSeat(entity.ConvertToBllEventSeat());
        }
    }
}
