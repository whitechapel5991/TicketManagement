// ****************************************************************************
// <copyright file="EventRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories
{
    internal class EventRepository : RepositoryBase<Event>
    {
        private const string IdParamName = "@Id";
        private const string NameParamName = "@Name";
        private const string DescriptionParamName = "@Description";
        private const string LayoutIdParamName = "@LayoutId";
        private const string BeginDateParamName = "@BeginDate";
        private const string EndDateParamName = "@EndDate";
        private const string PublishParamName = "@Published";

        public EventRepository(TicketManagementContext dbContext)
            : base(dbContext)
        {
        }

        public override int Create(Event entity)
        {
            const string CreateEventStoredProcedureName = "CreateEvent";

            var resultId = this.Context.Database.SqlQuery<int>(
                $"{CreateEventStoredProcedureName} {NameParamName}, {DescriptionParamName}, {LayoutIdParamName}, {BeginDateParamName}, {EndDateParamName}",
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId),
                new SqlParameter(BeginDateParamName, entity.BeginDate),
                new SqlParameter(EndDateParamName, entity.EndDate))
                .Single();

            this.Context.SaveChanges();
            return resultId;
        }

        public override void Update(Event entity)
        {
            const string UpdateEventStoredProcedureName = "UpdateEvent";

            this.Context.Database.ExecuteSqlCommand(
                $"{UpdateEventStoredProcedureName} {IdParamName}, {NameParamName}, {DescriptionParamName}, {LayoutIdParamName}, {BeginDateParamName}, {EndDateParamName}, {PublishParamName}",
                new SqlParameter(IdParamName, entity.Id),
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId),
                new SqlParameter(BeginDateParamName, entity.BeginDate) { SqlDbType = SqlDbType.DateTime },
                new SqlParameter(EndDateParamName, entity.EndDate) { SqlDbType = SqlDbType.DateTime },
                new SqlParameter(PublishParamName, entity.Published));

            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            const string DeleteEventStoredProcedureName = "DeleteEvent";

            this.Context.Database.ExecuteSqlCommand(
                $"{DeleteEventStoredProcedureName} {IdParamName}",
                new SqlParameter(IdParamName, id));
            this.Context.SaveChanges();
        }
    }
}
