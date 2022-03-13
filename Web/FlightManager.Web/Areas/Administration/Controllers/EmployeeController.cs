namespace FlightManager.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FlightManager.Common;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;
    using FlightManager.Web.Controllers;
    using FlightManager.Web.ViewModels.EmployeeModels;
    using FlightManager.Web.ViewModels.PaginationModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static FlightManager.Common.GlobalConstants;
    using FlightManager.Common.Extensions;
    using FlightManager.Services;
    using FlightManager.Web.Infrastucture.Routes;

    [Authorize(Roles = GlobalConstants.Roles.Administrator)]
    [Area("Administration")]
    public class EmployeeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPaginationService paginationService;

        public EmployeeController(UserManager<ApplicationUser> userManager,
            IPaginationService paginationService)
        {
            this.userManager = userManager;
            this.paginationService = paginationService;
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            ApplicationUser user = model.To<ApplicationUser>();
            await this.userManager.CreateAsync(user, model.Password);
            await this.userManager.AddToRoleAsync(user, Roles.Employee);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPage, int itemsPerPage = GlobalConstants.DefaultItemPerPage)
        {
            IEnumerable<ApplicationUser> users = await this.userManager.GetUsersInRoleAsync(Roles.Employee);

            var paginationData = new PaginationData()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                NumberOfPages = this.GetItemsPagesCount(users.Count(), itemsPerPage),
            };

            var result = new EmployeeAllViewModel()
            {
                Employees = users.To<EmployeeViewModel>()
                    .GetPage(page, itemsPerPage)
                    .ToList(),
                PaginationData = paginationData,
            };

            var routeString = new RouteString(
               "Administration/" +nameof(EmployeeController),
               nameof(this.All));

            result.PaginationData.Url = routeString.AppendPaginationPlaceholder();
            result.PaginationData.Url = routeString.AppendItemPerPagePlaceholder();
            return this.View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(id);
            return this.View(user.To<EmployeeDetailsViewModel>());
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(id);
            return this.View(user.To<EmployeeEditInputModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditInputModel model)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.UserName = model.Username;
            user.Address = model.Address;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.PersonalNumber = model.PersonalNumber;

            await this.userManager.UpdateAsync(user);
            return this.RedirectToAction(nameof(this.Details), new { model.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(id);
            return this.View(user.To<EmployeeDetailsViewModel>());
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteComfirm(string id)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(id);
            await this.userManager.DeleteAsync(user);
            return this.RedirectToAction(nameof(this.All));
        }

        private int GetItemsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
