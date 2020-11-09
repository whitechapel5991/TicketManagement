// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Models.Event;
using IEventServiceWeb = TicketManagement.Web.Interfaces.IEventService;

namespace TicketManagement.Web.Services
{
    internal class EventService : IEventServiceWeb
    {
        private readonly IEventService eventService;
        private readonly IEventAreaService eventAreaService;
        private readonly IEventSeatService eventSeatService;
        private readonly ILayoutService layoutService;
        private readonly IOrderService orderService;

        public EventService(
            IEventService eventService,
            ILayoutService layoutService,
            IOrderService orderService,
            IEventAreaService eventAreaService,
            IEventSeatService eventSeatService)
        {
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.orderService = orderService;
            this.eventAreaService = eventAreaService;
            this.eventSeatService = eventSeatService;
        }

        public IEnumerable<EventViewModel> GetPublishEvents()
        {
            return this.MapToEventViewModel(this.eventService.GetPublishEvents());
        }

        public int AddToCart(int seatId, int userId)
        {
            this.orderService.AddToCart(seatId, userId);
            return this.eventSeatService.GetEventSeat(seatId).EventAreaId;
        }

        public EventDetailViewModel GetEventDetailViewModel(int eventId)
        {
            var eventDetails = this.eventService.GetEvent(eventId);
            var layout = this.layoutService.GetLayout(eventDetails.LayoutId);
            var eventAreas = this.eventAreaService.GetEventAreasByEventId(eventId);

            var eventDetailVm = new EventDetailViewModel
            {
                EventId = eventId,
                Name = eventDetails.Name,
                Description = eventDetails.Description,
                BeginDate = eventDetails.BeginDateUtc,
                EndDate = eventDetails.EndDateUtc,
                LayoutName = layout.Name,
                EventAreas = new List<EventAreaViewModel>(),
            };

            foreach (var eventArea in eventAreas)
            {
                eventDetailVm.EventAreas.Add(this.MapToEventAreaViewModel(eventArea));
            }

            return eventDetailVm;
        }

        public EventAreaDetailViewModel GetEventAreaDetailViewModel(int eventAreaId)
        {
            var eventArea = this.eventAreaService.GetEventArea(eventAreaId);
            var eventSeats = this.eventSeatService.GetEventSeatsByEventAreaId(eventAreaId);

            var eventAreaDetailVm = new EventAreaDetailViewModel
            {
                EventArea = this.MapToEventAreaViewModel(eventArea),
                EventId = eventArea.EventId,
                EventSeats = new List<EventSeatViewModel>(),
            };

            foreach (var eventSeat in eventSeats)
            {
                var eventSeatVm = new EventSeatViewModel
                {
                    Id = eventSeat.Id,
                    Row = eventSeat.Row,
                    Number = eventSeat.Number,
                    State = eventSeat.State,
                };

                eventAreaDetailVm.EventSeats.Add(eventSeatVm);
            }

            return eventAreaDetailVm;
        }

        private IEnumerable<EventViewModel> MapToEventViewModel(IEnumerable<Event> events)
        {
            var eventList = events.Distinct().ToList();
            var layouts = this.layoutService.GetLayoutsByLayoutIds(eventList.Select(x => x.LayoutId).ToArray()).Distinct().ToDictionary(x => x.Id, x => x.Name);
            var eventsDictionary = eventList.ToDictionary(x => x.Id, x => this.eventService.GetAvailableSeatCount(x.Id));

            return eventList.Select(@event => new EventViewModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                BeginDate = @event.BeginDateUtc,
                EndDate = @event.EndDateUtc,
                BeginTime = @event.BeginDateUtc,
                EndTime = @event.EndDateUtc,
                CountFreeSeats = eventsDictionary[@event.Id],
                LayoutName = layouts[@event.LayoutId],
                ImagePath = @event.ImageUrl,
            })
                .ToList();
        }

        private EventAreaViewModel MapToEventAreaViewModel(EventArea eventArea)
        {
            return new EventAreaViewModel
            {
                Id = eventArea.Id,
                Description = eventArea.Description,
                CoordinateX = eventArea.CoordinateX,
                CoordinateY = eventArea.CoordinateY,
                Price = eventArea.Price,
            };
        }
    }
}