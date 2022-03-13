namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class ReservationDetailsViewModel : IMapFrom<Reservation>
    {
        public string ClientEmail { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ReservationPassengerViewModel> Passengers { get; set; }
    }
}
