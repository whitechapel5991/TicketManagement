// ****************************************************************************
// <copyright file="RepositoryBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Repositories.Base
{
    public abstract class RepositoryBase<TDalEntity> : IRepository<TDalEntity>
        where TDalEntity : class, IEntity, new()
    {
        protected RepositoryBase(IGenerateDbContext contextGenerator)
        {
            this.ContextGenerator = contextGenerator;
        }

        protected IGenerateDbContext ContextGenerator { get; }

        public abstract int Create(TDalEntity entity);

        public abstract void Update(TDalEntity entity);

        public abstract void Delete(int id);

        public virtual TDalEntity GetById(int id)
        {
            return this.ContextGenerator.GenerateNewContext().Set<TDalEntity>().AsNoTracking().First(x => x.Id == id);
        }

        public virtual IQueryable<TDalEntity> GetAll()
        {
            return this.ContextGenerator.GenerateNewContext().Set<TDalEntity>().AsNoTracking();
        }
    }
}
