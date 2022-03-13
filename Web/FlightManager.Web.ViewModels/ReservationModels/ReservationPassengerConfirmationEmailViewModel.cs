namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.ViewModels.FlightModels;

    public class ReservationPassengerConfirmationEmailViewModel
    {
        public ReservationPassangerInputModel Passenger { get; set; }

        public FlightViewModel Flight { get; set; }
    }
}
