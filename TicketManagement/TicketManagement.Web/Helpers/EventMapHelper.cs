using System.Web.Mvc;
using TicketManagement.BLL.Dto;
using TicketManagement.Web.Constants;

namespace TicketManagement.Web.Helpers
{
    public static class EventMapHelper
    {
        public static MvcHtmlString CreateEventMap(this HtmlHelper html, EventDto eventDto)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.SetInnerText($"Event: {eventDto.Name}.\nStart Date: {eventDto.BeginDate}.\n" +
                $"End Date: {eventDto.EndDate}.\nDescription: {eventDto.Description}\n" +
                $"{eventDto.Layout.Name}");

            foreach (var eventArea in eventDto.EventAreas)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText($"Description: {eventArea.Description}.\n" +
                    $"X: {eventArea.CoordX}, Y: {eventArea.CoordY}.\n" +
                    $"Price: {eventArea.Price}");

                TagBuilder a = new TagBuilder("a")
                {
                    InnerHtml = $"<a href='/Event/EventAreaDetail/{eventArea.Id}'>Area Details</a>"
                };

                li.InnerHtml += a.ToString();

                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }

        public static MvcHtmlString CreateEventAreaMap(this HtmlHelper html, EventAreaDto eventAreaDto)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.SetInnerText($"Event Area description: {eventAreaDto.Description}.\nPrice: {eventAreaDto.Price}.\n" +
                $"X: { eventAreaDto.CoordX}, Y: { eventAreaDto.CoordY}.\n");

            foreach (var eventSeat in eventAreaDto.EventSeats)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(
                    $"X: {eventSeat.Number}, Y: {eventSeat.Row}.\n" +
                    $"Status: {eventSeat.State}");

                li.MergeAttribute("style", $"background-color: {SeatColor.GetSeatColor(eventSeat.State)};");

                TagBuilder a = new TagBuilder("a")
                {
                    InnerHtml = $"<a href='/Event/AddToCart/{eventSeat.Id}'>Add to cart</a>"
                };

                li.InnerHtml += a.ToString();

                ul.InnerHtml += li.ToString();
            }

            TagBuilder a2 = new TagBuilder("a")
            {
                InnerHtml = $"<a href='/Event/EventDetail/{eventAreaDto.Event.Id}'>Back to event</a>"
            };

            ul.InnerHtml += a2.ToString();

            return new MvcHtmlString(ul.ToString());
        }
    }
}