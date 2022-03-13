namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Data.Models.Enums;
    using FlightManager.Services.Mapping;

    public class ReservationPassengerViewModel : IMapFrom<Passanger>, IMapFrom<ReservationPassangerInputModel>
    {
        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PersonalNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public TicketType TicketType { get; set; }

        public string FullName => $"{Name} {MiddleName} {Surname}";
    }
}
