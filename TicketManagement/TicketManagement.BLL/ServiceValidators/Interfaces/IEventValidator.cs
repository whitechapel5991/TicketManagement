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
        void UpdateValidation(Event entity);

        void DeleteValidation(int eventId);

        void PublishValidation(Event @event);
    }
}
