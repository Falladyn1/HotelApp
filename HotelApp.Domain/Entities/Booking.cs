using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestName { get; set; } = default!;
        public string GuestEmail { get; set; } = default!;
        public string? GuestPhone { get; set; }
        public DateRange TimeFrame { get; set; } = default!;
        public int TotalGuests { get; set; }
        public bool IsConfirmed { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
