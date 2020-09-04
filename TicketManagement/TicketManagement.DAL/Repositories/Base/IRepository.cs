// ****************************************************************************
// <copyright file="IRepository.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DAL.Repositories.Base
{
    public interface IRepository<T>
    {
        object Create(T entity);

        void Update(T entity);

        void Delete(int id);

        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
