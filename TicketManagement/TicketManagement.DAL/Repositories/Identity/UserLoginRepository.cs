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
        public UserLoginRepository(TicketManagementContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<UserLogin>();
        }

        private TicketManagementContext Context { get; set; }

        private DbSet<UserLogin> DbSet { get; set; }

        public UserLoginKey Create(UserLogin entity)
        {
            this.DbSet.Add(entity);
            this.Context.SaveChanges();
            return entity;
        }

        public void Delete(UserLoginKey id)
        {
            UserLogin entity = this.GetById(id);
            if (entity != null)
            {
                this.DbSet.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<UserLogin> FindByUserId(int userId)
        {
            return this.DbSet.Where(x => x.UserId == userId);
        }

        public IQueryable<UserLogin> GetAll()
        {
            return this.DbSet.AsNoTracking();
        }

        public UserLogin GetById(UserLoginKey id)
        {
            return this.DbSet.Find(id);
        }

        public void Update(UserLogin entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
    }
}
