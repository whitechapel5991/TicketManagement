// ****************************************************************************
// <copyright file="ISeatService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using TicketManagement.DAL.Models;

namespace TicketManagement.BLL.Interfaces
{
    internal interface ISeatService
    {
        IEnumerable<Seat> GetSeats();

        Seat GetSeat(int id);

        int AddSeat(Seat entity);

        void UpdateSeat(Seat entity);

        void DeleteSeat(int id);
    }
}
