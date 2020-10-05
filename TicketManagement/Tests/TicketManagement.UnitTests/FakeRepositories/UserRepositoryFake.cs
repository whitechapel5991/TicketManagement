// ****************************************************************************
// <copyright file="UserRepositoryFake.cs" company="EPAM Systems">
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
    public class UserRepositoryFake : IUserRepository, IUserRoleRepository
    {
        private readonly List<TicketManagementUser> userRepositoryData;
        private readonly List<UserRole> userRoleRepositoryData;
        private readonly List<Role> roleRepositoryData;

        public UserRepositoryFake(
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

        public int Create(TicketManagementUser entity)
        {
            this.userRepositoryData.Add(entity);
            return entity.Id;
        }

        public void Delete(int id)
        {
            int index = this.userRepositoryData.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                this.userRepositoryData.RemoveAt(index);
            }
        }

        public TicketManagementUser FindByNormalizedEmail(string normalizedEmail)
        {
            return this.userRepositoryData.First(x => x.Email == normalizedEmail);
        }

        public TicketManagementUser FindByNormalizedUserName(string normalizedUserName)
        {
            return this.userRepositoryData.First(x => x.UserName == normalizedUserName);
        }

        public IQueryable<TicketManagementUser> GetAll()
        {
            return this.userRepositoryData.AsQueryable();
        }

        public TicketManagementUser GetById(int id)
        {
            return this.userRepositoryData.Find(x => x.Id == id);
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

        public void Update(TicketManagementUser entity)
        {
            var index = this.userRepositoryData.FindIndex(x => x.Id == entity.Id);
            if (index != -1)
            {
                this.userRepositoryData.RemoveAt(index);
                this.userRepositoryData.Insert(index, entity);
            }
        }
    }
}
