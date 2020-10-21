// ****************************************************************************
// <copyright file="ILayoutService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    public interface ILayoutService
    {
        IEnumerable<Layout> GetLayouts();

        Layout GetLayout(int id);

        int AddLayout(Layout entity);

        void UpdateLayout(Layout entity);

        void DeleteLayout(int id);

        IEnumerable<Layout> GetLayoutsByLayoutIds(int[] layoutIdArray);
    }
}
