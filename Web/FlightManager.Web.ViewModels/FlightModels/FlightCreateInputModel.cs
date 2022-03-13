namespace FlightManager.Web.ViewModels.FlightModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class FlightCreateInputModel : IMapTo<Flight>, IHaveCustomMappings
    {
        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Display(Name = "Take off time")]
        public DateTime TakeOffTime { get; set; }

        [Display(Name = "Landing time")]
        public DateTime LandingTime { get; set; }

        [Required]
        [Display(Name = "Plane type")]
        public string PlaneType { get; set; }

        [Required]
        [Display(Name = "Plane number")]
        public string PlaneNumber { get; set; }

        [Required]
        [Display(Name = "Pilot name")]
        public string PilotName { get; set; }

        [Display(Name = "Available Economy")]
        public int AvailableEconomy { get; set; }

        [Display(Name = "Available bussines")]
        public int AvailableBussines { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FlightCreateInputModel, Flight>()
                .ForMember(m => m.Destination, y => y.Ignore())
                .ForMember(m => m.Origin, y => y.Ignore());

            configuration.CreateMap<Flight, FlightCreateInputModel>()
                .ForMember(m => m.Origin, y => y.MapFrom(f => f.Origin.Name))
                .ForMember(m => m.Destination, y => y.MapFrom(f => f.Destination.Name));
        }
    }
}
