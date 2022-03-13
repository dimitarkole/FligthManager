using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.FlightModels;
using System;
using System.Collections.Generic;

namespace FlightManager.Tests.Data
{
    /// <summary>
    /// This class has all of the testing data used to test Flight logic.
    /// </summary>
    public static class FlightTestsData
    {
        public static FlightCreateInputModel CreateModel => new FlightCreateInputModel()
        {
            Origin = "Test",
            Destination = "Test",
            AvailableBussines = 50,
            AvailableEconomy = 150,
            LandingTime = DateTime.UtcNow,
            PilotName = "Test Pilot",
            PlaneNumber = "1",
            PlaneType = "test",
            TakeOffTime = DateTime.UtcNow.AddDays(2),
        };

        public static FlightEditInputModel UpdateModel => new FlightEditInputModel()
        {
            Origin = "Updated",
            Destination = "Updated",
            AvailableBussines = 50,
            AvailableEconomy = 150,
            LandingTime = DateTime.UtcNow,
            PilotName = "Test Pilot Updated",
            PlaneNumber = "2",
            PlaneType = "test Updated",
            TakeOffTime = DateTime.UtcNow.AddDays(2),
        };

        public static int DeleteFlightId => GetFlights[0].Id;
        public static int GetAvailableBussinesTicketsFlightId => GetFlights[0].Id;
        public static int GetAvailableEconomyTickets => GetFlights[0].Id;
        public static int GetFlightId => GetFlights[0].Id;

        public static List<Flight> GetFlights => new List<Flight>()
        {
            new Flight(){Id = 1, DestinationId = Locations[0].Id, PilotName = "Test Pilot 1", AvailableBussines= 20, AvailableEconomy = 50, LandingTime = DateTime.UtcNow, OriginId = Origins[0].Id, PlaneNumber ="Test Plane Number 1", PlaneType= "Test Plane type 1", TakeOffTime = DateTime.UtcNow, Reservations = new List<Reservation>() },
            new Flight(){Id = 2, DestinationId = Locations[1].Id, PilotName = "Test Pilot 2", AvailableBussines= 25, AvailableEconomy = 30, LandingTime = DateTime.UtcNow, OriginId = Origins[0].Id, PlaneNumber ="Test Plane Number 2", PlaneType= "Test Plane type 2", TakeOffTime = DateTime.UtcNow, Reservations = new List<Reservation>() },
            new Flight(){Id = 3, DestinationId = Locations[0].Id, PilotName = "Test Pilot 3", AvailableBussines= 120, AvailableEconomy = 50, LandingTime = DateTime.UtcNow, OriginId = Origins[1].Id, PlaneNumber ="Test Plane Number 3", PlaneType= "Test Plane type 3", TakeOffTime = DateTime.UtcNow, Reservations = new List<Reservation>() },
            new Flight(){Id = 4, DestinationId = Locations[2].Id, PilotName = "Test Pilot 4", AvailableBussines= 125, AvailableEconomy = 230, LandingTime = DateTime.UtcNow, OriginId = Origins[2].Id, PlaneNumber ="Test Plane Number 4", PlaneType= "Test Plane type 4", TakeOffTime = DateTime.UtcNow, Reservations = new List<Reservation>() },
        };

        public static List<Location> Locations => new List<Location>()
        {
            new Location() {Id = 1, Name = "Test 1",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 2, Name = "Test 2",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 3, Name = "Test 3",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 4, Name = "Test 4",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 5, Name = "Test 5",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 6, Name = "Test 6",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
        };

        public static List<Location> Origins => new List<Location>()
        {
            new Location() {Id = 101, Name = "Test 1",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 102, Name = "Test 2",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 103, Name = "Test 3",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 104, Name = "Test 4",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 105, Name = "Test 5",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
            new Location() {Id = 106, Name = "Test 6",OriginFlights = new List<Flight>(), DestinationFlights = new List<Flight>()},
        };
    }
}
