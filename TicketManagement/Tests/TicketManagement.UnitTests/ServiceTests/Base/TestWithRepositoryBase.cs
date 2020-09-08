// ****************************************************************************
// <copyright file="TestWithRepositoryBase.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Autofac.Extras.Moq;
using AutoFixture;
using NUnit.Framework;
using TicketManagement.DAL.Models.Base;
using TicketManagement.DAL.Repositories.Base;
using TicketManagement.UnitTests.FakeRepositories;
using Test = TicketManagement.UnitTests.TestBase.TestBase;

namespace TicketManagement.UnitTests.ServiceTests.Base
{
    public abstract class TestWithRepositoryBase<T> : Test
        where T : IEntity, new()
    {
        private readonly ImmutableList<T> fakeRepositoryDataImmutable;
        private List<T> fakeRepositoryData;

        public TestWithRepositoryBase(List<T> repositoryData)
        {
            this.fakeRepositoryDataImmutable = repositoryData.ToImmutableList();
        }

        protected ImmutableList<T> FakeRepositoryData
        {
            get { return this.fakeRepositoryData.ToImmutableList(); }
        }

        protected AutoMock Mock { get; private set; }

        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void InitWithRepositoryBase()
        {
            this.Fixture = new Fixture();
            this.Mock = AutoMock.GetLoose();
            this.fakeRepositoryData = this.fakeRepositoryDataImmutable.ToList();

            var repositoryMoq = this.Mock.Mock<IRepository<T>>();
            var venueFakeRepository = new RepositoryFake<T>(this.fakeRepositoryData)
                .SetUpFakeRepository(repositoryMoq);

            // Register mock repository and build container
            this.ContainerBuilder.RegisterMock<IRepository<T>>(venueFakeRepository);
            this.Container = this.ContainerBuilder.Build();
        }

        public override void Dispose()
        {
            base.Dispose();
            this.Mock?.Dispose();
        }
    }
}
