using System;
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

        public IQueryable<IdentityRole> Roles => this.roleService.GetAll().Select(x => this.GetIdentityRole(x));

        public Task CreateAsync(IdentityRole role)
        {
            this.roleService.Add(role.Name);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IdentityRole role)
        {
            this.roleService.Delete(role.Name);
            return Task.CompletedTask;
        }

        public Task<IdentityRole> FindByIdAsync(int roleId)
        {
            return Task.FromResult(this.GetIdentityRole(this.roleService.FindById(roleId)));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult(this.GetIdentityRole(this.roleService.FindByName(roleName)));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            this.roleService.Update(this.GetRole(role));
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Autofac manage the lifecycle
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