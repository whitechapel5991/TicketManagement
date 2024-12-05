// ****************************************************************************
// <copyright file="LayoutRepository.cs" company="EPAM Systems">
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
    internal class LayoutRepository : RepositoryBase<Layout>
    {
        private const string IdColumnName = "Id";
        private const string VenueIdColumnName = "VenueId";
        private const string DescriptionColumnName = "Description";
        private const string NameColumnName = "Name";

        private const string IdParamName = "@Id";
        private const string VenueIdParamName = "@VenueId";
        private const string DescriptionParamName = "@Description";
        private const string NameParamName = "@Name";

        public LayoutRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(Layout entity, IDbCommand cmd)
        {
            const string CreateLayoutStoredProcedureName = "CreateLayout";

            cmd.CommandText = CreateLayoutStoredProcedureName;
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(VenueIdParamName, entity.VenueId);
            cmd.AddParameterWithValue(NameParamName, entity.Name);
        }

        protected override void UpdateCommandParameters(Layout entity, IDbCommand cmd)
        {
            const string UpdateLayoutStoredProcedureName = "UpdateLayout";

            cmd.CommandText = UpdateLayoutStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(VenueIdParamName, entity.VenueId);
            cmd.AddParameterWithValue(NameParamName, entity.Name);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteLayoutStoredProcedureName = "DeleteLayout";

            cmd.CommandText = DeleteLayoutStoredProcedureName;
            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{VenueIdColumnName}," +
                $"{DescriptionColumnName},{NameColumnName} " +
                $"FROM Layouts WHERE Id = {id}";
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{VenueIdColumnName}," +
                $"{DescriptionColumnName},{NameColumnName} " +
                $"from Layouts";
        }

        protected override Layout Map(IDataReader reader)
        {
            var layout = new Layout();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var venueIdColumnIndex = reader.GetOrdinal(VenueIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);

            while (reader.Read())
            {
                layout.Id = reader.GetInt32(idColumnIndex);
                layout.VenueId = reader.GetInt32(venueIdColumnIndex);
                layout.Description = reader.GetString(descriptionColumnIndex);
                layout.Name = reader.GetString(nameColumnIndex);
            }

            return layout;
        }

        protected override IEnumerable<Layout> Maps(IDataReader reader)
        {
            var layouts = new List<Layout>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var venueIdColumnIndex = reader.GetOrdinal(VenueIdColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);

            while (reader.Read())
            {
                var layout = new Layout
                {
                    Id = reader.GetInt32(idColumnIndex),
                    VenueId = reader.GetInt32(venueIdColumnIndex),
                    Description = reader.GetString(descriptionColumnIndex),
                    Name = reader.GetString(nameColumnIndex),
                };
                layouts.Add(layout);
            }

            return layouts;
        }
    }
}
