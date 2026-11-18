using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
