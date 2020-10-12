// ****************************************************************************
// <copyright file="EventValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Globalization;
using System.Linq;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.EventExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.ServiceValidators
{
    internal class EventValidator : IEventValidator
    {
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Layout> layoutRepository;
        private readonly IRepository<Area> areaRepository;
        private readonly IRepository<Seat> seatRepository;
        private readonly IRepository<EventArea> eventAreaRepository;
        private readonly IRepository<EventSeat> eventSeatRepository;

        public EventValidator(
            IRepository<Event> eventRepository,
            IRepository<EventArea> eventAreaRepository,
            IRepository<EventSeat> eventSeatRepository,
            IRepository<Layout> layoutRepository,
            IRepository<Area> areaRepository,
            IRepository<Seat> seatRepository)
        {
            this.eventRepository = eventRepository;
            this.eventAreaRepository = eventAreaRepository;
            this.eventSeatRepository = eventSeatRepository;
            this.layoutRepository = layoutRepository;
            this.areaRepository = areaRepository;
            this.seatRepository = seatRepository;
        }

        public void Validation(Event entity)
        {
            var layout = this.layoutRepository.GetById(entity.LayoutId);

            if (layout == default(Layout))
            {
                throw new EntityDoesNotExistException("Layout doesn't exist.");
            }

            var now = (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMinutes;
            var eventBeginDate = (long)TimeSpan.FromTicks(entity.BeginDate.Ticks).TotalMinutes;

            if (now > eventBeginDate)
            {
                throw new EventInPastException("Event can not be in the past.");
            }

            eventBeginDate = (long)TimeSpan.FromTicks(entity.BeginDate.Ticks).TotalMinutes;
            var eventEndDate = (long)TimeSpan.FromTicks(entity.EndDate.Ticks).TotalMinutes;

            if (eventBeginDate >= eventEndDate)
            {
                throw new BeginDateLongerThenEndDateException("Begin date cannot be longer than end date.");
            }

            if (this.EventInLayoutInTheSameTimeExist(
                entity.LayoutId,
                entity.BeginDate,
                entity.EndDate))
            {
                throw new EventExistInTheLayoutInThisTimeException($"Event in the layout={entity.LayoutId} exist in the same time");
            }

            if (!this.areaRepository.GetAll().Any(x => x.LayoutId == entity.LayoutId))
            {
                throw new LayoutHasNotAreaException($"Layout with id={entity.LayoutId} has not area.");
            }

            var isSeatInLayout = (from areasQ in this.areaRepository.GetAll().Where(x => x.LayoutId == entity.LayoutId).AsEnumerable()
                join seatsQ in this.seatRepository.GetAll().AsEnumerable() on areasQ.Id equals seatsQ.AreaId
                select new Seat { Id = seatsQ.Id, AreaId = seatsQ.AreaId, Number = seatsQ.Number, Row = seatsQ.Row }).Any();

            if (!isSeatInLayout)
            {
                throw new LayoutHasNotSeatException($"Layout with id={entity.LayoutId} has not seat.");
            }
        }

        public void UpdateValidation(Event entity)
        {
            this.Validation(entity);
            var @event = this.eventRepository.GetById(entity.Id);

            if (entity.LayoutId == @event.LayoutId)
            {
                return;
            }

            if (this.SoldSeatExist(entity.Id))
            {
                throw new LayoutHasSoldSeatAndCouldNotBeChangeException("Layout cannot be changed, because one or more ticket have been sold on this event.");
            }
        }

        public void DeleteValidation(int eventId)
        {
            var @event = this.eventRepository.GetById(eventId);

            if (this.SoldSeatExist(@event.Id))
            {
                throw new LayoutHasSoldSeatAndCouldNotBeChangeException("Event cannot be deleted, because one or more ticket have been sold on this event.");
            }
        }

        public void PublishValidation(Event @event)
        {
            if (@event.Published)
            {
                throw new EventAlreadyPublishedException("Event already published");
            }

            if (this.eventAreaRepository.GetAll().Any(x => x.Price == decimal.Zero && x.EventId == @event.Id))
            {
                throw new SomeAreaHasNotPriceException("All areas in event must have a price");
            }
        }

        private bool SoldSeatExist(int eventId)
        {
            return (from eventArea in this.eventAreaRepository.GetAll().Where(x => x.EventId == eventId)
                    join eventSeat in this.eventSeatRepository.GetAll() on eventArea.Id equals eventSeat.EventAreaId
                    where eventSeat.State != EventSeatState.Free
                    select new { eventSeat.State }).Any();
        }

        private bool EventInLayoutInTheSameTimeExist(int layoutId, DateTime beginDate, DateTime endDate)
        {
            var begin = Convert.ToDateTime(beginDate.ToString("dd.MM.yyyy HH:mm"), new CultureInfo("ru"));
            var end = Convert.ToDateTime(endDate.ToString("dd.MM.yyyy HH:mm"), new CultureInfo("ru"));

            return (from a in this.eventRepository.GetAll().Where(x => x.LayoutId == layoutId)
                    where (a.BeginDate <= begin && a.EndDate >= begin) ||
                          (a.BeginDate <= end && a.EndDate >= end) ||
                          (a.BeginDate >= begin && a.BeginDate <= end) ||
                          (a.EndDate >= begin && a.EndDate <= end)
                    select new { a.Id }).Any();
        }
    }
}
