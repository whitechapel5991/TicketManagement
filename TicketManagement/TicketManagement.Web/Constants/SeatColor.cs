using TicketManagement.DAL.Constants;

namespace TicketManagement.Web.Constants
{
    public static class SeatColor
    {
        public static string GetSeatColor(EventSeatState seatState)
        {
            var seatColor = "blue";
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