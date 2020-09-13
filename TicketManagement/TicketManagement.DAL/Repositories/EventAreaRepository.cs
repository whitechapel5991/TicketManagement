// ****************************************************************************
// <copyright file="EventAreaRepository.cs" company="EPAM Systems">
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
    internal class EventAreaRepository : RepositoryBase<EventArea>
    {
        private const string IdColumnName = "Id";
        private const string EventIdColumnName = "EventId";
        private const string PriceColumnName = "Price";
        private const string DescriptionColumnName = "Description";
        private const string CoordXColumnName = "CoordX";
        private const string CoordYColumnName = "CoordY";

        private const string CoordYParamName = "@CoordY";
        private const string CoordXParamName = "@CoordX";
        private const string PriceParamName = "@Price";
        private const string EventIdParamName = "@EventId";
        private const string DescriptionParamName = "@Description";
        private const string IdParamName = "@Id";

        public EventAreaRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(EventArea entity, IDbCommand cmd)
        {
            const string CreateEventAreaStoredProcedureName = "CreateEventArea";

            cmd.CommandText = CreateEventAreaStoredProcedureName;
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(CoordXParamName, entity.CoordX);
            cmd.AddParameterWithValue(CoordYParamName, entity.CoordY);
            cmd.AddParameterWithValue(PriceParamName, entity.Price);
            cmd.AddParameterWithValue(EventIdParamName, entity.EventId);
        }

        protected override void UpdateCommandParameters(EventArea entity, IDbCommand cmd)
        {
            const string UpdateEventAreaStoredProcedureName = "UpdateEventArea";

            cmd.CommandText = UpdateEventAreaStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(CoordXParamName, entity.CoordX);
            cmd.AddParameterWithValue(CoordYParamName, entity.CoordY);
            cmd.AddParameterWithValue(PriceParamName, entity.Price);
            cmd.AddParameterWithValue(EventIdParamName, entity.EventId);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteEventAreaStoredProcedureName = "DeleteEventArea";

            cmd.CommandText = DeleteEventAreaStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{EventIdColumnName}," +
                $"{DescriptionColumnName},{CoordXColumnName},{CoordYColumnName},{PriceColumnName} " +
                $"FROM EventAreas WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{EventIdColumnName}," +
                $"{DescriptionColumnName},{CoordXColumnName},{CoordYColumnName},{PriceColumnName} " +
                $"from EventAreas";
        }

        protected override EventArea Map(IDataReader reader)
        {
            var eventArea = new EventArea();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var eventIdColumnIndex = reader.GetOrdinal(EventIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var coordXColumnIndex = reader.GetOrdinal(CoordXColumnName);
            var coordYColumnIndex = reader.GetOrdinal(CoordYColumnName);
            var priceColumnIndex = reader.GetOrdinal(PriceColumnName);

            while (reader.Read())
            {
                eventArea.Id = reader.GetInt32(idColumnIndex);
                eventArea.EventId = reader.GetInt32(eventIdColumnIndex);
                eventArea.Description = reader.GetString(descriptionColumnIndex);
                eventArea.CoordX = reader.GetInt32(coordXColumnIndex);
                eventArea.CoordY = reader.GetInt32(coordYColumnIndex);
                eventArea.Price = reader.GetDecimal(priceColumnIndex);
            }

            return eventArea;
        }

        protected override IEnumerable<EventArea> Maps(IDataReader reader)
        {
            var eventAreas = new List<EventArea>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var eventIdColumnIndex = reader.GetOrdinal(EventIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var coordXColumnIndex = reader.GetOrdinal(CoordXColumnName);
            var coordYColumnIndex = reader.GetOrdinal(CoordYColumnName);
            var priceColumnIndex = reader.GetOrdinal(PriceColumnName);

            while (reader.Read())
            {
                var eventArea = new EventArea
                {
                    Id = reader.GetInt32(idColumnIndex),
                    EventId = reader.GetInt32(eventIdColumnIndex),
                    Description = reader.GetString(descriptionColumnIndex),
                    CoordX = reader.GetInt32(coordXColumnIndex),
                    CoordY = reader.GetInt32(coordYColumnIndex),
                    Price = reader.GetDecimal(priceColumnIndex),
                };
                eventAreas.Add(eventArea);
            }

            return eventAreas;
        }
    }
}
