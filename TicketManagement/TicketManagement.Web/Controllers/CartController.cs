using System.Web.Mvc;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return this.View(this.cartService.GetCartViewModelByUserName(this.User.Identity.Name));
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Buy(int orderId)
        {
            this.cartService.Buy(orderId);

            return this.View("Index");
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult DeleteFromCart(int orderId)
        {
            this.cartService.Delete(orderId);

            return this.View("Index");
        }
    }
}