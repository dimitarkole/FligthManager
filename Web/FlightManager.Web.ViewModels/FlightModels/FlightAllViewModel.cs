namespace FlightManager.Web.ViewModels.FlightModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.PaginationModels;

    public class FlightAllViewModel
    {
        public IEnumerable<FlightViewModel> Flight { get; set; }

        public PaginationData PaginationData { get; set; }
    }
}
