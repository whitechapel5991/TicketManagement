// ****************************************************************************
// <copyright file="EventAreaService.svc.cs" company="EPAM Systems">
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EventAreaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EventAreaService.svc or EventAreaService.svc.cs at the Solution Explorer and start debugging.
    public class EventAreaService : IEventAreaContract
    {
        private readonly IEventAreaService eventAreaService;

        public EventAreaService(IEventAreaService eventAreaService)
        {
            this.eventAreaService = eventAreaService;
        }

        public EventArea GetEventArea(int id)
        {
            return this.eventAreaService.GetEventArea(id).ConvertToWcfEventArea();
        }

        public decimal GetEventAreaCost(int seatId)
        {
            return this.eventAreaService.GetEventAreaCost(seatId);
        }

        public IEnumerable<EventArea> GetEventAreas()
        {
            return this.eventAreaService.GetEventAreas()
                .Select(entity => entity.ConvertToWcfEventArea());
        }

        public IEnumerable<EventArea> GetEventAreasByEventId(int eventId)
        {
            return this.eventAreaService.GetEventAreasByEventId(eventId)
                .Select(entity => entity.ConvertToWcfEventArea());
        }

        public IEnumerable<EventArea> GetEventAreasByEventSeatIds(int[] eventSeatIdArray)
        {
            return this.eventAreaService.GetEventAreasByEventSeatIds(eventSeatIdArray)
                .Select(entity => entity.ConvertToWcfEventArea());
        }

        public void UpdateEventArea(EventArea entity)
        {
            this.eventAreaService.UpdateEventArea(entity.ConvertToBllEventArea());
        }
    }
}
