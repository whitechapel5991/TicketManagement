// ****************************************************************************
// <copyright file="Repository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data.Entity;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models.Base;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories
{
    public class Repository<TDalEntity> : RepositoryBase<TDalEntity>
        where TDalEntity : class, IEntity, new()
    {
        public Repository(IGenerateDbContext contextGenerator)
            : base(contextGenerator)
        {
        }

        public override int Create(TDalEntity entity)
        {
            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Added;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
            return entity.Id;
        }

        public override void Update(TDalEntity entity)
        {
            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Modified;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.ContextGenerator.GenerateNewContext().Set<TDalEntity>().Find(id);
            if (entity == null)
            {
                return;
            }

            this.ContextGenerator.GenerateNewContext().Entry(entity).State = EntityState.Deleted;
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }

        public override TDalEntity GetById(int id)
        {
            return this.ContextGenerator.GenerateNewContext().Set<TDalEntity>().AsNoTracking().First(x => x.Id == id);
        }

        public override IQueryable<TDalEntity> GetAll()
        {
            return this.ContextGenerator.GenerateNewContext().Set<TDalEntity>().AsNoTracking();
        }
    }
}
