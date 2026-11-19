using HotelApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task Create(Entities.Reservation reservation);
        Task<List<Room>> GetRooms();
    }
}
