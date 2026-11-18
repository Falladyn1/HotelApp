using HotelApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _hotelReservationRepository;
        public ReservationService(IReservationRepository hotelReservationRepository)
        {
            _hotelReservationRepository = hotelReservationRepository;
        }
        public async Task CreateReservation(Domain.Entities.Reservation reservation)
        {
            await _hotelReservationRepository.CreateReservation(reservation);
        }
    }
}
