// ****************************************************************************
// <copyright file="FilterConfig.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;
using TicketManagement.Web.Filters.AcionFilters;
using TicketManagement.Web.Filters.ExceptionFilters;

namespace TicketManagement.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UnknownExceptionFilter(), order: int.MinValue);
            filters.Add(new LogAttribute());
        }
    }
}