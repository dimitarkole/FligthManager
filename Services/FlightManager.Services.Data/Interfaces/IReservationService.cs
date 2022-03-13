using FlightManager.Web.ViewModels.ReservationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Services.Data.Interfaces
{
    public interface IReservationService
    {
        Task Create(ReservationCreateInputModel model);

        T GetById<T>(int id);
    }
}
