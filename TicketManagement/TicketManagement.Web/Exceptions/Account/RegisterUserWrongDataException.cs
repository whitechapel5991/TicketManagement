using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagement.Web.Exceptions.Account
{
    public class RegisterUserWrongDataException : Exception
    {
        public RegisterUserWrongDataException(string message)
            : base(message)
        {
        }
    }
}