namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ClientEmail { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TicketsCount { get; set; }

        void IHaveCustomMappings.CreateMappings(IProfileExpression configuration) =>
            configuration.CreateMap<Reservation, ReservationViewModel>()
                .ForMember(m => m.TicketsCount, y => y.MapFrom(r => r.Passengers.Count));
    }
}