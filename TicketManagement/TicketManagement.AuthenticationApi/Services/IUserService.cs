using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.AuthenticationApi.Models;

namespace TicketManagement.AuthenticationApi.Services
{
    public interface IUserService
    {
        UserModel FindById(int id);

        void IncreaseBalance(decimal balance, string userName);

        List<UserModel> GetUsers();

        UserModel FindByName(string userName);

        IdentityResult Add(UserModel user);

        IdentityResult Update(int id, UserModel user);

        IdentityResult Delete(int id);

        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
    }
}
