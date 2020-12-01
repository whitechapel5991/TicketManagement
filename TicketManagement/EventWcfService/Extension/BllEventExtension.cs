namespace EventWcfService.Extension
{
    public static class BllEventExtension
    {
        public static Event ConvertToWcfEvent(this TicketManagement.DAL.Models.Event bllEvent)
        {
            return new Event()
            {
                Id = bllEvent.Id,
                Name = bllEvent.Name,
                BeginDateUtc = bllEvent.BeginDateUtc,
                EndDateUtc = bllEvent.EndDateUtc,
                Description = bllEvent.Description,
                Published = bllEvent.Published,
                LayoutId = bllEvent.LayoutId,
                ImageUrl = bllEvent.ImageUrl,
            };
        }
    }
}