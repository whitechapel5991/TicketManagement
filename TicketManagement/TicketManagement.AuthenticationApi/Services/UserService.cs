using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.AuthenticationApi.Models;
using TicketManagement.AuthenticationApi.Models.Extension;
using TicketManagement.AuthenticationApi.Services.Identity;

namespace TicketManagement.AuthenticationApi.Services
{
    public class UserService : IUserService
    {
        private readonly BLL.Interfaces.Identity.IUserService userService;
        private readonly UserManager<TicketManagementUser> userManager;

        public UserService(BLL.Interfaces.Identity.IUserService userService, UserManager<TicketManagementUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public IdentityResult Add(UserModel user)
        {
            return this.userManager.CreateAsync(user.ConvertToTicketManagementUser()).Result;
        }

        public IdentityResult Update(int id, UserModel user)
        {
            user.Id = id;
            return this.userManager.UpdateAsync(user.ConvertToTicketManagementUser()).Result;
        }

        public IdentityResult Delete(int id)
        {
            var deleteUser = this.userManager.FindByIdAsync(id.ToString()).Result;
            return this.userManager.DeleteAsync(deleteUser).Result;
        }

        public async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public UserModel FindById(int id)
        {
            return this.userManager.FindByIdAsync(id.ToString()).Result.ConvertToUserModel();
        }

        public UserModel FindByName(string userName)
        {
            return this.userManager.FindByNameAsync(userName).Result.ConvertToUserModel();
        }

        public List<UserModel> GetUsers()
        {
            return this.userService.GetUsers().ToList().ConvertToListUserModel();
        }

        public void IncreaseBalance(decimal balance, string userName)
        {
            this.userService.IncreaseBalance(balance, userName);
        }
    }
}
