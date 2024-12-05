// ****************************************************************************
// <copyright file="VenueRepository.cs" company="EPAM Systems">
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
    internal class VenueRepository : RepositoryBase<Venue>
    {
        private const string IdColumnName = "Id";
        private const string DescriptionColumnName = "Description";
        private const string AddressColumnName = "Address";
        private const string PhoneColumnName = "Phone";
        private const string NameColumnName = "Name";

        private const string IdParamName = "@Id";
        private const string AddressParamName = "@Address";
        private const string DescriptionParamName = "@Description";
        private const string PhoneParamName = "Phone";
        private const string NameParamName = "@Name";

        public VenueRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void CreateCommandParameters(Venue entity, IDbCommand cmd)
        {
            const string CreateVenueStoredProcedureName = "CreateVenue";

            cmd.CommandText = CreateVenueStoredProcedureName;

            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(AddressParamName, entity.Address);
            cmd.AddParameterWithValue(PhoneParamName, entity.Phone);
            cmd.AddParameterWithValue(NameParamName, entity.Name);
        }

        protected override void UpdateCommandParameters(Venue entity, IDbCommand cmd)
        {
            const string UpdateVenueStoredProcedureName = "UpdateVenue";

            cmd.CommandText = UpdateVenueStoredProcedureName;

            cmd.AddParameterWithValue(IdParamName, entity.Id);
            cmd.AddParameterWithValue(DescriptionParamName, entity.Description);
            cmd.AddParameterWithValue(AddressParamName, entity.Address);
            cmd.AddParameterWithValue(PhoneParamName, entity.Phone);
            cmd.AddParameterWithValue(NameParamName, entity.Name);
        }

        protected override void DeleteCommandParameters(int id, IDbCommand cmd)
        {
            const string DeleteVenueStoredProcedureName = "DeleteVenue";

            cmd.CommandText = DeleteVenueStoredProcedureName;

            cmd.AddParameterWithValue(IdParamName, id);
        }

        protected override void GetAllCommandParameters(IDbCommand cmd)
        {
            cmd.CommandText = $"select {IdColumnName},{DescriptionColumnName}," +
                $"{AddressColumnName},{PhoneColumnName},{NameColumnName} from Venues";
        }

        protected override void GetByIdCommandParameters(int id, IDbCommand cmd)
        {
            cmd.CommandText = $"SELECT {IdColumnName},{DescriptionColumnName}," +
                $"{AddressColumnName},{PhoneColumnName},{NameColumnName} FROM Venues WHERE Id = {id}";
        }

        protected override Venue Map(IDataReader reader)
        {
            var venue = new Venue();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var addressColumnIndex = reader.GetOrdinal(AddressColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);
            var phoneColumnIndex = reader.GetOrdinal(PhoneColumnName);

            while (reader.Read())
            {
                venue.Id = reader.GetInt32(idColumnIndex);
                venue.Description = reader.GetString(descriptionColumnIndex);
                venue.Address = reader.GetString(addressColumnIndex);
                venue.Phone = reader.GetString(phoneColumnIndex);
                venue.Name = reader.GetString(nameColumnIndex);
            }

            return venue;
        }

        protected override IEnumerable<Venue> Maps(IDataReader reader)
        {
            var venues = new List<Venue>();

            var idColumnIndex = reader.GetOrdinal(IdColumnName);
            var addressColumnIndex = reader.GetOrdinal(AddressColumnName);
            var descriptionColumnIndex = reader.GetOrdinal(DescriptionColumnName);
            var nameColumnIndex = reader.GetOrdinal(NameColumnName);
            var phoneColumnIndex = reader.GetOrdinal(PhoneColumnName);

            while (reader.Read())
            {
                var venue = new Venue
                {
                    Id = reader.GetInt32(idColumnIndex),
                    Description = reader.GetString(descriptionColumnIndex),
                    Address = reader.GetString(addressColumnIndex),
                    Phone = reader.GetString(phoneColumnIndex),
                    Name = reader.GetString(nameColumnIndex),
                };

                venues.Add(venue);
            }

            return venues;
        }
    }
}
