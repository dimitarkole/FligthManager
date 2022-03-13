namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Data.Models.Enums;
    using FlightManager.Services.Mapping;

    public class ReservationPassangerInputModel : IMapTo<Passanger>, IMapTo<ReservationPassangerInputModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\d{10}")]
        public string PersonalNumber { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Nationality { get; set; }

        public TicketType TicketType { get; set; }
    }
}
