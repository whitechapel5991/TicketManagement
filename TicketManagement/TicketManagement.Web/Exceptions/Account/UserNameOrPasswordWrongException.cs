using System;

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