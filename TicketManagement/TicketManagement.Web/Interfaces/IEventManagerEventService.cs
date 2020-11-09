// ****************************************************************************
// <copyright file="IEventManagerEventService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.Web.Areas.EventManager.Data;
using TicketManagement.Web.Models.Event;
using EventViewModel = TicketManagement.Web.Areas.EventManager.Data.EventViewModel;

namespace TicketManagement.Web.Interfaces
{
    public interface IEventManagerEventService
    {
        List<IndexEventViewModel> GetIndexEventViewModels();
        EventViewModel GetEventViewModel();
        void CreateEvent(EventViewModel eventViewModel);
        EventViewModel GetEventViewModel(int eventId);
        void UpdateEvent(EventViewModel eventViewModel);
        void DeleteEvent(int id);
        void PublishEvent(int id);
        AreaPriceViewModel GetAreaPriceViewModel(int areaId);
        void ChangeCost(AreaPriceViewModel areaPriceVm);
        EventDetailViewModel GetEventDetailViewModel(int eventId);
        EventAreaDetailViewModel GetEventAreaDetailViewModel(int eventAreaId);
    }
}
