using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Exceptions.Account
{
    public class UserNameOrPasswordWrongException : Exception
    {
        public UserNameOrPasswordWrongException(string message)
            : base(message)
        {
        }
    }
}