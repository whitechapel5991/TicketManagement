// ****************************************************************************
// <copyright file="BllMapperProfile.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BLL.DTO;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Util
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            this.CreateMap<EventArea, EventAreaDto>();
            this.CreateMap<Event, EventDto>();
            this.CreateMap<EventSeat, EventSeatDto>();
            this.CreateMap<Area, AreaDto>();
            this.CreateMap<Layout, LayoutDto>();
            this.CreateMap<Seat, SeatDto>();
            this.CreateMap<Venue, VenueDto>().ReverseMap();
        }
    }
}
