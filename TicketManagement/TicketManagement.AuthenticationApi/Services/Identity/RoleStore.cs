// ****************************************************************************
// <copyright file="RoleStore.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.AuthenticationApi.Services.Identity
{
    public class RoleStore : IRoleStore<IdentityRole>
    {
        private readonly IRoleService roleService;

        public RoleStore(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public void Dispose()
        {
            // Autofac manage the lifecycle
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.roleService.Add(role.Name);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.roleService.Delete(role.Name);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(roleId))
            {
                roleId = roleId.ToLower();
            }

            cancellationToken.ThrowIfCancellationRequested();
            if (roleId == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            return Task.FromResult(this.GetIdentityRole(this.roleService.FindById(Convert.ToInt32(roleId))));
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.GetIdentityRole(this.roleService.FindByName(normalizedRoleName)));
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.roleService.FindById(role.Id).Name);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.roleService.FindById(role.Id).Id.ToString());
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.roleService.FindById(role.Id).Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.roleService.Update(this.GetRole(role));
            return Task.FromResult(IdentityResult.Success);
        }

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
    }
}