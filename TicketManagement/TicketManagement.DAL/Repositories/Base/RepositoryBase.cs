// ****************************************************************************
// <copyright file="RepositoryBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data.Entity;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Repositories.Base
{
    public abstract class RepositoryBase<TDalEntity> : IRepository<TDalEntity>
        where TDalEntity : class, IEntity, new()
    {
        protected RepositoryBase(TicketManagementContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TDalEntity>();
        }

        protected TicketManagementContext Context { get; }

        protected DbSet<TDalEntity> DbSet { get; }

        public abstract int Create(TDalEntity entity);

        public abstract void Update(TDalEntity entity);

        public abstract void Delete(int id);

        public virtual TDalEntity GetById(int id)
        {
            return this.DbSet.AsNoTracking().First(x => x.Id == id);
        }

        public virtual IQueryable<TDalEntity> GetAll()
        {
            return this.DbSet.AsNoTracking();
        }
    }
}
