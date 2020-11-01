using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.Models.Event;
using EventViewModel = TicketManagement.Web.Areas.EventManager.Data.EventViewModel;
using IEventService = TicketManagement.BLL.Interfaces.IEventService;

namespace TicketManagement.Web.Services
{
    internal class EventManagerEventService : IEventManagerEventService
    {
        private readonly IEventService eventService;
        private readonly ILayoutService layoutService;
        private readonly IEventAreaService eventAreaService;
        private readonly IEventSeatService eventSeatService;

        public EventManagerEventService(
            IEventService eventService,
            ILayoutService layoutService,
            IEventAreaService eventAreaService,
            IEventSeatService eventSeatService)
        {
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.eventAreaService = eventAreaService;
            this.eventSeatService = eventSeatService;
        }

        public List<IndexEventViewModel> GetIndexEventViewModels()
        {
            return this.MapToIndexEventViewModel(this.eventService.GetEvents().ToList());
        }

        public EventViewModel GetEventViewModel()
        {
            var layouts = this.layoutService.GetLayouts().ToList();
            return new EventViewModel
            {
                LayoutList = new SelectList(layouts, "Id", "Name", layouts.First()),
            };
        }

        public EventViewModel GetEventViewModel(int eventId)
        {
            var eventDto = this.eventService.GetEvent(eventId);
            var layouts = this.layoutService.GetLayouts().Distinct().ToDictionary(x => x.Id, x => x.Name);

            return new EventViewModel
            {
                IndexEventViewModel = new IndexEventViewModel
                {
                    Id = eventDto.Id,
                    BeginDate = eventDto.BeginDateUtc,
                    BeginTime = eventDto.BeginDateUtc,
                    Description = eventDto.Description,
                    EndDate = eventDto.EndDateUtc,
                    EndTime = eventDto.EndDateUtc,
                    LayoutId = eventDto.LayoutId,
                    Name = eventDto.Name,
                    Published = eventDto.Published,
                    LayoutName = layouts[eventDto.LayoutId],
                },
                LayoutId = eventDto.LayoutId,
                LayoutList = new SelectList(layouts.OrderBy(x => x.Value), "Key", "Value", layouts[eventDto.LayoutId]),
            };
        }

        public void CreateEvent(EventViewModel eventViewModel)
        {
            this.eventService.AddEvent(
                new Event
                {
                    Name = eventViewModel.IndexEventViewModel.Name,
                    BeginDateUtc = eventViewModel.IndexEventViewModel.GetBeginDate(),
                    EndDateUtc = eventViewModel.IndexEventViewModel.GetEndDate(),
                    Description = eventViewModel.IndexEventViewModel.Description,
                    LayoutId = eventViewModel.LayoutId,
                });
        }

        public void UpdateEvent(EventViewModel eventViewModel)
        {
            this.eventService.UpdateEvent(new Event
            {
                Name = eventViewModel.IndexEventViewModel.Name,
                BeginDateUtc = eventViewModel.IndexEventViewModel.GetBeginDate(),
                EndDateUtc = eventViewModel.IndexEventViewModel.GetEndDate(),
                Description = eventViewModel.IndexEventViewModel.Description,
                LayoutId = eventViewModel.IndexEventViewModel.LayoutId,
                Published = eventViewModel.IndexEventViewModel.Published,
                Id = eventViewModel.IndexEventViewModel.Id,
            });
        }

        public void DeleteEvent(int id)
        {
            this.eventService.DeleteEvent(id);
        }

        public void PublishEvent(int id)
        {
            this.eventService.PublishEvent(id);
        }

        public AreaPriceViewModel GetAreaPriceViewModel(int areaId)
        {
            return new AreaPriceViewModel
            {
                EventAreaId = areaId,
                Price = this.eventAreaService.GetEventArea(areaId).Price,
            };
        }

        public void ChangeCost(AreaPriceViewModel areaPriceVm)
        {
            var area = this.eventAreaService.GetEventArea(areaPriceVm.EventAreaId);
            area.Price = areaPriceVm.Price;
            this.eventAreaService.UpdateEventArea(area);
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

        private List<IndexEventViewModel> MapToIndexEventViewModel(List<Event> eventList)
        {
            var layouts = this.layoutService.GetLayoutsByLayoutIds(eventList.Select(x => x.LayoutId).ToArray()).Distinct().ToDictionary(x => x.Id, x => x.Name);

            return eventList.Select(eventDto => new IndexEventViewModel
            {
                Id = eventDto.Id,
                BeginDate = eventDto.BeginDateUtc,
                BeginTime = eventDto.BeginDateUtc,
                Description = eventDto.Description,
                EndDate = eventDto.EndDateUtc,
                EndTime = eventDto.EndDateUtc,
                LayoutId = eventDto.LayoutId,
                Name = eventDto.Name,
                Published = eventDto.Published,
                LayoutName = layouts[eventDto.LayoutId],
            }).ToList();
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