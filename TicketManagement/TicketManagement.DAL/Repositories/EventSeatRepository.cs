// ****************************************************************************
// <copyright file="EventSeatRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Data;
using TicketManagement.DAL.Constants;
using TicketManagement.DAL.Extensions;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.DAL.Repositories
{
    internal class EventSeatRepository : RepositoryBase<EventSeat>
    {
        private const string IdColumnName = "Id";
        private const string EventAreaIdColumnName = "EventAreaId";
        private const string RowColumnName = "Row";
        private const string NumberColumnName = "Number";
        private const string StateColumnName = "State";

        private const string IdParamName = "@Id";
        private const string EventAreaIdParamName = "@EventAreaId";
        private const string RowParamName = "@Row";
        private const string NumberParamName = "@Number";
        private const string StateParamName = "@State";

        public EventSeatRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(EventSeat entity, IDbCommand cmd)
        {
            const string CreateEventSeatStoredProcedureName = "CreateEventSeat";

            cmd.CommandText = CreateEventSeatStoredProcedureName;
            cmd.AddParameterWithValue(RowParamName, entity.Row);
            cmd.AddParameterWithValue(NumberParamName, entity.Number);
            cmd.AddParameterWithValue(StateParamName, entity.State);
            cmd.AddParameterWithValue(EventAreaIdParamName, entity.EventAreaId);
        }

        protected override void UpdateCommandParameters(EventSeat entity, IDbCommand cmd)
        {
            const string UpdateEventSeatStoredProcedureName = "UpdateEventSeat";

            cmd.CommandText = UpdateEventSeatStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(RowParamName, entity.Row);
            cmd.AddParameterWithValue(NumberParamName, entity.Number);
            cmd.AddParameterWithValue(StateParamName, entity.State);
            cmd.AddParameterWithValue(EventAreaIdParamName, entity.EventAreaId);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteEventSeatStoredProcedureName = "DeleteEventSeat";

            cmd.CommandText = DeleteEventSeatStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{EventAreaIdColumnName}," +
                $"{RowColumnName},{NumberColumnName},{StateColumnName} " +
                $"FROM EventSeats WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{EventAreaIdColumnName}," +
                $"{RowColumnName},{NumberColumnName},{StateColumnName} " +
                $"from EventSeats";
        }

        protected override EventSeat Map(IDataReader reader)
        {
            var eventSeat = new EventSeat();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var eventAreaIdColumnIndex = reader.GetOrdinal(EventAreaIdColumnName);
            var rowColumnIndex = reader.GetOrdinal(RowColumnName);
            var numberColumnIndex = reader.GetOrdinal(NumberColumnName);
            var stateColumnIndex = reader.GetOrdinal(StateColumnName);

            while (reader.Read())
            {
                eventSeat.Id = reader.GetInt32(idColumnIndex);
                eventSeat.EventAreaId = reader.GetInt32(eventAreaIdColumnIndex);
                eventSeat.Row = reader.GetInt32(rowColumnIndex);
                eventSeat.Number = reader.GetInt32(numberColumnIndex);
                eventSeat.State = (EventSeatState)reader.GetInt32(stateColumnIndex);
            }

            return eventSeat;
        }

        protected override IEnumerable<EventSeat> Maps(IDataReader reader)
        {
            var eventSeats = new List<EventSeat>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var eventAreaIdColumnIndex = reader.GetOrdinal(EventAreaIdColumnName);
            var rowColumnIndex = reader.GetOrdinal(RowColumnName);
            var numberColumnIndex = reader.GetOrdinal(NumberColumnName);
            var stateColumnIndex = reader.GetOrdinal(StateColumnName);

            while (reader.Read())
            {
                var eventSeat = new EventSeat
                {
                    Id = reader.GetInt32(idColumnIndex),
                    EventAreaId = reader.GetInt32(eventAreaIdColumnIndex),
                    Row = reader.GetInt32(rowColumnIndex),
                    Number = reader.GetInt32(numberColumnIndex),
                    State = (EventSeatState)reader.GetInt32(stateColumnIndex),
                };
                eventSeats.Add(eventSeat);
            }

            return eventSeats;
        }
    }
}
