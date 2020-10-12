// ****************************************************************************
// <copyright file="IServiceValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.DAL.Models.Base;

namespace TicketManagement.BLL.ServiceValidators.Base
{
    public interface IServiceValidator<in TDalEntity>
        where TDalEntity : IEntity, new()
    {
        void Validation(TDalEntity entity);
    }
}
