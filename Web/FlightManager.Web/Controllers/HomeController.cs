namespace FlightManager.Web.Controllers
{
    using System.Diagnostics;

    using FlightManager.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.Common;
    using FlightManager.Web.ViewModels.FlightModels;
    using FlightManager.Web.Infrastucture.Routes;

    /// <summary>
    /// This class controls the Views releated to Homepage.
    /// </summary>
    public class HomeController : BaseController
    {

        private readonly IFlightService flightService;

        public HomeController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        /// <summary>
        /// This method calls the View that displays the Homepage.
        /// </summary>
        public ActionResult<FlightAllViewModel> Index(int page = GlobalConstants.DefaultPage, int itemsPerPage = GlobalConstants.DefaultItemPerPage)
        {
            var result = this.flightService.All(page, itemsPerPage);
            var routeString = new RouteString(
                nameof(HomeController),
                "");
            result.PaginationData.Url = routeString.AppendPaginationPlaceholder();
            result.PaginationData.Url = routeString.AppendItemPerPagePlaceholder();
            return this.View(result);
        }

        /// <summary>
        /// This method calls the Privacy View that displays the Privacy settings.
        /// </summary>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// This method calls the Error View that displays the Error in detail if it encounters any.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
