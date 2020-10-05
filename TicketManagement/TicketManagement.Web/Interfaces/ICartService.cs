using TicketManagement.Web.Models.Cart;

namespace TicketManagement.Web.Interfaces
{
    public interface ICartService
    {
        CartViewModel GetCartViewModelByUserName(string userName);

        void Buy(int orderId);

        void Delete(int orderId);
    }
}
