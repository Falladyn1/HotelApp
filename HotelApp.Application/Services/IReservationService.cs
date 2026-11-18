using HotelApp.Domain.Entities;

namespace HotelApp.Application.Services
{
    public interface IReservationService
    {
        Task<IReadOnlyList<Room>> GetRoomsAsync();
        Task CreateBookingAsync(Booking booking);
    }
}