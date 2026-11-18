using HotelApp.Domain.Entities;

namespace HotelApp.Application.Services
{
    public interface IReservationService
    {
        Task CreateReservation(Reservation reservation);
    }
}