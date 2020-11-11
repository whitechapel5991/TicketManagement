// ****************************************************************************
// <copyright file="EventRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
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
        private const string ImageUrlParamName = "@ImageUrl";

        public EventRepository(IGenerateDbContext contextGenerator)
            : base(contextGenerator)
        {
        }

        public override int Create(Event entity)
        {
            const string createEventStoredProcedureName = "CreateEvent";

            var resultId = this.ContextGenerator.GenerateNewContext().Database.SqlQuery<int>(
                $"{createEventStoredProcedureName} {NameParamName}, {DescriptionParamName}, {LayoutIdParamName}, {BeginDateParamName}, {EndDateParamName}, {ImageUrlParamName}",
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId),
                new SqlParameter(BeginDateParamName, entity.BeginDateUtc.ToUniversalTime()),
                new SqlParameter(EndDateParamName, entity.EndDateUtc.ToUniversalTime()),
                new SqlParameter(ImageUrlParamName, (object)entity.ImageUrl ?? DBNull.Value))
                    .Single();
            this.ContextGenerator.GenerateNewContext().SaveChanges();
            return resultId;
        }

        public override void Update(Event entity)
        {
            const string updateEventStoredProcedureName = "UpdateEvent";

            this.ContextGenerator.GenerateNewContext().Database.ExecuteSqlCommand(
                $"{updateEventStoredProcedureName} {IdParamName}, {NameParamName}, {DescriptionParamName}, {LayoutIdParamName}, {BeginDateParamName}, {EndDateParamName}, {PublishParamName}, {ImageUrlParamName}",
                new SqlParameter(IdParamName, entity.Id),
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId),
                new SqlParameter(BeginDateParamName, entity.BeginDateUtc.ToUniversalTime()) { SqlDbType = SqlDbType.DateTime },
                new SqlParameter(EndDateParamName, entity.EndDateUtc.ToUniversalTime()) { SqlDbType = SqlDbType.DateTime },
                new SqlParameter(PublishParamName, entity.Published),
                new SqlParameter(ImageUrlParamName, (object)entity.ImageUrl ?? DBNull.Value));
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }

        public override void Delete(int id)
        {
            const string deleteEventStoredProcedureName = "DeleteEvent";

            this.ContextGenerator.GenerateNewContext().Database.ExecuteSqlCommand(
                $"{deleteEventStoredProcedureName} {IdParamName}",
                new SqlParameter(IdParamName, id));
            this.ContextGenerator.GenerateNewContext().SaveChanges();
        }
    }
}
