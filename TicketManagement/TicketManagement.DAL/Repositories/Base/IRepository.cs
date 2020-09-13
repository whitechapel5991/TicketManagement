// ****************************************************************************
// <copyright file="IRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models.Base;

namespace TicketManagement.DAL.Repositories.Base
{
    public interface IRepository<TDalEntity>
        where TDalEntity : IEntity, new()
    {
        int Create(TDalEntity entity);

        void Update(TDalEntity entity);

        void Delete(int id);

        TDalEntity GetById(int id);

        IEnumerable<TDalEntity> GetAll();
    }
}
