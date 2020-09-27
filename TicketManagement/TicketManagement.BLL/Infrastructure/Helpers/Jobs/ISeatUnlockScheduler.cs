namespace TicketManagement.BLL.Infrastructure.Helpers.Jobs
{
    public interface ISeatUnlockScheduler
    {
        void Start(int eventSeatId);

        void Shutdown(int eventSeatId);
    }
}