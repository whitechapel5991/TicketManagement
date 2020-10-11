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
    public class UserRepositoryFake : IUserRepository
    {
        private readonly List<TicketManagementUser> userRepositoryData;

        public UserRepositoryFake(List<TicketManagementUser> userRepositoryData)
        {
            this.userRepositoryData = userRepositoryData;
        }

        public int Create(TicketManagementUser entity)
        {
            this.userRepositoryData.Add(entity);
            return entity.Id;
        }

        public void Delete(int id)
        {
            var index = this.userRepositoryData.FindIndex(x => x.Id == id);
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

        public void Update(TicketManagementUser entity)
        {
            var index = this.userRepositoryData.FindIndex(x => x.Id == entity.Id);
            if (index == -1)
            {
                return;
            }

            this.userRepositoryData.RemoveAt(index);
            this.userRepositoryData.Insert(index, entity);
        }
    }
}
