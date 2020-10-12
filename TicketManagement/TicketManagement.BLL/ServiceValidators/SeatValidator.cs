// ****************************************************************************
// <copyright file="SeatValidator.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Linq;
using TicketManagement.BLL.Exceptions.Base;
using TicketManagement.BLL.Exceptions.SeatExceptions;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.ServiceValidators
{
    internal class SeatValidator : ISeatValidator
    {
        private readonly IRepository<Seat> seatRepository;
        private readonly IRepository<Area> areaRepository;

        public SeatValidator(
            IRepository<Seat> seatRepository,
            IRepository<Area> areaRepository)
        {
            this.seatRepository = seatRepository;
            this.areaRepository = areaRepository;
        }

        public void Validation(Seat entity)
        {
            var area = this.areaRepository.GetById(entity.AreaId);
            if (area == default(Area))
            {
                throw new EntityDoesNotExistException($"Area with id={entity.AreaId} doesn't exist.");
            }

            if (this.seatRepository
                .GetAll().Any(x => x.AreaId == entity.AreaId && x.Row == entity.Row && x.Number == entity.Number))
            {
                throw new SeatWithSameRowAndNumberInTheAreaExistException("Row and number of seat should be unique for area.");
            }
        }
    }
}
