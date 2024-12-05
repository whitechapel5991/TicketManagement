﻿// ****************************************************************************
// <copyright file="AreaValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.BLL.Exceptions.AreaExceptions;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.ServiceValidators
{
    internal class AreaValidator : IAreaValidator
    {
        private readonly IRepository<Area> areaRepository;
        private readonly IRepository<Layout> layoutRepository;

        public AreaValidator(IRepository<Area> areaRepository, IRepository<Layout> layoutRepository)
        {
            this.areaRepository = areaRepository;
            this.layoutRepository = layoutRepository;
        }

        public void Validate(Area entity)
        {
            var layout = this.layoutRepository.GetById(entity.LayoutId);
            if (layout == default(Layout))
            {
                throw new EntityDoesNotExistException($"Layout with id={entity.LayoutId} doesn't exist.");
            }

            if (this.AreaWithDescriptionExist(entity.Description, entity.LayoutId))
            {
                throw new AreaWithSameDescriptionInTheLayoutExistException($"Area with description={entity.Description} in the layout already exist.");
            }
        }

        private bool AreaWithDescriptionExist(string areaDescription, int layoutId)
        {
            return this.areaRepository.GetAll()
                .Where(x => x.LayoutId == layoutId && x.Description == areaDescription).Any();
        }
    }
}
