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
        public UserRoleRepository(IGenerateDbContext contextGenerator)
        {
            this.ContextGenerator = contextGenerator;
        }

        private IGenerateDbContext ContextGenerator { get; }

        public void Add(int userId, string roleName)
        {
            var roleId = this.ContextGenerator.GenerateNewContext().Roles.First(x => x.Name == roleName).Id;
            this.ContextGenerator.GenerateNewContext().Set<UserRole>().Add(new UserRole() { RoleId = roleId, UserId = userId });
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }

        public IEnumerable<string> GetRoleNamesByUserId(int userId)
        {
            var roleIdList = this.ContextGenerator.GenerateNewContext().Set<UserRole>().Where(x => x.UserId == userId).Select(x => x.RoleId);
            return this.ContextGenerator.GenerateNewContext().Roles.Join(roleIdList, role => role.Id, id => id, (role, id) => role.Name);
        }

        public IEnumerable<TicketManagementUser> GetUsersByRoleName(string roleName)
        {
            var roleId = this.ContextGenerator.GenerateNewContext().Roles.First(x => x.Name == roleName).Id;
            var userOdList = this.ContextGenerator.GenerateNewContext().Set<UserRole>().Where(x => x.RoleId == roleId).Select(x => x.UserId);
            return this.ContextGenerator.GenerateNewContext().Users.Join(userOdList, user => user.Id, id => id, (user, id) => user);
        }

        public void Remove(int userId, string roleName)
        {
            var roleId = this.ContextGenerator.GenerateNewContext().Roles.First(x => x.Name == roleName).Id;
            var entity = this.ContextGenerator.GenerateNewContext().Set<UserRole>().First(x => x.RoleId == roleId && x.UserId == userId);
            this.ContextGenerator.GenerateNewContext().Set<UserRole>().Remove(entity);
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }
    }
}
