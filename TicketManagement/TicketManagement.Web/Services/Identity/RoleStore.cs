﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.Web.Services.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, int>, IQueryableRoleStore<IdentityRole, int>, IDisposable
    {
        private readonly IRoleService roleService;

        public RoleStore(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        #region IRoleStore<IdentityRole, int> Members
        public Task CreateAsync(IdentityRole role)
        {
            return Task.Run(() => this.roleService.Add(role.Name));
        }

        public Task DeleteAsync(IdentityRole role)
        {
            return Task.Run(() => this.roleService.Delete(role.Name));
        }

        public Task<IdentityRole> FindByIdAsync(int roleId)
        {
            return Task.Run(() => this.GetIdentityRole(this.roleService.FindById(roleId)));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return Task.Run(() => this.GetIdentityRole(this.roleService.FindByName(roleName)));
        }
        public Task UpdateAsync(IdentityRole role)
        {
            return Task.Run(() => this.roleService.Update(this.GetRole(role)));
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Autofac manage the lifecycle
        }
        #endregion

        #region IQueryableRoleStore<IdentityRole, int> Members
        public IQueryable<IdentityRole> Roles => this.roleService.GetAll().Select(x => this.GetIdentityRole(x));
        #endregion

        #region Mappers
        private IdentityRole GetIdentityRole(Role role) =>
            new IdentityRole
            {
                Id = role.Id,
                Name = role.Name,
            };

        private Role GetRole(IdentityRole identityRole) =>
            new Role
            {
                Id = identityRole.Id,
                Name = identityRole.Name,
            };
        #endregion
    }
}