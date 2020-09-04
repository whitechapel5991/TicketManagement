// ****************************************************************************
// <copyright file="ISeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.BLL.DTO;

namespace TicketManagement.BLL.Interfaces
{
    internal interface ISeatService
    {
        IEnumerable<SeatDto> GetSeats();

        SeatDto GetSeat(int id);

        int AddSeat(SeatDto entity);

        void UpdateSeat(SeatDto entity);

        void DeleteSeat(int id);
    }
}
