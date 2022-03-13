namespace FlightManager.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.FlightModels;

    public interface IFlightService
    {
        Task Create(FlightCreateInputModel model);

        Task Update(FlightEditInputModel model, int id);

        FlightAllViewModel All(int page, int entitesPerPage);

        FlightDetailsViewModel GetById(int id, int page, int entitesPerPage);

        FlightEditInputModel GetByIdForEdit(int id);

        Task Delete(int id);

        int AvailableEconomyTickets(int flightId);

        int AvailableBussinesTickets(int flightId);

        Task UpdateAvailableTickets(int flightId, int ecenomyTickets, int bussinesTickets);
    }
}
