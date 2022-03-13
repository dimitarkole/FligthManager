namespace FlightManager.Web.ViewModels.EmployeeModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class EmployeeDetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Personal number")]
        public string PersonalNumber { get; set; }

        public string Address { get; set; }
    }
}
