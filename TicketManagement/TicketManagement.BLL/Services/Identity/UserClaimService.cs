// ****************************************************************************
// <copyright file="UserClaimService.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.BLL.Services.Identity
{
    internal class UserClaimService : IUserClaimService
    {
        private readonly IUserClaimRepository userClaimRepository;

        public UserClaimService(IUserClaimRepository userClaimRepository)
        {
            this.userClaimRepository = userClaimRepository;
        }

        public void Add(int userId, Claim claim)
        {
            this.userClaimRepository.Create(new UserClaim
            {
                UserId = userId,
                ClaimValue = claim.Value,
                ClaimType = claim.Type,
            });
        }

        public IList<Claim> GetClaims(int userId)
        {
            return this.userClaimRepository
                .GetByUserId(userId)
                .Select(x => new Claim(x.ClaimType, x.ClaimValue))
                .ToList();
        }

        public void Remove(int userId, Claim claim)
        {
            var userClaims = this.userClaimRepository.GetByUserId(userId);
            var userClaimForDeleting = userClaims.FirstOrDefault(x =>
                x.UserId == userId && x.ClaimValue == claim.Value && x.ClaimType == claim.Type);
            if (userClaimForDeleting != null)
            {
                this.userClaimRepository.Delete(userClaimForDeleting.Id);
            }
        }
    }
}
