namespace FlightManager.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.Data;
    using FlightManager.Services.Data.Tests.ClassFixtures;
    using FlightManager.Services.Data.Tests.Factories;
    using Xunit;

    public class BaseTestClass : IClassFixture<MappingsProvider>
    {
        protected readonly ApplicationDbContext context;

        public BaseTestClass()
        {
            this.context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }
    }
}
