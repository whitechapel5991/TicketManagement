using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Interfaces;
using IEventService = TicketManagement.BLL.Interfaces.IEventService;

namespace TicketManagement.Web.Services
{
    internal class EventManagerEventService : IEventManagerEventService
    {
        private readonly IEventService eventService;
        private readonly ILayoutService layoutService;
        private readonly IEventAreaService eventAreaService;

        public EventManagerEventService(
            IEventService eventService,
            ILayoutService layoutService,
            IEventAreaService eventAreaService)
        {
            this.eventService = eventService;
            this.layoutService = layoutService;
            this.eventAreaService = eventAreaService;
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
            var layouts = this.layoutService.GetLayouts().ToList();

            return new EventViewModel
            {
                IndexEventViewModel = new IndexEventViewModel
                {
                    Id = eventDto.Id,
                    BeginDate = eventDto.BeginDate,
                    BeginTime = eventDto.BeginDate,
                    Description = eventDto.Description,
                    EndDate = eventDto.EndDate,
                    EndTime = eventDto.EndDate,
                    LayoutId = eventDto.LayoutId,
                    Name = eventDto.Name,
                    Published = eventDto.Published,
                    LayoutName = this.layoutService.GetLayout(eventDto.LayoutId).Name,
                },
                LayoutId = eventDto.LayoutId,
                LayoutList = new SelectList(layouts, "Id", "Name", eventDto.LayoutId),
            };
        }

        public void CreateEvent(EventViewModel eventViewModel)
        {
            this.eventService.AddEvent(
                new Event
                {
                    Name = eventViewModel.IndexEventViewModel.Name,
                    BeginDate = eventViewModel.IndexEventViewModel.GetBeginDate(),
                    EndDate = eventViewModel.IndexEventViewModel.GetEndDate(),
                    Description = eventViewModel.IndexEventViewModel.Description,
                    LayoutId = eventViewModel.LayoutId,
                });
        }

        public void UpdateEvent(EventViewModel eventViewModel)
        {
            this.eventService.UpdateEvent(new Event
            {
                Name = eventViewModel.IndexEventViewModel.Name,
                BeginDate = eventViewModel.IndexEventViewModel.GetBeginDate(),
                EndDate = eventViewModel.IndexEventViewModel.GetEndDate(),
                Description = eventViewModel.IndexEventViewModel.Description,
                LayoutId = eventViewModel.LayoutId,
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

        public AreaViewModel GetAreaViewModel(int areaId)
        {
            var areaCost = this.eventAreaService.GetEventArea(areaId).Price;
            return new AreaViewModel
            {
                EventAreaId = areaId,
                Price = areaCost,
            };
        }

        public void ChangeCost(AreaViewModel areaVm)
        {
            var area = this.eventAreaService.GetEventArea(areaVm.EventAreaId);
            area.Price = areaVm.Price;
            this.eventAreaService.UpdateEventArea(area);
        }

        private List<IndexEventViewModel> MapToIndexEventViewModel(List<Event> eventList)
        {
            var eventVMList = new List<IndexEventViewModel>();

            foreach (var eventDto in eventList)
            {
                var vm = new IndexEventViewModel
                {
                    Id = eventDto.Id,
                    BeginDate = eventDto.BeginDate,
                    BeginTime = eventDto.BeginDate,
                    Description = eventDto.Description,
                    EndDate = eventDto.EndDate,
                    EndTime = eventDto.EndDate,
                    LayoutId = eventDto.LayoutId,
                    Name = eventDto.Name,
                    Published = eventDto.Published,
                    LayoutName = this.layoutService.GetLayout(eventDto.LayoutId).Name,
                };

                eventVMList.Add(vm);
            }

            return eventVMList;
        }
    }
}