// ****************************************************************************
// <copyright file="IEventValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.BLL.ServiceValidators.Base;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.ServiceValidators.Interfaces
{
    public interface IEventValidator : IServiceValidator<Event>
    {
        void UpdateValidate(Event entity);

        void DeleteValidate(int eventId);

        void PublishValidate(Event @event);
    }
}
