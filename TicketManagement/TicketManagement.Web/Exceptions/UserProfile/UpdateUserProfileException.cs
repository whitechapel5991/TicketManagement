using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Exceptions.UserProfile
{
    public class UpdateUserProfileException : Exception
    {
        public UpdateUserProfileException(string message)
            : base(message)
        {
        }
    }
}