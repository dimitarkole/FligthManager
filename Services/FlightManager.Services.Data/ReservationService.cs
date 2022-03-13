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
    using FlightManager.Web.ViewModels.ReservationModels;

    /// <summary>
    /// This class does all of the logic behind the Reservations.
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext context;

        public ReservationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Create(ReservationCreateInputModel model)
        {
            Reservation reservation = model.To<Reservation>();
            Client client = GetReservationClient(model.Client.Email);
            //Check if client has already been added to the database
            if (client != null)
            {
                reservation.Client = client;
            }

            foreach (ReservationPassangerInputModel passanger in model.Passengers)
            {
                reservation.Passengers.Add(this.GetReservationPassanger(passanger));
            }

            await this.context.Reservations.AddAsync(reservation);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(int id) =>
           this.context.Reservations
            .Where(r => r.Id == id)
            .To<T>()
            .FirstOrDefault();

        public Client GetReservationClient(string clientEmail) =>
            this.context.Clients.FirstOrDefault(c => c.Email == clientEmail);

        private Passanger GetReservationPassanger(ReservationPassangerInputModel model)
        {
            //Check if passanger has already been added to the database
            Passanger passanger = context.Passangers
                .FirstOrDefault(c =>
                c.PersonalNumber == model.PersonalNumber &&
                c.Email == model.Email &&
                c.PhoneNumber == model.PhoneNumber);

            return passanger ?? model.To<Passanger>();
        }
    }
}
