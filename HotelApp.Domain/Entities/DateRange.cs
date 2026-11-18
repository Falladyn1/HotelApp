using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Domain.Entities
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TotalNights => (EndDate - StartDate).Days;
    }
}
