// ****************************************************************************
// <copyright file="SeatService.svc.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces;
using TicketManagement.WcfService.Contracts;
using TicketManagement.WcfService.Extensions;

namespace TicketManagement.WcfService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SeatService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SeatService.svc or SeatService.svc.cs at the Solution Explorer and start debugging.
    public class SeatService : ISeatContract
    {
        private readonly ISeatService seatService;

        public SeatService(ISeatService seatService)
        {
            this.seatService = seatService;
        }

        public int AddSeat(Seat entity)
        {
            return this.seatService.AddSeat(entity.ConvertToBllSeat());
        }

        public void DeleteSeat(int id)
        {
            this.seatService.DeleteSeat(id);
        }

        public Seat GetSeat(int id)
        {
            return this.seatService.GetSeat(id).ConvertToWcfSeat();
        }

        public IEnumerable<Seat> GetSeats()
        {
            return this.seatService.GetSeats().Select(entity => entity.ConvertToWcfSeat());
        }

        public void UpdateSeat(Seat entity)
        {
            this.seatService.UpdateSeat(entity.ConvertToBllSeat());
        }
    }
}
