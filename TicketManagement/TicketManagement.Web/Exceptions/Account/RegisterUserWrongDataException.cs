using System;

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