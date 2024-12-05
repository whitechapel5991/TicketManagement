// ****************************************************************************
// <copyright file="IAreaService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    internal interface IAreaService
    {
        IEnumerable<Area> GetAreas();

        Area GetArea(int id);

        int AddArea(Area entity);

        void UpdateArea(Area entity);

        void DeleteArea(int id);
    }
}
