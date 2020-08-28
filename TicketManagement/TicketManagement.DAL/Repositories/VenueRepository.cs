// ****************************************************************************
// <copyright file="VenueRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Data.Common;
using TicketManagement.DAL.Extensions;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Abstract;

namespace TicketManagement.DAL.Repositories
{
    internal class VenueRepository : AbstractRepository<Venue>
    {
        public VenueRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        protected override void DeleteCommandParameters(int id, DbCommand cmd)
        {
            cmd.CommandText = "DeleteVenue";
            cmd.AddParameterWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
        {
            cmd.CommandText = "GetAllVenue";
        }

        protected override void GetByIdCommandParameters(int id, DbCommand cmd)
        {
            cmd.CommandText = "GetByIdVenue";
            cmd.AddParameterWithValue("@Id", id);
        }

        protected override void InsertCommandParameters(Venue entity, DbCommand cmd)
        {
            cmd.CommandText = "InsertVenue";

            cmd.AddParameterWithValue("@Description", entity.Description);
            cmd.AddParameterWithValue("@Address", entity.Address);
            cmd.AddParameterWithValue("@Phone", entity.Phone);
        }

        protected override Venue Map(DbDataReader reader)
        {
            Venue venue = new Venue();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    venue.Id = Convert.ToInt32(reader["Id"].ToString());
                    venue.Description = reader["Description"].ToString();
                    venue.Address = reader["Address"].ToString();
                    venue.Phone = reader["Phone"].ToString();
                }
            }

            return venue;
        }

        protected override List<Venue> Maps(DbDataReader reader)
        {
            List<Venue> venues = new List<Venue>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Venue venue = new Venue
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Description = reader["Description"].ToString(),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString(),
                    };
                    venues.Add(venue);
                }
            }

            return venues;
        }

        protected override void UpdateCommandParameters(Venue entity, DbCommand cmd)
        {
            cmd.CommandText = "UpdateVenue";
            cmd.AddParameterWithValue("@Id", entity.Id);
            cmd.AddParameterWithValue("@Description", entity.Description);
            cmd.AddParameterWithValue("@Address", entity.Address);
            cmd.AddParameterWithValue("@Phone", entity.Phone);
        }
    }
}
