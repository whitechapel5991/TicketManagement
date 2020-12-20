// ****************************************************************************
// <copyright file="EventManagerEventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.EventAreaService;
using TicketManagement.Web.EventSeatService;
using TicketManagement.Web.EventService;
using TicketManagement.Web.Interfaces;
using TicketManagement.Web.LayoutService;
using TicketManagement.Web.Models.Event;
using TicketManagement.Web.WcfInfrastructure;
using EventViewModel = TicketManagement.Web.Areas.EventManager.Data.EventViewModel;

namespace TicketManagement.Web.Services
{
    internal class EventManagerEventService : IEventManagerEventService
    {
        private readonly IImageService imageService;

        public EventManagerEventService(
            IImageService imageService)
        {
            this.imageService = imageService;
        }

        public List<IndexEventViewModel> GetIndexEventViewModels()
        {
            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                return this.MapToIndexEventViewModel(client.GetEvents().ToList());
            }
        }

        public EventViewModel GetEventViewModel()
        {
            using (var client = new LayoutContractClient())
            {
                client.AddClientCredentials();
                var layouts = client.GetLayouts().ToList();
                return new EventViewModel
                {
                    LayoutList = new SelectList(layouts, "Id", "Name", layouts.First()),
                };
            }
        }

        public EventViewModel GetEventViewModel(int eventId)
        {
            Event eventDto = null;
            Dictionary<int,string> layouts = null;

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                eventDto = client.GetEvent(eventId);
            }

            using (var client = new LayoutContractClient())
            {
                client.AddClientCredentials();
                layouts = client.GetLayouts().Distinct().ToDictionary(x => x.Id, x => x.Name);
            }

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
                    ImagePath = eventDto.ImageUrl,
                },
                LayoutId = eventDto.LayoutId,
                LayoutList = new SelectList(layouts.OrderBy(x => x.Value), "Key", "Value", layouts[eventDto.LayoutId]),
            };
        }

        public void CreateEvent(EventViewModel eventViewModel)
        {
            var imageUrl = string.Empty;
            if (eventViewModel.IndexEventViewModel.Image != null)
            {
                var uploadedFile = new byte[eventViewModel.IndexEventViewModel.Image.InputStream.Length];
                eventViewModel.IndexEventViewModel.Image.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                this.imageService.SaveImage(eventViewModel.IndexEventViewModel.Image.FileName, uploadedFile);
                imageUrl = this.imageService.GetImageUri(eventViewModel.IndexEventViewModel.Image.FileName);
            }

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                client.AddEvent(new Event
                {
                    Name = eventViewModel.IndexEventViewModel.Name,
                    BeginDateUtc = eventViewModel.IndexEventViewModel.GetBeginDate(),
                    EndDateUtc = eventViewModel.IndexEventViewModel.GetEndDate(),
                    Description = eventViewModel.IndexEventViewModel.Description,
                    LayoutId = eventViewModel.LayoutId,
                    ImageUrl = imageUrl,
                });
            }
        }

        public void UpdateEvent(EventViewModel eventViewModel)
        {
            var imageUrl = string.Empty;
            if (eventViewModel.IndexEventViewModel.Image != null)
            {
                var uploadedFile = new byte[eventViewModel.IndexEventViewModel.Image.InputStream.Length];
                eventViewModel.IndexEventViewModel.Image.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                this.imageService.SaveImage(eventViewModel.IndexEventViewModel.Image.FileName, uploadedFile);
                imageUrl = this.imageService.GetImageUri(eventViewModel.IndexEventViewModel.Image.FileName);
            }

            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                client.UpdateEvent(new Event
                {
                    Name = eventViewModel.IndexEventViewModel.Name,
                    BeginDateUtc = eventViewModel.IndexEventViewModel.GetBeginDate(),
                    EndDateUtc = eventViewModel.IndexEventViewModel.GetEndDate(),
                    Description = eventViewModel.IndexEventViewModel.Description,
                    LayoutId = eventViewModel.IndexEventViewModel.LayoutId,
                    Published = eventViewModel.IndexEventViewModel.Published,
                    Id = eventViewModel.IndexEventViewModel.Id,
                    ImageUrl = imageUrl,
                });
            }
        }

        public void DeleteEvent(int id)
        {
            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                client.DeleteEvent(id);
            }
        }

        public void PublishEvent(int id)
        {
            using (var client = new EventContractClient())
            {
                client.AddClientCredentials();
                client.PublishEvent(id);
            }
        }

        public AreaPriceViewModel GetAreaPriceViewModel(int areaId)
        {
            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                return new AreaPriceViewModel
                {
                    EventAreaId = areaId,
                    Price = client.GetEventArea(areaId).Price,
                };
            }
        }

        public void ChangeCost(AreaPriceViewModel areaPriceVm)
        {
            using (var client = new EventAreaContractClient())
            {
                client.AddClientCredentials();
                var area = client.GetEventArea(areaPriceVm.EventAreaId);
                area.Price = areaPriceVm.Price;
                client.UpdateEventArea(area);
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

        private List<IndexEventViewModel> MapToIndexEventViewModel(List<Event> eventList)
        {
            using (var client = new LayoutContractClient())
            {
                client.AddClientCredentials();
                var layouts = client.GetLayoutsByLayoutIds(eventList.Select(x => x.LayoutId).ToArray()).Distinct().ToDictionary(x => x.Id, x => x.Name);
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
                    ImagePath = eventDto.ImageUrl,
                }).ToList();
            }
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