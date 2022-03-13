namespace FlightManager.Services.Data.Tests.ClassFixtures
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    using FlightManager.Services.Mapping;
    using FlightManager.Web.ViewModels;

    public class MappingsProvider
    {
        public MappingsProvider()
        {
            //Register all mappings in the app
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
