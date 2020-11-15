// ****************************************************************************
// <copyright file="BundleConfig.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Web.Optimization;
using TicketManagement.Web.Extensions;

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

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .UnorderedBundling()
                .Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/cldr.js",
                "~/Scripts/cldr/event.js",
                "~/Scripts/cldr/supplemental.js",
                "~/Scripts/globalize.js",
                "~/Scripts/globalize/number.js",
                "~/Scripts/globalize/date.js",
                "~/Scripts/globalize/currency.js",
                "~/Scripts/globalize/message.js",
                "~/Scripts/globalize/plural.js",
                "~/Scripts/globalize/relative-time.js",
                "~/Scripts/globalize/unit.js",
                "~/Scripts/jquery.validate.globalize.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui-i18n.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/umd/popper.js",
                "~/Scripts/bootstrap*",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts").Include(
                "~/Scripts/TicketManagement/constants.js",
                "~/Scripts/TicketManagement/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/jquery-ui.css",
                "~/Content/Site.css"));
        }
    }
}