namespace FlightManager.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FlightManager.Common;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.FlightModels;
    using Microsoft.AspNetCore.Mvc;
    using FlightManager.Services.Mapping;

    public class FlightController : AdministrationController
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(FlightCreateInputModel model)
        {
            if (model.LandingTime < DateTime.Now)
            {
                this.ModelState.AddModelError(nameof(FlightCreateInputModel.LandingTime), "Landing time must be in the future!");
            }

            if (model.TakeOffTime < DateTime.Now)
            {
                this.ModelState.AddModelError(nameof(FlightCreateInputModel.LandingTime), "Take off time must be in the future!");
            }

            if (model.LandingTime < model.TakeOffTime)
            {
                this.ModelState.AddModelError(nameof(FlightCreateInputModel.TakeOffTime), "Take off time must be before landing time!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.flightService.Create(model);
            return this.Redirect("/");
        }

        public IActionResult Edit(int id)
        {
            FlightEditInputModel model = this.flightService.GetById(id, GlobalConstants.DefaultPage, GlobalConstants.DefaultItemPerPage)
                .To<FlightEditInputModel>();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FlightEditInputModel model, int id)
        {
            await this.flightService.Update(model, id);
            return this.Redirect("/");
        }

        public IActionResult Delete(int id, int page = GlobalConstants.DefaultPage, int itemsPerPage = GlobalConstants.DefaultItemPerPage)
        {
            FlightDetailsViewModel model = this.flightService.GetById(id, page, itemsPerPage);
            return this.View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await this.flightService.Delete(id);
            return this.Redirect("/");
        }
    }
}
