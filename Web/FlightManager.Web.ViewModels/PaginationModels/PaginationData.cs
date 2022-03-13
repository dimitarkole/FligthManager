namespace FlightManager.Web.ViewModels.PaginationModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaginationData
    {
        public int NumberOfPages { get; set; }

        public string Url { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
