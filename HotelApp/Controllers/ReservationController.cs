using HotelApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Domain.Entities.Reservation reservation)
        {
            await _reservationService.CreateReservation(reservation);

            return RedirectToAction(nameof(CreateReservation)); // to do 
        }
    }
}
