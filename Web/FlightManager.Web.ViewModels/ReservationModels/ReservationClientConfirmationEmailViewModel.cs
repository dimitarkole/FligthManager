namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.ViewModels.FlightModels;

    public class ReservationClientConfirmationEmailViewModel
    {
        public IEnumerable<ReservationPassengerViewModel> Passengers { get; set; }

        public FlightViewModel Flight { get; set; }
    }
}
