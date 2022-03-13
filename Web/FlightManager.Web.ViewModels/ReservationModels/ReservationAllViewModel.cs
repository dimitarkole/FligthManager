namespace FlightManager.Web.ViewModels.ReservationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.Web.ViewModels.PaginationModels;

    public class ReservationAllViewModel
    {
        public IEnumerable<ReservationViewModel> Reservations { get; set; }

        public PaginationData PaginationData { get; set; }
    }
}
