namespace FlightManager.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FlightManager.Common;
    using FlightManager.Data.Models;
    using FlightManager.Data.Models.Enums;
    using FlightManager.Services.Data.Interfaces;
    using FlightManager.Services.Mapping;
    using FlightManager.Services.Messaging;
    using FlightManager.ViewModels.FlightModels;
    using FlightManager.Web.Infrastucture.Extensions;
    using FlightManager.Web.ViewModels.ReservationModels;
    using Microsoft.AspNetCore.Mvc;
    using static FlightManager.Common.GlobalConstants;

    /// <summary>
    /// This class controls the Views the Views releated to TicketReservation
    /// </summary>
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IFlightService flightService;
        private readonly IEmailSender emailSender;

        public ReservationController(
            IReservationService reservationService,
            IFlightService flightService,
            IEmailSender emailSender)
        {
            this.reservationService = reservationService;
            this.flightService = flightService;
            this.emailSender = emailSender;
        }

        public IActionResult Create(int flightId)
        {
            var reservation = new ReservationCreateInputModel();
            reservation.Passengers.Add(new ReservationPassangerInputModel());
            reservation.FlightId = flightId;
            return View(reservation);
        }

        /// <summary>
        /// This method calls the Create View which displays the page that lets you create a reservation.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateInputModel model)
        {
            int ecenomyTickets = model.Passengers.Count(p => p.TicketType == TicketType.Economy);
            int bussinesTickets = model.Passengers.Count(p => p.TicketType == TicketType.Bussines);
            int availableEconomyTickets = flightService.AvailableEconomyTickets(model.FlightId);
            int availableBusinessTickets = flightService.AvailableBussinesTickets(model.FlightId);
            if (availableEconomyTickets < ecenomyTickets)
            {
                ModelState.AddModelError(string.Empty, $"There are only {availableEconomyTickets} economy tickets left.");
            }
            if (availableBusinessTickets < bussinesTickets)
            {
                ModelState.AddModelError(string.Empty, $"There are only {availableBusinessTickets} business tickets left.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await reservationService.Create(model);
            await flightService.UpdateAvailableTickets(model.FlightId, ecenomyTickets, bussinesTickets);

            var flight = flightService.GetById(model.FlightId, GlobalConstants.DefaultPage,GlobalConstants.DefaultItemPerPage);
            await SendConfirmationEmailsToPassengers(model.Passengers, flight);
            await SendEmailToClient(model.Client.Email, flight, model.Passengers);

            return Redirect("/");
        }

        /// <summary>
        /// This method calls the Details View that lets you see the details of your reservation.
        /// </summary>
        public IActionResult Details(int id)
        {
            ReservationDetailsViewModel model = reservationService.GetById<ReservationDetailsViewModel>(id);
            return View(model);
        }

        /// <summary>
        /// This method calls the send email function and sends a confirmation email to the Passenger.
        /// </summary>
        private async Task SendConfirmationEmailsToPassengers(IEnumerable<ReservationPassangerInputModel> passengers, FlightViewModel flight)
        {
            const string EmailTemplateName = "PassengerConfirmationEmail";
            var model = new ReservationPassengerConfirmationEmailViewModel
            {
                Flight = flight
            };

            foreach (ReservationPassangerInputModel passenger in passengers)
            {
                model.Passenger = passenger.To<ReservationPassangerInputModel>();
                string body = await this.RenderViewAsync(EmailTemplateName, model, true);
                //Add smtp server credentials in appsettings.json and then uncomment next line
                await emailSender.SendEmailAsync(passenger.Email, EmailSubjects.PassengerConfirmationEmail, body);
            }
        }

        /// <summary>
        /// This method calls the send email function and sends a confirmation email to the Client.
        /// </summary>
        private async Task SendEmailToClient(string email, FlightViewModel flight, IEnumerable<ReservationPassangerInputModel> passengers)
        {
            const string EmailTemplateName = "ClientConfirmationEmail";
            var model = new ReservationClientConfirmationEmailViewModel
            {
                Flight = flight,
                Passengers = passengers.Select(p => p.To<ReservationPassengerViewModel>())
            };

            string body = await this.RenderViewAsync(EmailTemplateName, model, true);
            //Add smtp server credentials in appsettings.json and then uncomment next line
            await emailSender.SendEmailAsync(email, EmailSubjects.ClientConfirmationEmail, body);
        }
    }
}
