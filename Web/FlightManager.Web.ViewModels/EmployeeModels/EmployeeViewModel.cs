namespace FlightManager.Web.ViewModels.EmployeeModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class EmployeeViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Personal number")]
        public string PersonalNumber { get; set; }
    }
}
