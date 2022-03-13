
namespace FlightManager.ViewModels.FlightModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;
    using FlightManager.Web.ViewModels.ReservationModels;

    public class FlightDetailsViewModel : FlightViewModel, IHaveCustomMappings
    {
        [Display(Name = "Plane type")]
        public string PlaneType { get; set; }

        [Display(Name = "Pilot name")]
        public string PilotName { get; set; }

        [Display(Name = "Available Economy")]
        public int AvailableEconomy { get; set; }

        [Display(Name = "Available bussines")]
        public int AvailableBussines { get; set; }

        [NotMapped]
        public ReservationAllViewModel Reservations { get; set; }

        public new void CreateMappings(IProfileExpression configuration) =>
           configuration.CreateMap<Flight, FlightDetailsViewModel>()
               .ForMember(m => m.Duration, y => y.MapFrom(f => f.LandingTime - f.TakeOffTime))
               .ForMember(m => m.Origin, y => y.MapFrom(f => f.Origin.Name))
               .ForMember(m => m.Destination, y => y.MapFrom(f => f.Destination.Name));

    }
}
