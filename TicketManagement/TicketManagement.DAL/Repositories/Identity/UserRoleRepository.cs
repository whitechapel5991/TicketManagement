// ****************************************************************************
// <copyright file="UserRoleRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.DAL.Repositories.Identity
{
    internal class UserRoleRepository : IUserRoleRepository
    {
        public UserRoleRepository(TicketManagementContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<UserRole>();
        }

        private TicketManagementContext Context { get; set; }

        private DbSet<UserRole> DbSet { get; set; }

        public void Add(int userId, string roleName)
        {
            var roleId = this.Context.Roles.First(x => x.Name == roleName).Id;
            this.DbSet.Add(new UserRole() { RoleId = roleId, UserId = userId });
            this.Context.SaveChanges();
        }

        public IEnumerable<string> GetRoleNamesByUserId(int userId)
        {
            var roleIdList = this.DbSet.Where(x => x.UserId == userId).Select(x => x.RoleId);
            return this.Context.Roles.Join(roleIdList, role => role.Id, id => id, (role, id) => role.Name);
        }

        public IEnumerable<TicketManagementUser> GetUsersByRoleName(string roleName)
        {
            var roleId = this.Context.Roles.First(x => x.Name == roleName).Id;
            var userOdList = this.DbSet.Where(x => x.RoleId == roleId).Select(x => x.UserId);
            return this.Context.Users.Join(userOdList, user => user.Id, id => id, (user, id) => user);
        }

        public void Remove(int userId, string roleName)
        {
            var roleId = this.Context.Roles.First(x => x.Name == roleName).Id;
            var entity = this.DbSet.Find(new UserRole() { RoleId = roleId, UserId = userId });
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }
    }
}
