// ****************************************************************************
// <copyright file="RepositoryFake.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.DAL.Models.Base;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.UnitTests.FakeRepositories
{
    internal class RepositoryFake<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity, new()
    {
        private readonly List<TEntity> repositoryData;

        public RepositoryFake(List<TEntity> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public int Create(TEntity entity)
        {
            this.repositoryData.Add(entity);
            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            int index = this.repositoryData.FindIndex(x => x.Id == entity.Id);
            if (index != -1)
            {
                this.repositoryData.RemoveAt(index);
                this.repositoryData.Insert(index, entity);
            }
        }

        public void Delete(int id)
        {
            int index = this.repositoryData.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                this.repositoryData.RemoveAt(index);
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.repositoryData.AsQueryable();
        }

        public TEntity GetById(int id)
        {
            try
            {
                return this.repositoryData.First(x => x.Id == id);
            }
            catch (InvalidOperationException)
            {
                return default;
            }
        }
    }
}
