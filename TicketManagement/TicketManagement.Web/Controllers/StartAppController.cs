using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagement.Web.Filters;

namespace TicketManagement.Web.Controllers
{
    public class StartAppController : Controller
    {
        // GET: StartApp
        [AjaxContentUrl]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReloadMenu()
        {
            return PartialView("_MenuBar");
        }
    }
}