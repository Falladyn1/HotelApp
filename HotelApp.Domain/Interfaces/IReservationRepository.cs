using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<IReadOnlyList<Domain.Entities.Room>> GetRoomsAsync();
        Task CreateBookingAsync(Domain.Entities.Booking booking);
    }
}
