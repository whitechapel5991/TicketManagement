// ****************************************************************************
// <copyright file="LayoutService.svc.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.WcfService.Contracts;
using TicketManagement.WcfService.Extensions;

namespace TicketManagement.WcfService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LayoutService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LayoutService.svc or LayoutService.svc.cs at the Solution Explorer and start debugging.
    public class LayoutService : ILayoutContract
    {
        private readonly ILayoutService layoutService;

        public LayoutService(ILayoutService layoutService)
        {
            this.layoutService = layoutService;
        }

        public int AddLayout(Layout entity)
        {
            return this.layoutService.AddLayout(entity.ConvertToBllLayout());
        }

        public void DeleteLayout(int id)
        {
            this.layoutService.DeleteLayout(id);
        }

        public Layout GetLayout(int id)
        {
            return this.layoutService.GetLayout(id).ConvertToWcfLayout();
        }

        public IEnumerable<Layout> GetLayouts()
        {
            return this.layoutService.GetLayouts().Select(entity => entity.ConvertToWcfLayout());
        }

        public IEnumerable<Layout> GetLayoutsByLayoutIds(int[] layoutIdArray)
        {
            return this.layoutService.GetLayoutsByLayoutIds(layoutIdArray).Select(entity => entity.ConvertToWcfLayout());
        }

        public void UpdateLayout(Layout entity)
        {
            this.layoutService.UpdateLayout(entity.ConvertToBllLayout());
        }
    }
}
