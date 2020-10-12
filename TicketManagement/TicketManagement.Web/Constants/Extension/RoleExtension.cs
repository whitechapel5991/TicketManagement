using System.Reflection;

namespace TicketManagement.Web.Constants.Extension
{
    public static class RoleExtension
    {
        public static string GetStringValue(this Roles value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());

            var attributes = fieldInfo.GetCustomAttribute(
                typeof(EnumRoleAttribute), false) as EnumRoleAttribute;

            return attributes?.StringValue;
        }
    }
}