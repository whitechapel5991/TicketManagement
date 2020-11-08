using System.Web.Mvc;
using TicketManagement.Web.Filters.AcionFilters;
using TicketManagement.Web.Filters.ExceptionFilters;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Controllers
{
    [Authorize]
    [UnknownExceptionFilter]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        [AjaxContentUrl]
        public PartialViewResult Index()
        {
            return this.PartialView(this.cartService.GetCartViewModelByUserName(this.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult Buy(int orderId)
        {
            this.cartService.Buy(orderId);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", "Cart") });
        }

        [HttpPost]
        public ActionResult DeleteFromCart(int orderId)
        {
            this.cartService.Delete(orderId);
            return this.Json(new { returnContentUrl = this.Url.Action("Index", "Cart") });
        }
    }
}