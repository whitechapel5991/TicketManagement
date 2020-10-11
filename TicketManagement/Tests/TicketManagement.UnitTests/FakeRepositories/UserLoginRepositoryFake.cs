// ****************************************************************************
// <copyright file="UserLoginRepositoryFake.cs" company="EPAM Systems">
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
    public class UserLoginRepositoryFake : IUserLoginRepository
    {
        private readonly List<UserLogin> repositoryData;

        public UserLoginRepositoryFake(List<UserLogin> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public UserLoginKey Create(UserLogin entity)
        {
            this.repositoryData.Add(entity);
            return new UserLoginKey
            {
                LoginProvider = entity.LoginProvider,
                ProviderKey = entity.ProviderKey,
            };
        }

        public void Delete(UserLoginKey id)
        {
            this.repositoryData.Remove(this.repositoryData
                .Find(x => x.ProviderKey == id.ProviderKey && x.LoginProvider == id.LoginProvider));
        }

        public IEnumerable<UserLogin> FindByUserId(int userId)
        {
            return this.repositoryData
                .Where(x => x.UserId == userId);
        }

        public IQueryable<UserLogin> GetAll()
        {
            return this.repositoryData.AsQueryable();
        }

        public UserLogin GetById(UserLoginKey id)
        {
            return this.repositoryData
                .First(x => x.ProviderKey == id.ProviderKey && x.LoginProvider == id.LoginProvider);
        }

        public void Update(UserLogin entity)
        {
            var index = this.repositoryData.FindIndex(x => x.ProviderKey == entity.ProviderKey && x.LoginProvider == entity.LoginProvider);
            if (index == -1)
            {
                return;
            }

            this.repositoryData.RemoveAt(index);
            this.repositoryData.Insert(index, entity);
        }
    }
}
