using FlightManager.Common;
using FlightManager.Data.Models;
using FlightManager.Tests.Data;
using FlightManager.Web.ViewModels.ReservationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlightManager.Services.Data.Tests
{
    /// <summary>
    /// This class is responsible for testing the Reservation logic that the system contains.
    /// </summary>
    public class ReservationServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateFligth_WithValidData_ShouldWorkCorrect()
        {
            var reservationService = await this.CreateReservationService(new List<Reservation>());
            var model = ReservationTestsData.CreateModel;

            await reservationService.Create(model);

            Assert.True(this.context.Reservations.Any(r => r.FlightId == model.FlightId));
        }

        //[Fact]
        //public async Task GetById_WithValidData_ShouldWorkCorrect()
        //{
        //    var getReservations = ReservationTestsData.GetReservations;
        //    var reservationService = await this.CreateReservationService(getReservations);
        //    var reservationId = ReservationTestsData.ReservationId;

        //    var result = reservationService.GetById<ReservationDetailsViewModel>(reservationId);
        //    var expectedClientEmail = this.context.Reservations.Where(r => r.Id == reservationId)
        //        .Select(r => r.Client.Email)
        //        .FirstOrDefault();
        //    Assert.Equal(result.ClientEmail, expectedClientEmail);
        //}

        private async Task<ReservationService> CreateReservationService(List<Reservation> reservations)
        {
            await this.context.Reservations.AddRangeAsync(reservations);
            await this.context.Locations.AddRangeAsync(ReservationTestsData.Locations);
            await this.context.Locations.AddRangeAsync(ReservationTestsData.Origins);
            await this.context.Flights.AddRangeAsync(ReservationTestsData.GetFlights);
            await this.context.SaveChangesAsync();
            var service = new ReservationService(this.context);

            return service;
        }
    }
}
