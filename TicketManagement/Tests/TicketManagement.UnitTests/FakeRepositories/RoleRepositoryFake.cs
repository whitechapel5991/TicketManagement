// ****************************************************************************
// <copyright file="RoleRepositoryFake.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Linq;
using TicketManagement.DAL.Models.Identity;
using TicketManagement.DAL.Repositories.Identity;

namespace TicketManagement.UnitTests.FakeRepositories
{
    public class RoleRepositoryFake : IRoleRepository
    {
        private readonly List<Role> repositoryData;

        public RoleRepositoryFake(List<Role> repositoryData)
        {
            this.repositoryData = repositoryData;
        }

        public int Create(Role entity)
        {
            this.repositoryData.Add(entity);
            return entity.Id;
        }

        public void Delete(int id)
        {
            this.repositoryData.Remove(this.repositoryData.Find(x => x.Id == id));
        }

        public Role FindByName(string roleName)
        {
            return this.repositoryData.First(x => x.Name == roleName);
        }

        public IQueryable<Role> GetAll()
        {
            return this.repositoryData.AsQueryable();
        }

        public Role GetById(int id)
        {
            return this.repositoryData.First(x => x.Id == id);
        }

        public void Update(Role role)
        {
            var index = this.repositoryData.FindIndex(x => x.Id == role.Id);
            if (index == -1)
            {
                return;
            }

            this.repositoryData.RemoveAt(index);
            this.repositoryData.Insert(index, role);
        }
    }
}
