using FlightManager.ViewModels.FlightModels;
using FlightManager.Web.ViewModels.PaginationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Web.ViewModels.EmployeeModels
{
    public class EmployeeAllViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public PaginationData PaginationData { get; set; }
    }
}
