using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Exceptions.UserProfile
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException(string message)
            : base(message)
        {
        }
    }
}