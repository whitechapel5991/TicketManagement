// ****************************************************************************
// <copyright file="SeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.BLL.Interfaces;
using TicketManagement.BLL.ServiceValidators.Interfaces;
using TicketManagement.DAL.Models;
using TicketManagement.DAL.Repositories.Base;

namespace TicketManagement.BLL.Services
{
    internal class SeatService : ISeatService
    {
        private readonly IRepository<Seat> seatRepository;
        private readonly ISeatValidator seatValidator;

        public SeatService(
            IRepository<Seat> seatRepository,
            ISeatValidator seatValidator)
        {
            this.seatRepository = seatRepository;
            this.seatValidator = seatValidator;
        }

        public int AddSeat(Seat seatDto)
        {
            this.seatValidator.Validate(seatDto);
            return this.seatRepository.Create(seatDto);
        }

        public void UpdateSeat(Seat seatDto)
        {
            this.seatValidator.Validate(seatDto);
            this.seatRepository.Update(seatDto);
        }

        public void DeleteSeat(int id)
        {
            this.seatRepository.Delete(id);
        }

        public Seat GetSeat(int id)
        {
            return this.seatRepository.GetById(id);
        }

        public IEnumerable<Seat> GetSeats()
        {
            return this.seatRepository.GetAll();
        }
    }
}
