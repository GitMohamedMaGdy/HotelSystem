using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelSystem.ViewModels
{
    public class BookingViewModel
    {
        [Display(Name = "customer name")]
        [Required(ErrorMessage = "you must give customer name")]
        public string CustomerName { get; set; }
        [Display(Name = "customer address")]
        [Required(ErrorMessage = "you must give address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "customer phone")]
        [Required(ErrorMessage = "you must give phone")]
        public string CustomerPhone { get; set; }
        [Display(Name = "from")]
        [Required(ErrorMessage = "you must give date to start booking")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime BookingFrom { get; set; }
        [Display(Name = "to")]
        [Required(ErrorMessage = "you must give date to end booking")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime BookingTo { get; set; }
        [Display(Name = "room number")]
        [Required(ErrorMessage = "you must assign room")]
        public int AssignRoomId { get; set; }
        [Display(Name = "guests")]
        [Required(ErrorMessage = "you must give number of guests")]

        public int NoOfMembers { get; set; }
        [Display(Name = "total ")]
        public decimal totalAmount { get; set; }
        public IEnumerable<SelectListItem> listOfRooms { get; set; }
    }
}