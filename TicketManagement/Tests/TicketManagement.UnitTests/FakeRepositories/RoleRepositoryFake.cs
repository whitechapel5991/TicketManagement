// ****************************************************************************
// <copyright file="RoleRepositoryFake.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.UnitTests.FakeRepositories
{
    public class RoleRepositoryFake : IRoleStore<Role, int>
    {
        private readonly List<Role> repositoryData;

        public RoleRepositoryFake(List<Role> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public Task CreateAsync(Role role)
        {
            this.repositoryData.Add(role);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Role role)
        {
            this.repositoryData.Add(role);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Empty
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            return Task.FromResult<Role>(this.repositoryData.First(x => x.Id == roleId));
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.FromResult<Role>(this.repositoryData.First(x => x.Name == roleName));
        }

        public Task UpdateAsync(Role role)
        {
            int index = this.repositoryData.FindIndex(x => x.Id == role.Id);
            if (index != -1)
            {
                this.repositoryData.RemoveAt(index);
                this.repositoryData.Insert(index, role);
            }

            return Task.CompletedTask;
        }
    }
}
