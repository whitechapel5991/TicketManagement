// ****************************************************************************
// <copyright file="RepositoryBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Repositories.Base
{
    internal abstract class RepositoryBase<TDalEntity> : IRepository<TDalEntity>
        where TDalEntity : IEntity, new()
    {
        private readonly DbProviderFactory providerFactory;
        private readonly string connectionString;

        protected RepositoryBase(string connectionString, string provider)
        {
            this.providerFactory = DbProviderFactories.GetFactory(provider);
            this.connectionString = connectionString;
        }

        public int Create(TDalEntity entity)
        {
            return Convert.ToInt32(this.CommandExecuteScalar(entity, this.CreateCommandParameters));
        }

        public void Update(TDalEntity entity)
        {
            this.CommandExecuteScalar(entity, this.UpdateCommandParameters);
        }

        public void Delete(int id)
        {
            this.CommandExecuteScalar(id, this.DeleteCommandParameters);
        }

        public TDalEntity GetById(int id)
        {
            return this.CommandExecuteReaderById(id, this.GetByIdCommandParameters);
        }

        public IEnumerable<TDalEntity> GetAll()
        {
            return this.CommandExecuteReaderAll(this.GetAllCommandParameters);
        }

        protected abstract void CreateCommandParameters(TDalEntity entity, IDbCommand cmd);

        protected abstract void UpdateCommandParameters(TDalEntity entity, IDbCommand cmd);

        protected abstract void DeleteCommandParameters(int id, IDbCommand cmd);

        protected abstract void GetByIdCommandParameters(int id, IDbCommand cmd);

        protected abstract void GetAllCommandParameters(IDbCommand cmd);

        protected abstract TDalEntity Map(IDataReader reader);

        protected abstract IEnumerable<TDalEntity> Maps(IDataReader reader);

        private object CommandExecuteScalar<TCommandParam>(TCommandParam param, Action<TCommandParam, IDbCommand> dbOperation)
        {
            using (IDbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dbOperation(param, cmd);
                    return cmd.ExecuteScalar();
                }
            }
        }

        private TDalEntity CommandExecuteReaderById(int id, Action<int, IDbCommand> operation)
        {
            using (IDbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    operation(id, cmd);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        return this.Map(reader);
                    }
                }
            }
        }

        private IEnumerable<TDalEntity> CommandExecuteReaderAll(Action<IDbCommand> operation)
        {
            using (IDbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    operation(cmd);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        return this.Maps(reader);
                    }
                }
            }
        }
    }
}
