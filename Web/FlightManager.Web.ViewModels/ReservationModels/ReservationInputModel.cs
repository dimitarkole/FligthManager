namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using FlightManager.Services.Mapping;

    public class ReservationInputModel : IHaveCustomMappings
    {
        public ReservationInputModel()
        {
            Passengers = new List<ReservationPassangerInputModel>();
        }

        public ReservationClientInputModel Client { get; set; }

        public int FlightId { get; set; }

        public List<ReservationPassangerInputModel> Passengers { get; set; }

        public void CreateMappings(IProfileExpression configuration) =>
            configuration.CreateMap<ReservationInputModel, Data.Models.Reservation>()
            .ForMember(m => m.Passengers, y => y.Ignore());
    }
}
