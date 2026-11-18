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
        public Task<IReadOnlyList<Domain.Entities.Room>> GetRoomsAsync()
        {
            return _hotelReservationRepository.GetRoomsAsync();
        }

        public Task CreateBookingAsync(Domain.Entities.Booking booking)
        {
            return _hotelReservationRepository.CreateBookingAsync(booking);
        }
    }
}
