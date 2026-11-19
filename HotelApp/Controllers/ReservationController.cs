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

        public async Task<ActionResult> Create()
        {
            ViewBag.Rooms = await _reservationService.GetRooms();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Domain.Entities.Reservation reservation)
        {
            await _reservationService.Create(reservation);
            return RedirectToAction(nameof(Create)); //to do
        }
    }
}
