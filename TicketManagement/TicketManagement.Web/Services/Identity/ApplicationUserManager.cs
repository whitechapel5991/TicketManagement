using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace TicketManagement.Web.Services.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser, int>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, int> store)
            : base(store)
        {
        }
    }
}