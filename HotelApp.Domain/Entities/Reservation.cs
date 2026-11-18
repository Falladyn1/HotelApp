using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = default!;
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
    }
}
