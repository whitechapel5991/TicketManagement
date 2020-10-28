// ****************************************************************************
// <copyright file="UserLoginRepository.cs" company="EPAM Systems">
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
    internal class UserLoginRepository : IUserLoginRepository
    {
        public UserLoginRepository(IGenerateDbContext contextGenerator)
        {
            this.ContextGenerator = contextGenerator;
        }

        private IGenerateDbContext ContextGenerator { get; }

        public UserLoginKey Create(UserLogin entity)
        {
            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Added;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
            return entity;
        }

        public void Delete(UserLoginKey id)
        {
            var entity = this.GetById(id);
            if (entity == null)
            {
                return;
            }

            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Deleted;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }

        public IEnumerable<UserLogin> FindByUserId(int userId)
        {
            return this.ContextGenerator.GenerateNewContext().Set<UserLogin>().Where(x => x.UserId == userId);
        }

        public IQueryable<UserLogin> GetAll()
        {
            return this.ContextGenerator.GenerateNewContext().Set<UserLogin>().AsNoTracking();
        }

        public UserLogin GetById(UserLoginKey id)
        {
            return this.ContextGenerator.GenerateNewContext().Set<UserLogin>().First(x => x.LoginProvider == id.LoginProvider && x.ProviderKey == id.ProviderKey);
        }

        public void Update(UserLogin entity)
        {
            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Modified;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }
    }
}
