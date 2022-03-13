namespace FlightManager.Web.ViewModels.EmployeeModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;

    public class EmployeeEditInputModel : IMapTo<ApplicationUser>, IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(@"\d{10}")]
        [Display(Name = "Personal number")]
        public string PersonalNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
