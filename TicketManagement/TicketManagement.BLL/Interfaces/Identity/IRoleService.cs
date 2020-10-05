// ****************************************************************************
// <copyright file="IRoleService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.BLL.Interfaces.Identity
{
    public interface IRoleService
    {
        void Add(string roleName);

        void Delete(string roleName);

        Role FindById(int id);

        Role FindByName(string roleName);

        void Update(Role role);

        IQueryable<Role> GetAll();
    }
}
