// ****************************************************************************
// <copyright file="StartAppController.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Mvc;
using TicketManagement.Web.Filters.ActionFilters;

namespace TicketManagement.Web.Controllers
{
    public class StartAppController : Controller
    {
        // GET: StartApp
        [AjaxContentUrl]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult ReloadMenu()
        {
            return this.PartialView("_MenuBar");
        }
    }
}