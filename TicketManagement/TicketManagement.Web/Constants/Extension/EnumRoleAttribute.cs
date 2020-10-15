using System;

namespace TicketManagement.Web.Constants.Extension
{
    public class EnumRoleAttribute : Attribute
    {
        public EnumRoleAttribute(string stringValue)
        {
            this.StringValue = stringValue;
        }

        public string StringValue { get; }
    }
}