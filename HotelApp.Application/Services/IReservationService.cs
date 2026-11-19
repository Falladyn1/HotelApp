using HotelApp.Domain.Entities;

namespace HotelApp.Application.Services
{
    public interface IReservationService
    {
        Task Create(Reservation reservation);
        Task<List<Room>> GetRooms();
    }
}