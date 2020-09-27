// ****************************************************************************
// <copyright file="EventSeatServiceTests.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using Autofac;
using NUnit.Framework;
using TicketManagement.BLL.Interfaces;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Models;
using Test = TicketManagement.IntegrationTests.TestBase.TestBase;

namespace TicketManagement.IntegrationTests.ServiceTests
{
    [TestFixture]
    internal class EventSeatServiceTests : Test
    {
        private IEventSeatService eventSeatService;

        [SetUp]
        public void Init()
        {
            this.eventSeatService = this.Container.Resolve<IEventSeatService>();
        }

        [Test]
        public void UpdateEventSeat_NewState_GetEventSeat()
        {
            EventSeat eventSeatDto = new EventSeat
            {
                Id = 1,
                Number = 2,
                State = EventSeatState.Sold,
                Row = 2,
                EventAreaId = 2,
            };

            this.eventSeatService.UpdateEventSeat(eventSeatDto);

            EventSeat eventSeatDtoTemp = this.eventSeatService.GetEventSeat(1);

            Assert.AreEqual(2, eventSeatDtoTemp.Number);
            Assert.AreEqual(EventSeatState.Sold, eventSeatDtoTemp.State);
            Assert.AreEqual(2, eventSeatDtoTemp.Row);
            Assert.AreEqual(2, eventSeatDtoTemp.EventAreaId);
        }

        [Test]
        public void GetEventSeat_EventSeatId_GetEventSeat()
        {
            EventSeat eventSeatDtoTemp = this.eventSeatService.GetEventSeat(1);

            Assert.AreEqual(1, eventSeatDtoTemp.Number);
            Assert.AreEqual(EventSeatState.Free, eventSeatDtoTemp.State);
            Assert.AreEqual(1, eventSeatDtoTemp.Row);
            Assert.AreEqual(1, eventSeatDtoTemp.EventAreaId);
        }

        [Test]
        public void GetEventSeats_GetEventSeatsCount()
        {
            int eventSeatDtoCount = this.eventSeatService.GetEventSeats().Count();

            Assert.AreEqual(20, eventSeatDtoCount);
        }
    }
}
