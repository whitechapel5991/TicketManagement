// ****************************************************************************
// <copyright file="UserClaimRepositoryFake.cs" company="EPAM Systems">
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
    internal class UserClaimRepositoryFake : IUserClaimRepository
    {
        private readonly List<UserClaim> userClaimRepositoryData;
        private readonly List<TicketManagementUser> userRepositoryData;

        public UserClaimRepositoryFake(List<UserClaim> userClaimRepositoryData, List<TicketManagementUser> userRepositoryData)
        {
            this.userClaimRepositoryData = userClaimRepositoryData;
            this.userRepositoryData = userRepositoryData;
        }

        public int Create(UserClaim entity)
        {
            this.userClaimRepositoryData.Add(entity);
            return entity.Id;
        }

        public void Delete(int id)
        {
            this.userClaimRepositoryData.Remove(this.userClaimRepositoryData.Find(x => x.Id == id));
        }

        public IQueryable<UserClaim> GetAll()
        {
            return this.userClaimRepositoryData.AsQueryable();
        }

        public UserClaim GetById(int id)
        {
            return this.userClaimRepositoryData.First(x => x.Id == id);
        }

        public IEnumerable<UserClaim> GetByUserId(int userId)
        {
            return this.userClaimRepositoryData.Where(x => x.UserId == userId);
        }

        public IEnumerable<TicketManagementUser> GetUsersForClaim(string claimType, string claimValue)
        {
            var userIdList = this.userClaimRepositoryData.Where(x => x.ClaimType == claimType && x.ClaimValue == claimValue).Select(x => x.UserId);
            return this.userRepositoryData.Join(userIdList, user => user.Id, id => id, (user, id) => user);
        }

        public void Update(UserClaim entity)
        {
            var index = this.userClaimRepositoryData.FindIndex(x => x.Id == entity.Id);
            if (index == -1)
            {
                return;
            }

            this.userClaimRepositoryData.RemoveAt(index);
            this.userClaimRepositoryData.Insert(index, entity);
        }
    }
}
