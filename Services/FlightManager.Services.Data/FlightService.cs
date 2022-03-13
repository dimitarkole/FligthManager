namespace FlightManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FlightManager.Data;
    using FlightManager.Data.Models;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.Services.Mapping;
    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.FlightModels;
    using FlightManager.Common.Extensions;
    using FlightManager.Web.ViewModels.PaginationModels;
    using FlightManager.Web.ViewModels.ReservationModels;

    /// <summary>
    /// This class does all of the logic behind the Flights.
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public FlightService(ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public FlightAllViewModel All(int page, int entitesPerPage)
        {
            var flights = this.context.Flights
               .To<FlightViewModel>();

            var paginationData = new PaginationData()
            {
                CurrentPage = page,
                ItemsPerPage = entitesPerPage,
                NumberOfPages = this.GetItemsPagesCount(flights.Count(), entitesPerPage),
            };

            return new FlightAllViewModel()
            {
                Flight = flights
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                PaginationData = paginationData,
            };
        }

        public int AvailableBussinesTickets(int flightId) =>
             this.context.Flights.Where(f => f.Id == flightId)
            .Select(f => f.AvailableBussines)
            .FirstOrDefault();

        public int AvailableEconomyTickets(int flightId) =>
           this.context.Flights.Where(f => f.Id == flightId)
            .Select(f => f.AvailableEconomy)
            .FirstOrDefault();

        public async Task Create(FlightCreateInputModel model)
        {
            Flight flight = model.To<Flight>();
            flight.Origin = GetFlightLocation(model.Origin);
            flight.Destination = GetFlightLocation(model.Destination);
            await context.Flights.AddAsync(flight);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Flight flight = this.context.Flights.Find(id);
            IQueryable<Reservation> reservations = this.context.Reservations.Where(r => r.FlightId == id);
            IQueryable<Passanger> passengers = reservations.SelectMany(r => r.Passengers);
            this.context.Reservations.RemoveRange(reservations);
            this.context.Passangers.RemoveRange(passengers);
            this.context.Flights.Remove(flight);
            await this.context.SaveChangesAsync();
        }

        public FlightDetailsViewModel GetById(int id, int page, int entitesPerPage)
        {
            var result = this.context.Flights
                .Where(f => f.Id == id)
                .To<FlightDetailsViewModel>()
                .FirstOrDefault();

            var reservation = this.context.Reservations
                .Where(r => r.FlightId == id)
                .OrderByDescending(r => r.CreatedOn)
                .To<ReservationViewModel>();

            var paginationData = new PaginationData()
            {
                CurrentPage = page,
                ItemsPerPage = entitesPerPage,
                NumberOfPages = this.GetItemsPagesCount(reservation.Count(), entitesPerPage),
            };

            var reservations = new ReservationAllViewModel()
            {
                Reservations = reservation
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                PaginationData = paginationData,
            };

            result.Reservations = reservations;

            return result;
        }

        public FlightEditInputModel GetByIdForEdit(int id)
           => this.context.Flights
                .Where(f => f.Id == id)
                .To<FlightEditInputModel>()
                .FirstOrDefault();

        public async Task Update(FlightEditInputModel model, int id)
        {
            Flight flight = this.context.Flights.Find(id);
            flight.LandingTime = model.LandingTime;
            flight.Origin = this.GetFlightLocation(model.Origin);
            flight.Destination = this.GetFlightLocation(model.Destination);
            flight.PilotName = model.PilotName;
            flight.PlaneNumber = model.PlaneNumber;
            flight.PlaneType = model.PlaneType;
            flight.TakeOffTime = model.TakeOffTime;
            flight.AvailableBussines = model.AvailableBussines;
            flight.AvailableEconomy = model.AvailableEconomy;

            this.context.Flights.Update(flight);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAvailableTickets(int flightId, int ecenomyTickets, int bussinesTickets)
        {
            Flight flight = this.context.Flights.Find(flightId);
            flight.AvailableEconomy -= ecenomyTickets;
            flight.AvailableBussines -= bussinesTickets;

            context.Flights.Update(flight);
            await context.SaveChangesAsync();
        }

        private Location GetFlightLocation(string locationName)
        {
            Location location = context.Locations.FirstOrDefault(l => l.Name == locationName);
            if (location == null)
            {
                location = new Location { Name = locationName };
            }

            return location;
        }

        private int GetItemsPagesCount(int entityCount, int entitesPerPage)
         => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
