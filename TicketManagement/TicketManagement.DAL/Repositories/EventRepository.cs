// ****************************************************************************
// <copyright file="EventRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TicketManagement.DAL.EFContext;
using TicketManagement.DAL.Extensions;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Models.Base;
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

        public EventRepository(TicketManagementContext dbContext)
            : base(dbContext)
        {
        }

        public override int Create(Event entity)
        {
            const string CreateEventStoredProcedureName = "CreateEvent";

            var @event = this.context.Database.SqlQuery<int>(
                $"{CreateEventStoredProcedureName} {NameParamName}, {DescriptionParamName}, {LayoutIdParamName}, {BeginDateParamName}, {EndDateParamName}",
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId),
                new SqlParameter(BeginDateParamName, entity.BeginDate),
                new SqlParameter(EndDateParamName, entity.EndDate))
                .FirstOrDefault();

            this.context.SaveChanges();
            return @event;
        }

        public override void Update(Event entity)
        {
            const string UpdateEventStoredProcedureName = "UpdateEvent";

            this.context.Database.SqlQuery<Event>(
                $"{UpdateEventStoredProcedureName} {IdParamName}, {NameParamName}, {BeginDateParamName}, {EndDateParamName}, {DescriptionParamName}, {LayoutIdParamName}",
                new SqlParameter(IdParamName, entity.Id),
                new SqlParameter(NameParamName, entity.Name),
                new SqlParameter(BeginDateParamName, entity.BeginDate),
                new SqlParameter(EndDateParamName, entity.EndDate),
                new SqlParameter(DescriptionParamName, entity.Description),
                new SqlParameter(LayoutIdParamName, entity.LayoutId));
            this.context.SaveChanges();
        }

        public override void Delete(int id)
        {
            const string DeleteEventStoredProcedureName = "DeleteEvent";

            this.context.Database.ExecuteSqlCommand(
                $"{DeleteEventStoredProcedureName} {IdParamName}",
                new SqlParameter(IdParamName, id));
            this.context.SaveChanges();
        }
    }
}
