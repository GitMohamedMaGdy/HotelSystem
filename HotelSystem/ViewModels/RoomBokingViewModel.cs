using System;

namespace HotelSystem.ViewModels
{
    public class RoomBokingViewModel
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfMembers { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public string RoomNumber { get; set; }
        public int NumberOfDays { get; set; }
        public decimal RoomPrice { get; set; }

        public decimal totalAmount { get; set; }
    }
}