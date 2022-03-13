using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.ReservationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Tests.Data
{
    /// <summary>
    /// This class has all of the testing data used to test Reservation logic.
    /// </summary>
    public static class ReservationTestsData
    {
        public static ReservationCreateInputModel CreateModel => new ReservationCreateInputModel
        {
                FlightId = 1,
                Client = new ReservationClientInputModel() 
                {
                    Email = "asd@abv.bg",
                },
                Passengers = new List<ReservationPassangerInputModel>()
                {
                    new ReservationPassangerInputModel()
                    {
                        Email = "asd@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Bussines,
                        Name = "Gosho",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                    new ReservationPassangerInputModel()
                    {
                        Email = "asd@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Bussines,
                        Name = "Stoqn",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                    new ReservationPassangerInputModel()
                    {
                        Email = "dsa@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Economy,
                        Name = "Pesho",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                }
        };

        public static int ReservationId = 1;
        public static List<Flight> GetFlights => FlightTestsData.GetFlights;
        public static List<Location> Locations => FlightTestsData.Locations;
        public static List<Location> Origins => FlightTestsData.Origins;

        public static List<Reservation> GetReservations => new List<Reservation>()
        {
            new Reservation()
            {
                Id = 1,
                FlightId = 1,
                ClientId = "1",
                Passengers = new List<Passanger>()
                {
                    new Passanger()
                    {
                        Id = "2",
                        Email = "asd@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Bussines,
                        Name = "Gosho",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                    new Passanger()
                    {
                        Id = "3",
                        Email = "asd@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Bussines,
                        Name = "Stoqn",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                    new Passanger()
                    {
                        Id = "4",
                        Email = "dsa@abv.bg",
                        TicketType = FlightManager.Data.Models.Enums.TicketType.Economy,
                        Name = "Pesho",
                        MiddleName = "Petrov",
                        Surname = "Petrov",
                        Nationality = "bulgarian",
                        PersonalNumber = "1234567890",
                        PhoneNumber = "1234567890",
                    },
                }
            }
        };
    }
}
