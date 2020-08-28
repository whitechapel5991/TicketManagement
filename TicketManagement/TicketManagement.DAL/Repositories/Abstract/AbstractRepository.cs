// ****************************************************************************
// <copyright file="AbstractRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TicketManagement.DAL.Models.Abstract;

namespace TicketManagement.DAL.Repositories.Abstract
{
    public abstract class AbstractRepository<T>
        where T : IEntity, new()
    {
        private readonly DbProviderFactory providerFactory;
        private readonly string connectionString;

        protected AbstractRepository(string connectionString, string provider)
        {
            this.providerFactory = DbProviderFactories.GetFactory(provider);
            this.connectionString = connectionString;
        }

        public object Create(T entity)
        {
            var cmd = this.CommandBuild<T>(entity, this.InsertCommandParameters);
            var res = cmd.ExecuteScalar();
            return res;
        }

        public void Update(T entity)
        {
            var cmd = this.CommandBuild<T>(entity, this.UpdateCommandParameters);
            var res = cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var cmd = this.CommandBuild<int>(id, this.DeleteCommandParameters);
            var res = cmd.ExecuteNonQuery();
        }

        public T GetById(int id)
        {
            var cmd = this.CommandBuild<int>(id, this.GetByIdCommandParameters);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                return this.Map(reader);
            }
        }

        public IEnumerable<T> GetAll()
        {
            var cmd = this.CommandBuild(this.GetAllCommandParameters);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                return this.Maps(reader);
            }
        }

        protected abstract void InsertCommandParameters(T entity, DbCommand cmd);

        protected abstract void UpdateCommandParameters(T entity, DbCommand cmd);

        protected abstract void DeleteCommandParameters(int id, DbCommand cmd);

        protected abstract void GetByIdCommandParameters(int id, DbCommand cmd);

        protected abstract void GetAllCommandParameters(DbCommand cmd);

        protected abstract T Map(DbDataReader reader);

        protected abstract List<T> Maps(DbDataReader reader);

        private DbCommand CommandBuild<TU>(TU param, Action<TU, DbCommand> operation)
        {
            using (var connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    operation(param, cmd);
                    return cmd;
                }
            }
        }

        private DbCommand CommandBuild(Action<DbCommand> operation)
        {
            using (var connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    operation(cmd);
                    return cmd;
                }
            }
        }
    }
}
