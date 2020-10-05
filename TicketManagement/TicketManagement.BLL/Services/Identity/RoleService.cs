// ****************************************************************************
// <copyright file="RoleService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.BLL.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void Add(string roleName)
        {
            this.roleRepository.Create(new Role { Name = roleName });
        }

        public void Delete(string roleName)
        {
            this.roleRepository.Delete(this.roleRepository.FindByName(roleName).Id);
        }

        public Role FindById(int id)
        {
            return this.roleRepository.GetById(id);
        }

        public Role FindByName(string roleName)
        {
            return this.roleRepository.FindByName(roleName);
        }

        public void Update(Role role)
        {
            this.roleRepository.Update(role);
        }

        public IQueryable<Role> GetAll()
        {
            return this.roleRepository.GetAll();
        }
    }
}
