// ****************************************************************************
// <copyright file="DalSeatModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalSeatModelExtension
    {
        public static Seat ConvertToWcfSeat(this TicketManagement.DAL.Models.Seat bllSeat)
        {
            return new Seat()
            {
                Id = bllSeat.Id,
                Row = bllSeat.Row,
                Number = bllSeat.Number,
                AreaId = bllSeat.AreaId,
            };
        }
    }
}