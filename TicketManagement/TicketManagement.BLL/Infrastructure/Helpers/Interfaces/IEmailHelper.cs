// ****************************************************************************
// <copyright file="IEmailHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        void SendEmail(string recipient, string subject, string message);
    }
}
