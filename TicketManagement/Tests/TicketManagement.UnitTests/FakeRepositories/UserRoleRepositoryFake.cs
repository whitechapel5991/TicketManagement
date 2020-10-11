// ****************************************************************************
// <copyright file="UserRoleRepositoryFake.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.UnitTests.FakeRepositories
{
    public class UserRoleRepositoryFake : IUserRoleRepository
    {
        private readonly List<TicketManagementUser> userRepositoryData;
        private readonly List<UserRole> userRoleRepositoryData;
        private readonly List<Role> roleRepositoryData;

        public UserRoleRepositoryFake(
            List<TicketManagementUser> userRepositoryData,
            List<UserRole> userRoleRepositoryData,
            List<Role> roleRepositoryData)
        {
            this.userRepositoryData = userRepositoryData;
            this.userRoleRepositoryData = userRoleRepositoryData;
            this.roleRepositoryData = roleRepositoryData;
        }

        public void Add(int userId, string roleName)
        {
            var roleId = this.roleRepositoryData.First(x => x.Name == roleName).Id;
            this.userRoleRepositoryData.Add(new UserRole() { RoleId = roleId, UserId = userId });
        }

        public IEnumerable<string> GetRoleNamesByUserId(int userId)
        {
            var roleIdList = this.userRoleRepositoryData.Where(x => x.UserId == userId).Select(x => x.RoleId);
            return this.roleRepositoryData.Join(roleIdList, role => role.Id, id => id, (role, id) => role.Name);
        }

        public IEnumerable<TicketManagementUser> GetUsersByRoleName(string roleName)
        {
            var roleId = this.roleRepositoryData.First(x => x.Name == roleName).Id;
            var userIdList = this.userRoleRepositoryData.Where(x => x.RoleId == roleId).Select(x => x.UserId);
            return this.userRepositoryData.Join(userIdList, user => user.Id, id => id, (user, id) => user);
        }

        public void Remove(int userId, string roleName)
        {
            var roleId = this.roleRepositoryData.First(x => x.Name == roleName).Id;
            var index = this.userRoleRepositoryData.FindIndex(x => x.RoleId == roleId && x.UserId == userId);
            if (index != -1)
            {
                this.userRoleRepositoryData.RemoveAt(index);
            }
        }
    }
}
