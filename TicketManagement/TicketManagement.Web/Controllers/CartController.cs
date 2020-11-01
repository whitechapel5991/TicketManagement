using System.Web.Mvc;
using TicketManagement.Web.Filters;
using TicketManagement.Web.Interfaces;

namespace TicketManagement.Web.Controllers
{
    [Log]
    [LogCustomExceptionFilter(Order = 0)]
    [Authorize(Roles = "user")]
    [RedirectExceptionFilter]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        [AjaxContentUrl]
        public ActionResult Index()
        {
            return this.PartialView(this.cartService.GetCartViewModelByUserName(this.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult Buy(int orderId)
        {
            this.cartService.Buy(orderId);

            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFromCart(int orderId)
        {
            this.cartService.Delete(orderId);

            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}