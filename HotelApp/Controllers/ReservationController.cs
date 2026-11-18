using HotelApp.Application.Services;
using HotelApp.Domain.Entities;
using HotelApp.Models;
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

        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            var model = new CreateReservationViewModel();
            await PopulateRoomsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel model)
        {
            var rooms = await _reservationService.GetRoomsAsync();
            model.SelectedRoomIds = model.SelectedRoomIds.Distinct().ToList();

            if (model.SelectedRoomIds.Count == 0)
            {
                ModelState.AddModelError(nameof(model.SelectedRoomIds), "Select at least one cottage.");
            }

            var selectedRooms = rooms
                .Where(room => model.SelectedRoomIds.Contains(room.Id))
                .ToList();

            if (selectedRooms.Count != model.SelectedRoomIds.Count)
            {
                ModelState.AddModelError(nameof(model.SelectedRoomIds), "One or more selected cottages are not available.");
            }

            if (model.StartDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.StartDate), "The check-in date cannot be in the past.");
            }

            if (model.EndDate.Date <= model.StartDate.Date)
            {
                ModelState.AddModelError(nameof(model.EndDate), "The check-out date must be after the check-in date.");
            }

            if (selectedRooms.Count > 0 && model.TotalGuests > selectedRooms.Sum(room => room.Capacity))
            {
                ModelState.AddModelError(nameof(model.TotalGuests), "The selected cottages do not have enough combined capacity.");
            }

            if (!ModelState.IsValid)
            {
                SetRoomOptions(model, rooms);
                return View(model);
            }

            var booking = new Booking
            {
                GuestName = model.GuestName.Trim(),
                GuestEmail = model.GuestEmail.Trim(),
                GuestPhone = model.GuestPhone?.Trim(),
                TimeFrame = new DateRange
                {
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate.Date
                },
                TotalGuests = model.TotalGuests,
                IsConfirmed = false,
                Reservations = selectedRooms.Select(room => new Reservation
                {
                    RoomId = room.Id
                }).ToList()
            };

            await _reservationService.CreateBookingAsync(booking);
            TempData["BookingSubmitted"] = true;
            return RedirectToAction(nameof(CreateReservation));
        }

        private async Task PopulateRoomsAsync(CreateReservationViewModel model)
        {
            var rooms = await _reservationService.GetRoomsAsync();
            SetRoomOptions(model, rooms);
        }

        private static void SetRoomOptions(CreateReservationViewModel model, IReadOnlyList<Room> rooms)
        {
            model.Rooms = rooms.Select(room => new RoomOptionViewModel
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsSelected = model.SelectedRoomIds.Contains(room.Id)
            }).ToList();
        }
    }
}
