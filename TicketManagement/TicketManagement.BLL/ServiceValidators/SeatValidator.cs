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
        private readonly IRepository<Seat, int> seatRepository;
        private readonly IRepository<Area, int> areaRepository;

        public SeatValidator(
            IRepository<Seat, int> seatRepository,
            IRepository<Area, int> areaRepository)
        {
            this.seatRepository = seatRepository;
            this.areaRepository = areaRepository;
        }

        public void Validate(Seat entity)
        {
            var area = this.areaRepository.GetById(entity.AreaId);
            if (area == default(Area))
            {
                throw new EntityDoesNotExistException($"Area with id={entity.AreaId} doesn't exist.");
            }

            if (this.SeatWithRowAndNumberInTheAreaExist(entity.AreaId, entity.Row, entity.Number))
            {
                throw new SeatWithSameRowAndNumberInTheAreaExistException("Row and number of seat should be unique for area.");
            }
        }

        private bool SeatWithRowAndNumberInTheAreaExist(int areaId, int row, int number)
        {
            return this.seatRepository.GetAll()
                .Where(x => x.AreaId == areaId && x.Row == row && x.Number == number).Any();
        }
    }
}
