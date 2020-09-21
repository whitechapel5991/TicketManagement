using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.dbSet.Add(entity);
            this.context.SaveChanges();
            return entity.Id;
        }

        public override void Update(TDalEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public override void Delete(int id)
        {
            TDalEntity area = this.GetById(id);
            if (area != null)
            {
                this.dbSet.Remove(area);
                this.context.SaveChanges();
            }
        }

        public override TDalEntity GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public override IQueryable<TDalEntity> GetAll()
        {
            return this.dbSet.AsNoTracking();
        }
    }
}
