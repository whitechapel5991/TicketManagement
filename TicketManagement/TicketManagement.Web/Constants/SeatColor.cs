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
                    seatColor = "blue";
                    break;
                case EventSeatState.InBasket:
                    seatColor = "orange";
                    break;
                case EventSeatState.Sold:
                    seatColor = "black";
                    break;
                default:
                    seatColor = "blue";
                    break;
            }

            return seatColor;
        }
    }
}