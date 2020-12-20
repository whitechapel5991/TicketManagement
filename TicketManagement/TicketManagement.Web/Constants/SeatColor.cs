// ****************************************************************************
// <copyright file="SeatColor.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using TicketManagement.Web.EventSeatService;

namespace TicketManagement.Web.Constants
{
    public static class SeatColor
    {
        public static string GetSeatColor(EventSeatState seatState)
        {
            string seatColor;
            switch (seatState)
            {
                case EventSeatState.Free:
                    seatColor = "skyblue";
                    break;
                case EventSeatState.InBasket:
                    seatColor = "sandybrown";
                    break;
                case EventSeatState.Sold:
                    seatColor = "darkgray";
                    break;
                default:
                    seatColor = "skyblue";
                    break;
            }

            return seatColor;
        }
    }
}