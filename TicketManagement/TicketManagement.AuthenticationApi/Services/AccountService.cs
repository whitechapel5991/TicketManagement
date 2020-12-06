// ****************************************************************************
// <copyright file="AccountService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TicketManagement.AuthenticationApi.Exceptions.Account;
using TicketManagement.AuthenticationApi.Models;
using TicketManagement.AuthenticationApi.Services.Identity;

namespace TicketManagement.AuthenticationApi.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<TicketManagementUser> userManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<TicketManagementUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> SignInAsync(string userName, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new UserNameOrPasswordWrongException(HttpStatusCode.BadRequest, "WrongCredentials");
            }

            var userSigninResult = await this.userManager.CheckPasswordAsync(user, password);

            if (!userSigninResult)
            {
                throw new UserNameOrPasswordWrongException(HttpStatusCode.BadRequest, "WrongCredentials");
            }

            var userRoles = await this.userManager.GetRolesAsync(user);

            return this.GenerateJwt(user, userRoles);
        }

        public async Task RegisterUserAsync(RegisterModel registerVm)
        {
            var user = this.MapIdentityUser(registerVm);
            var registerResult = await this.userManager.CreateAsync(user, registerVm.Password);
            if (!registerResult.Succeeded)
            {
                throw new RegisterUserWrongDataException(HttpStatusCode.BadRequest, string.Join(", ", registerResult.Errors));
            }
        }

        public string ValidateToken(string token)
        {
            ClaimsPrincipal principal = this.GetPrincipal(token);
            if (principal == default)
            {
                return default;
            }

            ClaimsIdentity identity;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return default;
            }

            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            string username = usernameClaim.Value;
            return username;
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return default;
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = key,
                    ValidAudience = this.configuration["JWT:ValidAudience"],
                    ValidIssuer = this.configuration["JWT:ValidIssuer"],
                };
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, validatedToken: out _);
                return principal;
            }
            catch
            {
                return default;
            }
        }

        private string GenerateJwt(TicketManagementUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(3);

            var token = new JwtSecurityToken(
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private TicketManagementUser MapIdentityUser(RegisterModel viewModel)
        {
            return new TicketManagementUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                FirstName = viewModel.FirstName,
                Surname = viewModel.Surname,
                Language = viewModel.Language,
                TimeZone = viewModel.TimeZone,
            };
        }
    }
}