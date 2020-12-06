// ****************************************************************************
// <copyright file="DalOrderModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalOrderModelExtension
    {
        public static Order ConvertToWcfOrder(this TicketManagement.DAL.Models.Order bllOrder)
        {
            return new Order()
            {
                Id = bllOrder.Id,
                UserId = bllOrder.UserId,
                EventSeatId = bllOrder.EventSeatId,
                DateUtc = bllOrder.DateUtc,
            };
        }
    }
}