using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class CreateReservationViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Guest name")]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(320)]
        [Display(Name = "Email")]
        public string GuestEmail { get; set; } = string.Empty;

        [Phone]
        [StringLength(50)]
        [Display(Name = "Phone")]
        public string? GuestPhone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-in date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-out date")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        [Range(1, int.MaxValue)]
        [Display(Name = "Total guests")]
        public int TotalGuests { get; set; }

        public List<int> SelectedRoomIds { get; set; } = new();

        public List<RoomOptionViewModel> Rooms { get; set; } = new();
    }

    public class RoomOptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsSelected { get; set; }
    }
}
