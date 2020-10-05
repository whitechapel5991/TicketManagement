// ****************************************************************************
// <copyright file="RoleRepositoryFake.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.BLL.Interfaces.Identity;
using TicketManagement.DAL.Models.Identity;

namespace TicketManagement.UnitTests.FakeRepositories
{
    public class RoleRepositoryFake : IRoleService
    {
        private readonly List<Role> repositoryData;

        public RoleRepositoryFake(List<Role> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public void Add(string roleName)
        {
            this.repositoryData.Add(new Role { Name = roleName });
        }

        public void Delete(string roleName)
        {
            this.repositoryData.Remove(this.repositoryData.Find(x => x.Name == roleName));
        }

        public Role FindById(int id)
        {
            return this.repositoryData.First(x => x.Id == id);
        }

        public Role FindByName(string roleName)
        {
            return this.repositoryData.First(x => x.Name == roleName);
        }

        public IQueryable<Role> GetAll()
        {
            return this.repositoryData.AsQueryable();
        }

        public void Update(Role role)
        {
            int index = this.repositoryData.FindIndex(x => x.Id == role.Id);
            if (index == -1)
            {
                return;
            }

            this.repositoryData.RemoveAt(index);
            this.repositoryData.Insert(index, role);
        }
    }
}
