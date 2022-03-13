namespace FlightManager.ViewModels.FlightModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class FlightViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        [Display(Name = "Take off time")]
        public DateTime TakeOffTime { get; set; }

        public TimeSpan Duration { get; set; }

        [Display(Name = "Plane number")]
        public string PlaneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration) =>
            configuration.CreateMap<Flight, FlightViewModel>()
                .ForMember(m => m.Duration, y => y.MapFrom(f => f.LandingTime - f.TakeOffTime))
                .ForMember(m => m.Origin, y => y.MapFrom(f => f.Origin.Name))
                .ForMember(m => m.Destination, y => y.MapFrom(f => f.Destination.Name));
    }
}
