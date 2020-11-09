// ****************************************************************************
// <copyright file="IDataTimeHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;

namespace TicketManagement.BLL.Infrastructure.Helpers.Interfaces
{
    public interface IDataTimeHelper
    {
        DateTime GetDateTimeUtcNow();
    }
}
