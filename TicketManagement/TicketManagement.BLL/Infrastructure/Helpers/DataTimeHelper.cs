// ****************************************************************************
// <copyright file="DataTimeHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;

namespace TicketManagement.BLL.Infrastructure.Helpers
{
    public class DataTimeHelper : IDataTimeHelper
    {
        public DateTime GetDateTimeUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
