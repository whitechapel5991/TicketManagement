// ****************************************************************************
// <copyright file="AutomapperExtensions.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using AutoMapper;

namespace TicketManagement.BLL.Extensions
{
    internal static class AutomapperExtensions
    {
        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            return mapper.Map(item2, mapper.Map<TResult>(item1));
        }
    }
}
