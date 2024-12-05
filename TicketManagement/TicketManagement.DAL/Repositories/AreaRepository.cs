// ****************************************************************************
// <copyright file="AreaRepository.cs" company="EPAM Systems">
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
    internal class AreaRepository : RepositoryBase<Area>
    {
        private const string IdColumnName = "Id";
        private const string LayoutIdColumnName = "LayoutId";
        private const string DescriptionColumnName = "Description";
        private const string CoordXColumnName = "CoordX";
        private const string CoordYColumnName = "CoordY";

        private const string CoordYParamName = "@CoordY";
        private const string CoordXParamName = "@CoordX";
        private const string LayoutIdParamName = "@LayoutId";
        private const string DescriptionParamName = "@Description";
        private const string IdParamName = "@Id";

        public AreaRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(Area entity, IDbCommand cmd)
        {
            const string CreateAreaStoredProcedureName = "CreateArea";

            cmd.CommandText = CreateAreaStoredProcedureName;
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(CoordXParamName, entity.CoordX);
            cmd.AddParameterWithValue(CoordYParamName, entity.CoordY);
            cmd.AddParameterWithValue(LayoutIdParamName, entity.LayoutId);
        }

        protected override void UpdateCommandParameters(Area entity, IDbCommand cmd)
        {
            const string UpdateAreaStoredProcedureName = "UpdateArea";

            cmd.CommandText = UpdateAreaStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(CoordXParamName, entity.CoordX);
            cmd.AddParameterWithValue(CoordYParamName, entity.CoordY);
            cmd.AddParameterWithValue(LayoutIdParamName, entity.LayoutId);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteAreaStoredProcedureName = "DeleteArea";

            cmd.CommandText = DeleteAreaStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{LayoutIdColumnName}," +
                $"{DescriptionColumnName},{CoordXColumnName},{CoordYColumnName} " +
                $"FROM Areas WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{LayoutIdColumnName}," +
                $"{DescriptionColumnName},{CoordXColumnName},{CoordYColumnName} " +
                $"from Areas";
        }

        protected override Area Map(IDataReader reader)
        {
            var area = new Area();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var layoutIdColumnIndex = reader.GetOrdinal(LayoutIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var coordXColumnIndex = reader.GetOrdinal(CoordXColumnName);
            var coordYColumnIndex = reader.GetOrdinal(CoordYColumnName);

            while (reader.Read())
            {
                area.Id = reader.GetInt32(idColumnIndex);
                area.LayoutId = reader.GetInt32(layoutIdColumnIndex);
                area.Description = reader.GetString(descriptionColumnIndex);
                area.CoordX = reader.GetInt32(coordXColumnIndex);
                area.CoordY = reader.GetInt32(coordYColumnIndex);
            }

            return area;
        }

        protected override IEnumerable<Area> Maps(IDataReader reader)
        {
            var areas = new List<Area>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var layoutIdColumnIndex = reader.GetOrdinal(LayoutIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var coordXColumnIndex = reader.GetOrdinal(CoordXColumnName);
            var coordYColumnIndex = reader.GetOrdinal(CoordYColumnName);

            while (reader.Read())
            {
                var area = new Area
                {
                    Id = reader.GetInt32(idColumnIndex),
                    LayoutId = reader.GetInt32(layoutIdColumnIndex),
                    Description = reader.GetString(descriptionColumnIndex),
                    CoordX = reader.GetInt32(coordXColumnIndex),
                    CoordY = reader.GetInt32(coordYColumnIndex),
                };
                areas.Add(area);
            }

            return areas;
        }
    }
}
