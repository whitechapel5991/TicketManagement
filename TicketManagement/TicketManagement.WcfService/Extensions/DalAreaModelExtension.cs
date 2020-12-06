// ****************************************************************************
// <copyright file="DalAreaModelExtension.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.WcfService.Contracts;

namespace TicketManagement.WcfService.Extensions
{
    public static class DalAreaModelExtension
    {
        public static Area ConvertToWcfArea(this TicketManagement.DAL.Models.Area bllArea)
        {
            return new Area()
            {
                Id = bllArea.Id,
                Description = bllArea.Description,
                CoordinateX = bllArea.CoordinateX,
                CoordinateY = bllArea.CoordinateY,
                LayoutId = bllArea.LayoutId,
            };
        }
    }
}