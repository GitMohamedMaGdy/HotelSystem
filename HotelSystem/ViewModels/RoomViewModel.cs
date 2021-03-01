using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace HotelSystem.ViewModels
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        [Display(Name = "Room No")]
        [Required(ErrorMessage = "Room number must be exist")]
        public string RoomNumber { get; set; }
        [Display(Name = "Image")]
        public string RoomImage { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "you must give a price for the room")]
        [Range(500, 10000, ErrorMessage = "price must be equal or greater than {1}")]

        public decimal RoomPrice { get; set; }
        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "you must select status of this room")]

        public int BookingStatusId { get; set; }
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "you must select type of this room")]
        public int RoomTypeId { get; set; }
        [Display(Name = "Room Capacity")]
        [Required(ErrorMessage = "you must give capacity to this room")]
        [Range(1, 8, ErrorMessage = "capacity must be equal or greater than {1}")]
        public int RoomCapacity { get; set; }
        [Display(Name = "Room Description")]
        public string RoomDescription { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<SelectListItem> ListOfBookingStatus { get; set; }
        public List<SelectListItem> ListOfRoomTypes { get; set; }
    }
}