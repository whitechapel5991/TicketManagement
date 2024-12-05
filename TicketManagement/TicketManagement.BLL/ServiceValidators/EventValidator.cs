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

        public EventValidator(IRepository<Event> eventRepository, IRepository<EventArea> eventAreaRepository, IRepository<EventSeat> eventSeatRepository, IRepository<Layout> layoutRepository, IRepository<Area> areaRepository, IRepository<Seat> seatRepository)
        {
            this.eventRepository = eventRepository;
            this.eventAreaRepository = eventAreaRepository;
            this.eventSeatRepository = eventSeatRepository;
            this.layoutRepository = layoutRepository;
            this.areaRepository = areaRepository;
            this.seatRepository = seatRepository;
        }

        public void Validate(Event entity)
        {
            var layout = this.layoutRepository.GetById(entity.LayoutId);

            if (layout == default(Layout))
            {
                throw new EntityDoesNotExistException("Layout doesn't exist.");
            }

            if (this.IsDateInPast(entity.BeginDate))
            {
                throw new EventInPastException("Event can not be in the past.");
            }

            if (this.IsBeginDateLongerThenEndDate(entity.BeginDate, entity.EndDate))
            {
                throw new BeginDateLongerThenEndDateException("Begin date cannot be longer than end date.");
            }

            if (this.EventInLayoutInTheSameTimeExist(
                entity.LayoutId,
                entity.BeginDate.ToString("dd.MM.yyyy HH:mm"),
                entity.EndDate.ToString("dd.MM.yyyy HH:mm")))
            {
                throw new EventExistInTheLayoutInThisTimeException($"Event in the layout={entity.LayoutId} exist in the same time");
            }

            if (!this.AreaInLayoutExist(entity.LayoutId))
            {
                throw new LayoutHasNotAreaException($"Layout with id={entity.LayoutId} has not area.");
            }

            if (!this.SeatInLayoutExist(entity.LayoutId))
            {
                throw new LayoutHasNotSeatException($"Layout with id={entity.LayoutId} has not seat.");
            }
        }

        public void UpdateValidate(Event entity)
        {
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

        public void DeleteValidate(int eventId)
        {
            var @event = this.eventRepository.GetById(eventId);

            if (this.SoldSeatExist(@event.Id))
            {
                throw new LayoutHasSoldSeatAndCouldNotBeChangeException("Event cannot be deleted, because one or more ticket have been sold on this event.");
            }
        }

        private bool SoldSeatExist(int eventId)
        {
            return (from eventArea in this.eventAreaRepository.GetAll().Where(x => x.EventId == eventId)
                    join eventSeat in this.eventSeatRepository.GetAll() on eventArea.Id equals eventSeat.EventAreaId
                    where eventSeat.State != EventSeatState.Free
                    select new { eventSeat.State }).Any();
        }

        private bool AreaInLayoutExist(int layoutId)
        {
            return this.areaRepository.GetAll().Where(x => x.LayoutId == layoutId).Any();
        }

        private bool SeatInLayoutExist(int layoutId)
        {
            return (from areasQ in this.areaRepository.GetAll().Where(x => x.LayoutId == layoutId)
                join seatsQ in this.seatRepository.GetAll() on areasQ.Id equals seatsQ.AreaId
                select new Seat { Id = seatsQ.Id, AreaId = seatsQ.AreaId, Number = seatsQ.Number, Row = seatsQ.Row }).Any();
        }

        private bool IsDateInPast(DateTime dateTime)
        {
            DateTime dt = DateTime.Now;
            long now = (long)TimeSpan.FromTicks(dt.Ticks).TotalMinutes;
            long begin = (long)TimeSpan.FromTicks(dateTime.Ticks).TotalMinutes;
            return now > begin;
        }

        private bool IsBeginDateLongerThenEndDate(DateTime beginDate, DateTime endDate)
        {
            long begin = (long)TimeSpan.FromTicks(beginDate.Ticks).TotalMinutes;
            long end = (long)TimeSpan.FromTicks(endDate.Ticks).TotalMinutes;
            return begin >= end;
        }

        private bool EventInLayoutInTheSameTimeExist(int layoutId, string beginDate, string endDate)
        {
            DateTime begin = Convert.ToDateTime(beginDate, new CultureInfo("ru"));
            DateTime end = Convert.ToDateTime(endDate, new CultureInfo("ru"));

            var seatsInAreasQvery =
                from a in this.eventRepository.GetAll().Where(x => x.LayoutId == layoutId)
                where (a.BeginDate <= begin && a.EndDate >= begin) ||
                          (a.BeginDate <= end && a.EndDate >= end) ||
                      (a.BeginDate >= begin && a.BeginDate <= end) ||
                          (a.EndDate >= begin && a.EndDate <= end)
                select new { a.Id };
            var events = seatsInAreasQvery.Any();
            return events;
        }
    }
}
