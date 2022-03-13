namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class ReservationClientInputModel : IMapTo<Client>
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
