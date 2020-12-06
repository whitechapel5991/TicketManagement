// ****************************************************************************
// <copyright file="AreaService.svc.cs" company="EPAM Systems">
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AreaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AreaService.svc or AreaService.svc.cs at the Solution Explorer and start debugging.
    public class AreaService : IAreaContract
    {
        private readonly IAreaService areaService;

        public AreaService(IAreaService areaService)
        {
            this.areaService = areaService;
        }

        public int AddArea(Area entity)
        {
            return this.areaService.AddArea(entity.ConvertToBllArea());
        }

        public void DeleteArea(int id)
        {
            this.areaService.DeleteArea(id);
        }

        public Area GetArea(int id)
        {
            return this.areaService.GetArea(id).ConvertToWcfArea();
        }

        public IEnumerable<Area> GetAreas()
        {
            return this.areaService.GetAreas().Select(entity => entity.ConvertToWcfArea());
        }

        public void UpdateArea(Area entity)
        {
            this.areaService.UpdateArea(entity.ConvertToBllArea());
        }
    }
}
