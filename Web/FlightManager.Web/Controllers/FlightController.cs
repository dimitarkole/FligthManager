namespace FlightManager.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FlightManager.Common;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.Infrastucture.Routes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// This class controls the Views releated to Flight.
    /// </summary>
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        /// <summary>
        /// This method calls the "All Flights" View.
        /// </summary>
        public IActionResult All(int page = GlobalConstants.DefaultPage, int itemsPerPage = GlobalConstants.DefaultItemPerPage)
        {
            var result = this.flightService.All(page, itemsPerPage);
            var routeString = new RouteString(
                nameof(FlightController),
                nameof(this.All) + "/");
            result.PaginationData.Url = routeString.AppendPaginationPlaceholder();
            result.PaginationData.Url = routeString.AppendItemPerPagePlaceholder();
            return this.View(result);
        }

        /// <summary>
        /// This method calls the "Details of certain Flight" View.
        /// </summary>
        [Authorize]
        [HttpGet(nameof(Details) + "/{id}")]
        public IActionResult Details(int id, int page = GlobalConstants.DefaultPage, int itemsPerPage = GlobalConstants.DefaultItemPerPage)
        {
            var result = this.flightService.GetById(id, page, itemsPerPage);
            var routeString = new RouteString(
                nameof(FlightController),
                nameof(this.Details) + "/");
            result.Reservations.PaginationData.Url = routeString.AppendId(id);
            result.Reservations.PaginationData.Url = routeString.AppendPaginationPlaceholder();
            result.Reservations.PaginationData.Url = routeString.AppendItemPerPagePlaceholder();
            return this.View(result);
        }
    }
}
