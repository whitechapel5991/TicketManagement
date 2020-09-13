// ****************************************************************************
// <copyright file="SeatRepository.cs" company="EPAM Systems">
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
    internal class SeatRepository : RepositoryBase<Seat>
    {
        private const string IdColumnName = "Id";
        private const string AreaIdColumnName = "AreaId";
        private const string RowColumnName = "Row";
        private const string NumberColumnName = "Number";

        private const string IdParamName = "@Id";
        private const string AreaIdParamName = "@AreaId";
        private const string RowParamName = "@Row";
        private const string NumberParamName = "@Number";

        public SeatRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(Seat entity, IDbCommand cmd)
        {
            const string CreateSeatStoredProcedureName = "CreateSeat";

            cmd.CommandText = CreateSeatStoredProcedureName;
            cmd.AddParameterWithValue(RowParamName, entity.Row);
            cmd.AddParameterWithValue(NumberParamName, entity.Number);
            cmd.AddParameterWithValue(AreaIdParamName, entity.AreaId);
        }

        protected override void UpdateCommandParameters(Seat entity, IDbCommand cmd)
        {
            const string UpdateSeatStoredProcedureName = "UpdateSeat";

            cmd.CommandText = UpdateSeatStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(RowParamName, entity.Row);
            cmd.AddParameterWithValue(NumberParamName, entity.Number);
            cmd.AddParameterWithValue(AreaIdParamName, entity.AreaId);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteSeatStoredProcedureName = "DeleteSeat";

            cmd.CommandText = DeleteSeatStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{AreaIdColumnName},{RowColumnName},{NumberColumnName} FROM Seats WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{AreaIdColumnName},{RowColumnName},{NumberColumnName} from Seats";
        }

        protected override Seat Map(IDataReader reader)
        {
            var seat = new Seat();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var areaIdColumnIndex = reader.GetOrdinal(AreaIdColumnName);
            var rowColumnIndex = reader.GetOrdinal(RowColumnName);
            var numberColumnIndex = reader.GetOrdinal(NumberColumnName);

            while (reader.Read())
            {
                seat.Id = reader.GetInt32(idColumnIndex);
                seat.AreaId = reader.GetInt32(areaIdColumnIndex);
                seat.Row = reader.GetInt32(rowColumnIndex);
                seat.Number = reader.GetInt32(numberColumnIndex);
            }

            return seat;
        }

        protected override IEnumerable<Seat> Maps(IDataReader reader)
        {
            var seats = new List<Seat>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var areaIdColumnIndex = reader.GetOrdinal(AreaIdColumnName);
            var rowColumnIndex = reader.GetOrdinal(RowColumnName);
            var numberColumnIndex = reader.GetOrdinal(NumberColumnName);

            while (reader.Read())
            {
                var seat = new Seat
                {
                    Id = reader.GetInt32(idColumnIndex),
                    AreaId = reader.GetInt32(areaIdColumnIndex),
                    Row = reader.GetInt32(rowColumnIndex),
                    Number = reader.GetInt32(numberColumnIndex),
                };
                seats.Add(seat);
            }

            return seats;
        }
    }
}
