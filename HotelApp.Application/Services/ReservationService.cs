using HotelApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Create(Domain.Entities.Reservation reservation)
        {
            await _reservationRepository.Create(reservation);
        }

        public async Task<List<Domain.Entities.Room>> GetRooms()
        {
            return await _reservationRepository.GetRooms();
        }
    }
}
