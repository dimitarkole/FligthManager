namespace FlightManager.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FlightManager.Common;
    using FlightManager.Common.Extensions;
    using FlightManager.Data.Models;
    using FlightManager.Services.Mapping;
    using FlightManager.Tests.Data;
    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.FlightModels;
    using FlightManager.Web.ViewModels.PaginationModels;
    using FlightManager.Web.ViewModels.ReservationModels;
    using Xunit;

    /// <summary>
    /// This class is responsible for testing the Flight logic that the system contains.
    /// </summary>
    public class FligthServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateFligth_WithValidData_ShouldWorkCorrect()
        {
            var flightService = await this.CreateFlightService(new List<Flight>());
            var model = FlightTestsData.CreateModel;

            await flightService.Create(model);

            Assert.True(this.context.Flights.Any(f => f.LandingTime == model.LandingTime
                && f.Origin.Name == model.Origin
                && f.PilotName == model.PilotName
                && f.PlaneNumber == model.PlaneNumber
                && f.PlaneType == model.PlaneType
                && f.TakeOffTime == model.TakeOffTime
                && f.AvailableBussines == model.AvailableBussines
                && f.AvailableEconomy == model.AvailableEconomy
                && f.Destination.Name == model.Destination));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllFligths_WithValidData_ShouldWorkCorrect(int page)
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var expextedResult = this.context.Flights
                    .To<FlightViewModel>();
            var entitesPerPage = GlobalConstants.DefaultItemPerPage;
            var result = flightService.All(page, entitesPerPage);
            var countPages = this.GetItemsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckFligthAllViewModelsIsEqual(page, countPages, expectedMusicsList, result);
        }

        [Fact]
        public async Task DeleteFligth_WithValidData_ShouldWorkCorrect()
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var flightId = FlightTestsData.DeleteFlightId;

            await flightService.Delete(flightId);

            Assert.False(this.context.Flights.Any(c => c.Id == flightId));
        }

        [Fact]
        public async Task GetAvailableBussinesTickets_WithValidData_ShouldWorkCorrect()
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var flightId = FlightTestsData.GetAvailableBussinesTicketsFlightId;
            var expectedResult = this.context.Flights.Where(f => f.Id == flightId)
                .Select(f => f.AvailableBussines)
                .FirstOrDefault();

            var availableBussinesTickets = flightService.AvailableBussinesTickets(flightId);

            Assert.Equal(expectedResult, availableBussinesTickets);
        }

        [Fact]
        public async Task GetAvailableEconomyTickets_WithValidData_ShouldWorkCorrect()
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var flightId = FlightTestsData.GetAvailableEconomyTickets;
            var expectedResult = this.context.Flights.Where(f => f.Id == flightId)
                .Select(f => f.AvailableEconomy)
                .FirstOrDefault();

            var availableEconomyTickets = flightService.AvailableEconomyTickets(flightId);

            Assert.Equal(expectedResult, availableEconomyTickets);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetById_WithValidData_ShouldWorkCorrect(int page)
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var entitesPerPage = GlobalConstants.DefaultItemPerPage;
            var flightId = FlightTestsData.GetFlightId;

            FlightDetailsViewModel expextedResult = this.GetByIdExpectedResult(page, entitesPerPage, flightId);

            var result = flightService.GetById(flightId, page, entitesPerPage);

            var countPages = this.GetItemsPagesCount(expextedResult.Reservations.Reservations.Count(), entitesPerPage);

            expextedResult.Reservations.Reservations = expextedResult.Reservations.Reservations.GetPage(page, entitesPerPage)
               .ToList();

            CheckFlightDetailsViewModelIsEqual(page, countPages, expextedResult, result);
        }

        [Theory]
        [InlineData(1, 10, 5)]
        [InlineData(2, 3, 50)]
        public async Task UpdateAvailableTickets_WithValidData_ShouldWorkCorrect(int flightId, int ecenomyTickets, int bussinesTickets)
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            Flight flight = this.context.Flights.Find(flightId);
            var expectedAvailableBussines = flight.AvailableBussines - bussinesTickets;
            var expectedAvailableEconomy = flight.AvailableEconomy - ecenomyTickets;

            var result = flightService.UpdateAvailableTickets(flightId, ecenomyTickets, bussinesTickets);

            Assert.True(this.context.Flights.Any(f => f.Id == flightId
                && f.AvailableBussines == expectedAvailableBussines
                && f.AvailableEconomy == expectedAvailableEconomy));
        }

        [Fact]
        public async Task UpdateFlight_WithValidData_ShouldWorkCorrect()
        {
            var getFlight = FlightTestsData.GetFlights;
            var flightService = await this.CreateFlightService(getFlight);
            var flightId = FlightTestsData.GetFlightId;
            var updateModel = FlightTestsData.UpdateModel;

            var result = flightService.Update(updateModel, flightId);

            Assert.True(this.context.Flights.Any(f => f.LandingTime == updateModel.LandingTime
               && f.Origin.Name == updateModel.Origin
               && f.PilotName == updateModel.PilotName
               && f.PlaneNumber == updateModel.PlaneNumber
               && f.PlaneType == updateModel.PlaneType
               && f.TakeOffTime == updateModel.TakeOffTime
               && f.AvailableBussines == updateModel.AvailableBussines
               && f.AvailableEconomy == updateModel.AvailableEconomy
               && f.Destination.Name == updateModel.Destination));
        }

        private static void CheckFlightDetailsViewModelIsEqual(int page, int countPages, FlightDetailsViewModel expextedResult, FlightDetailsViewModel result)
        {
            Assert.Equal(result.Reservations.PaginationData.CurrentPage, page);
            Assert.Equal(result.Reservations.PaginationData.NumberOfPages, countPages);
            Assert.True(CheckFlightDetailsViewModelIsEqual(expextedResult, result));
            CheckReservationViewModelIsEqual(expextedResult.Reservations.Reservations.ToList(), result.Reservations.Reservations.ToList());
        }

        private static void CheckReservationViewModelIsEqual(List<ReservationViewModel> expextedResults, List<ReservationViewModel> results)
        {
            Assert.Equal(expextedResults.Count(), results.Count());

            for (int i = 0; i < expextedResults.Count(); i++)
            {
                Assert.True(CheckReservationViewModelIsEqual(expextedResults[i], results[i]));
            }
        }

        private static bool CheckReservationViewModelIsEqual(ReservationViewModel expextedResult, ReservationViewModel result)
            => expextedResult.ClientEmail == result.ClientEmail
                && expextedResult.CreatedOn == result.CreatedOn
                && expextedResult.Id == result.Id
                && expextedResult.TicketsCount == result.TicketsCount;

        private static bool CheckFlightDetailsViewModelIsEqual(FlightDetailsViewModel expextedResult, FlightDetailsViewModel result)
            => expextedResult.Origin == result.Origin
                && expextedResult.Id == result.Id
                && expextedResult.PilotName == result.PilotName
                && expextedResult.PlaneNumber == result.PlaneNumber
                && expextedResult.PlaneType == result.PlaneType
                && expextedResult.TakeOffTime == result.TakeOffTime
                && expextedResult.AvailableBussines == result.AvailableBussines
                && expextedResult.AvailableEconomy == result.AvailableEconomy
                && expextedResult.Destination == result.Destination
                && expextedResult.Duration == result.Duration;

        private static void CheckFligthAllViewModelsIsEqual(int page, int countPages, List<FlightViewModel> expextedResult, FlightAllViewModel result)
        {
            Assert.Equal(result.PaginationData.CurrentPage, page);
            Assert.Equal(result.PaginationData.NumberOfPages, countPages);
            var resultFligths = result.Flight.ToList();
            Assert.Equal(resultFligths.Count, expextedResult.Count);

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedFligth = expextedResult[i];
                var resultFligth = resultFligths[i];
                Assert.True(CheckFligthViewModelIsEqual(expectedFligth, resultFligth));
            }
        }

        private static bool CheckFligthViewModelIsEqual(FlightViewModel expectedFligth, FlightViewModel resultFligth)
            => expectedFligth.Destination == resultFligth.Destination
                && expectedFligth.Duration == resultFligth.Duration
                && expectedFligth.Id == resultFligth.Id
                && expectedFligth.Origin == resultFligth.Origin
                && expectedFligth.PlaneNumber == resultFligth.PlaneNumber
                && expectedFligth.TakeOffTime == resultFligth.TakeOffTime;

        private int GetItemsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private FlightDetailsViewModel GetByIdExpectedResult(int page, int entitesPerPage, int flightId)
        {
            var result = this.context.Flights
                .Where(f => f.Id == flightId)
                .To<FlightDetailsViewModel>()
                .FirstOrDefault();

            var reservation = this.context.Reservations
                .Where(r => r.FlightId == flightId)
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

        private async Task<FlightService> CreateFlightService(List<Flight> flights)
        {
            await this.context.Flights.AddRangeAsync(flights);
            await this.context.Locations.AddRangeAsync(FlightTestsData.Locations);
            await this.context.Locations.AddRangeAsync(FlightTestsData.Origins);
            await this.context.SaveChangesAsync();
            var service = new FlightService(this.context, new PaginationService());

            return service;
        }
    }
}
