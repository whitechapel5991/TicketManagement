// ****************************************************************************
// <copyright file="EventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.Web.EventAreaService;
using TicketManagement.Web.EventSeatService;
using TicketManagement.Web.EventService;
using TicketManagement.Web.LayoutService;
using TicketManagement.Web.Models.Event;
using TicketManagement.Web.OrderService;
using TicketManagement.Web.WcfInfrastructure;
using IEventServiceWeb = TicketManagement.Web.Interfaces.IEventService;

namespace TicketManagement.Web.Services
{
    internal class EventService : IEventServiceWeb
    {
        public IEnumerable<EventViewModel> GetPublishEvents()
        {
            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                return this.MapToEventViewModel(client.GetPublishEvents());
            }
        }

        public int AddToCart(int seatId, int userId)
        {
            using (var client = new EventSeatContractClient())
            {
                client.AddClientCredentials();
                using (var orderClient = new OrderContractClient())
                {
                    client.AddClientCredentials();
                    orderClient.AddToCart(seatId, userId);
                }

                return client.GetEventSeat(seatId).EventAreaId;
            }
        }

        public EventDetailViewModel GetEventDetailViewModel(int eventId)
        {
            Event eventDetails = null;
            Layout layout = null;
            EventArea[] eventAreas = null;

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                eventDetails = client.GetEvent(eventId);
            }

            using (var client = new LayoutContractClient())
            {
                client.AddClientCredentials();
                layout = client.GetLayout(eventDetails.LayoutId);
            }

            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                eventAreas = client.GetEventAreasByEventId(eventId);
            }

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
            EventArea eventArea = null;
            EventSeat[] eventSeats = null;

            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                eventArea = client.GetEventArea(eventAreaId);
            }

            using (var client = new EventSeatContractClient())
            {
                client.AddClientCredentials();
                eventSeats = client.GetEventSeatsByEventAreaId(eventAreaId);
            }

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
            Dictionary<int, string> layouts = null;
            Dictionary<int, int> eventsDictionary = null;

            using (var client = new LayoutContractClient())
            {
                client.AddClientCredentials();
                layouts = client.GetLayoutsByLayoutIds(eventList.Select(x => x.LayoutId).ToArray()).Distinct().ToDictionary(x => x.Id, x => x.Name);
            }

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                eventsDictionary = eventList.ToDictionary(x => x.Id, x => client.GetAvailableSeatCount(x.Id));
            }

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