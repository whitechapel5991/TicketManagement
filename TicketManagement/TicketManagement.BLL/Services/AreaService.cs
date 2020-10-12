// ****************************************************************************
// <copyright file="AreaService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> areaRepository;
        private readonly IAreaValidator areaValidator;

        public AreaService(IRepository<Area> areaRepository, IAreaValidator areaValidator)
        {
            this.areaRepository = areaRepository;
            this.areaValidator = areaValidator;
        }

        public int AddArea(Area areaDto)
        {
            this.areaValidator.Validation(areaDto);
            return this.areaRepository.Create(areaDto);
        }

        public void UpdateArea(Area areaDto)
        {
            this.areaValidator.Validation(areaDto);
            this.areaRepository.Update(areaDto);
        }

        public void DeleteArea(int id)
        {
            this.areaRepository.Delete(id);
        }

        public Area GetArea(int id)
        {
            return this.areaRepository.GetById(id);
        }

        public IEnumerable<Area> GetAreas()
        {
            return this.areaRepository.GetAll();
        }
    }
}
