using System;
using TicketManagement.Web.Constants.Extension;

namespace TicketManagement.Web.Constants
{
    [Flags]
    public enum Roles
    {
        [EnumRole("unauthorized")]
        Unauthorized = 0b00000000,

        [EnumRole("user")]
        User = 0b00000001,

        [EnumRole("event manager")]
        UserManager = 0b00000010,

        [EnumRole("admin")]
        Admin = 0b00000100,
    }
}