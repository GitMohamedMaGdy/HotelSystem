using HotelSystem.Logic;
using HotelSystem.Models;
using HotelSystem.ViewModels;
using System.Web.Mvc;

namespace HotelSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly HotelDBEntities dbContext;
        private readonly BookingLogic _bookingLogic;

        // GET: Room

        public BookingController()
        {
            dbContext = new HotelDBEntities();
            _bookingLogic = new BookingLogic();
        }


        public ActionResult Index()
        {
            return View(_bookingLogic.GetBookingVmWithFreeRooms());
        }

        [HttpPost]
        public ActionResult AddBooking(BookingViewModel bookingViewModel)
        {
            if (_bookingLogic.AddBooking(bookingViewModel) != null)
                return Json(new { message = "Booking is done successfully", success = true }, JsonRequestBehavior.AllowGet);
            return Json(new { message = "Failed to add Booking", success = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            return PartialView("_BookingHistoryPartial", _bookingLogic.GetAllBookings());
        }



    }
}