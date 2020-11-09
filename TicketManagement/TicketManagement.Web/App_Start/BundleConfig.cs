// ****************************************************************************
// <copyright file="BundleConfig.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Optimization;

namespace TicketManagement.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/autoNumeric").Include(
                "~/Scripts/autoNumeric/autoNumeric-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/umd/popper.js",
                "~/Scripts/bootstrap*",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts").Include(
                "~/Scripts/TicketManagement/constants.js",
                "~/Scripts/TicketManagement/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"));
        }
    }
}