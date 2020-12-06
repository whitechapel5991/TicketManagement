// ****************************************************************************
// <copyright file="WcfModule.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using Autofac;
using TicketManagement.WcfService.Services;

namespace TicketManagement.WcfService.Util
{
    public class WcfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventService>();
            builder.RegisterType<AreaService>();
            builder.RegisterType<EventAreaService>();
            builder.RegisterType<EventSeatService>();
            builder.RegisterType<LayoutService>();
            builder.RegisterType<OrderService>();
            builder.RegisterType<SeatService>();
            builder.RegisterType<VenueService>();
        }
    }
}