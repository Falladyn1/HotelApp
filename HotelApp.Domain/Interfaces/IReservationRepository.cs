using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task CreateReservation(Domain.Entities.Reservation reservation);
    }
}
