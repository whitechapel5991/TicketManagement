// ****************************************************************************
// <copyright file="UserRepositoryFake.cs" company="EPAM Systems">
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
    public class UserRepositoryFake : IQueryableUserStore<TicketManagementUser, int>
    {
        private readonly List<TicketManagementUser> repositoryData;

        public UserRepositoryFake(List<TicketManagementUser> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public IQueryable<TicketManagementUser> Users => this.repositoryData.AsQueryable();

        public Task CreateAsync(TicketManagementUser user)
        {
            this.repositoryData.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TicketManagementUser user)
        {
            this.repositoryData.Add(user);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Empty
        }

        public Task<TicketManagementUser> FindByIdAsync(int userId)
        {
            return Task.FromResult<TicketManagementUser>(this.repositoryData.First(x => x.Id == userId));
        }

        public Task<TicketManagementUser> FindByNameAsync(string userName)
        {
            return Task.FromResult<TicketManagementUser>(this.repositoryData.First(x => x.UserName == userName));
        }

        public Task UpdateAsync(TicketManagementUser user)
        {
            int index = this.repositoryData.FindIndex(x => x.Id == user.Id);
            if (index != -1)
            {
                this.repositoryData.RemoveAt(index);
                this.repositoryData.Insert(index, user);
            }

            return Task.CompletedTask;
        }
    }
}
