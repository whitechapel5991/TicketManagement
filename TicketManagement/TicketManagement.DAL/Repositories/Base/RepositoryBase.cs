// ****************************************************************************
// <copyright file="RepositoryBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Repositories.Base
{
    public abstract class RepositoryBase<TDalEntity> : IRepository<TDalEntity>
        where TDalEntity : class, IEntity, new()
    {
        protected readonly TicketManagementContext context;
        protected DbSet<TDalEntity> dbSet;

        public RepositoryBase(TicketManagementContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TDalEntity>();
        }

        public abstract int Create(TDalEntity entity);

        public abstract void Update(TDalEntity entity);

        public abstract void Delete(int id);

        public virtual TDalEntity GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public virtual IQueryable<TDalEntity> GetAll()
        {
            return this.dbSet.AsNoTracking();
        }
    }
}
