// ****************************************************************************
// <copyright file="EventRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Data;
using TicketManagement.DAL.Extensions;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories
{
    internal class EventRepository : RepositoryBase<Event>
    {
        private const string IdColumnName = "Id";
        private const string NameColumnName = "Name";
        private const string DescriptionColumnName = "Description";
        private const string LayoutIdColumnName = "LayoutId";
        private const string BeginDateColumnName = "BeginDate";
        private const string EndDateColumnName = "EndDate";

        private const string IdParamName = "@Id";
        private const string NameParamName = "@Name";
        private const string DescriptionParamName = "@Description";
        private const string LayoutIdParamName = "@LayoutId";
        private const string BeginDateParamName = "@BeginDate";
        private const string EndDateParamName = "@EndDate";

        public EventRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(Event entity, IDbCommand cmd)
        {
            const string CreateEventStoredProcedureName = "CreateEvent";

            cmd.CommandText = CreateEventStoredProcedureName;
            cmd.AddParameterWithValue(NameParamName, entity.Name);
            cmd.AddParameterWithValue(BeginDateParamName, entity.BeginDate);
            cmd.AddParameterWithValue(EndDateParamName, entity.EndDate);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(LayoutIdParamName, entity.LayoutId);
        }

        protected override void UpdateCommandParameters(Event entity, IDbCommand cmd)
        {
            const string UpdateEventStoredProcedureName = "UpdateEvent";

            cmd.CommandText = UpdateEventStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(NameParamName, entity.Name);
            cmd.AddParameterWithValue(BeginDateParamName, entity.BeginDate);
            cmd.AddParameterWithValue(EndDateParamName, entity.EndDate);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(LayoutIdParamName, entity.LayoutId);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteEventStoredProcedureName = "DeleteEvent";

            cmd.CommandText = DeleteEventStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{NameColumnName},{DescriptionColumnName}," +
                $"{LayoutIdColumnName},{BeginDateColumnName},{EndDateColumnName} " +
                $"FROM Events WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{NameColumnName},{DescriptionColumnName}," +
                $"{LayoutIdColumnName},{BeginDateColumnName},{EndDateColumnName} " +
                $"from Events";
        }

        protected override Event Map(IDataReader reader)
        {
            var @event = new Event();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var layoutIdColumnIndex = reader.GetOrdinal(LayoutIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var beginDateColumnIndex = reader.GetOrdinal(BeginDateColumnName);
            var endDateColumnIndex = reader.GetOrdinal(EndDateColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);

            while (reader.Read())
            {
                @event.Id = reader.GetInt32(idColumnIndex);
                @event.Name = reader.GetString(nameColumnIndex);
                @event.Description = reader.GetString(descriptionColumnIndex);
                @event.LayoutId = reader.GetInt32(layoutIdColumnIndex);
                @event.BeginDate = reader.GetDateTime(beginDateColumnIndex);
                @event.EndDate = reader.GetDateTime(endDateColumnIndex);
            }

            return @event;
        }

        protected override IEnumerable<Event> Maps(IDataReader reader)
        {
            var events = new List<Event>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var layoutIdColumnIndex = reader.GetOrdinal(LayoutIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var beginDateColumnIndex = reader.GetOrdinal(BeginDateColumnName);
            var endDateColumnIndex = reader.GetOrdinal(EndDateColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);

            while (reader.Read())
            {
                var @event = new Event
                {
                    Id = reader.GetInt32(idColumnIndex),
                    Name = reader.GetString(nameColumnIndex),
                    Description = reader.GetString(descriptionColumnIndex),
                    LayoutId = reader.GetInt32(layoutIdColumnIndex),
                    BeginDate = reader.GetDateTime(beginDateColumnIndex),
                    EndDate = reader.GetDateTime(endDateColumnIndex),
                };
                events.Add(@event);
            }

            return events;
        }
    }
}
