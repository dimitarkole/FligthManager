namespace FlightManager.Web.Areas.Administration.Controllers
{
    using FlightManager.Common;
    using FlightManager.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.Roles.Administrator)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
