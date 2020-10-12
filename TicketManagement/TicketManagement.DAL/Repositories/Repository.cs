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
        public Repository(TicketManagementContext context)
            : base(context)
        {
        }

        public override int Create(TDalEntity entity)
        {
            this.DbSet.Add(entity);
            this.Context.SaveChanges();
            return entity.Id;
        }

        public override void Update(TDalEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.GetById(id);
            if (entity == null)
            {
                return;
            }

            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        public override TDalEntity GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public override IQueryable<TDalEntity> GetAll()
        {
            return this.DbSet.AsNoTracking();
        }
    }
}
