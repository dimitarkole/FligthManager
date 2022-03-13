namespace FlightManager.Data.Models
{
    using System;

    using FlightManager.Data.Models.Enums;

    public class Passanger
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PersonalNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public TicketType TicketType { get; set; }

        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
