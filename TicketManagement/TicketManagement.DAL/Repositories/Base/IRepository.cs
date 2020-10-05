// ****************************************************************************
// <copyright file="IRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;

namespace TicketManagement.DAL.Repositories.Base
{
    public interface IRepository<TDalEntity, TKey>
        where TDalEntity : class, new()
    {
        TKey Create(TDalEntity entity);

        void Update(TDalEntity entity);

        void Delete(TKey id);

        TDalEntity GetById(TKey id);

        IQueryable<TDalEntity> GetAll();
    }
}
