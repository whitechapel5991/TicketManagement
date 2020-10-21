// ****************************************************************************
// <copyright file="LayoutService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IRepository<Layout> layoutRepository;
        private readonly ILayoutValidator layoutValidator;

        public LayoutService(
            IRepository<Layout> layoutRepository,
            ILayoutValidator layoutValidator)
        {
            this.layoutRepository = layoutRepository;
            this.layoutValidator = layoutValidator;
        }

        public int AddLayout(Layout layoutDto)
        {
            this.layoutValidator.Validation(layoutDto);
            return this.layoutRepository.Create(layoutDto);
        }

        public void UpdateLayout(Layout layoutDto)
        {
            this.layoutValidator.Validation(layoutDto);
            this.layoutRepository.Update(layoutDto);
        }

        public void DeleteLayout(int id)
        {
            this.layoutRepository.Delete(id);
        }

        public Layout GetLayout(int id)
        {
            return this.layoutRepository.GetById(id);
        }

        public IEnumerable<Layout> GetLayouts()
        {
            return this.layoutRepository.GetAll();
        }

        public IEnumerable<Layout> GetLayoutsByLayoutIds(int[] layoutIdArray)
        {
            return this.layoutRepository.GetAll().Where(x => layoutIdArray.Contains(x.Id));
        }
    }
}
