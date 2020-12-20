using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.AuthenticationApi.Models.Extension
{
    public static class BllTicketManagementUserExtension
    {
        public static List<UserModel> ConvertToListUserModel(this List<TicketManagementUser> userList)
        {
            return userList.Select(tmUser => new UserModel
            {
                Id = tmUser.Id,
                UserName = tmUser.UserName,
                Email = tmUser.Email,
                Balance = tmUser.Balance,
                FirstName = tmUser.FirstName,
                Surname = tmUser.Surname,
                Language = tmUser.Language,
                TimeZone = tmUser.TimeZone,
            }).ToList();
        }
    }
}
