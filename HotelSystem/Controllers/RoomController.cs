using HotelSystem.Logic;
using HotelSystem.Models;
using HotelSystem.ViewModels;
using System.Web.Mvc;

namespace HotelSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly HotelDBEntities dbContext;
        private RoomLogic _roomLogic;

        // GET: Room

        public RoomController()
        {
            dbContext = new HotelDBEntities();
            _roomLogic = new RoomLogic();
        }

        public ActionResult Index()
        {
            return View(_roomLogic.GetRoomTypesAndBookingTypes());
        }

        [HttpPost]
        public ActionResult Index(RoomViewModel roomViewModel)
        {

            if (roomViewModel.RoomId == 0)
            {
                if (_roomLogic.AddRoom(roomViewModel) != null)
                    return Json(new { success = true, type = "add", message = "Room Added successfull" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to Add Room" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (_roomLogic.EditRoom(roomViewModel))
                    return Json(new { success = true, type = "update", message = "Room Updated successfull" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Failed to update room" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteRoom(int roomId)
        {
            if (_roomLogic.DeleteRoom(roomId))
                return Json(new { success = true, message = "Record Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = false, message = "Failed to delete record" }, JsonRequestBehavior.AllowGet);

        }




        public PartialViewResult GetAllRooms()
        {
            return PartialView("_RoomDetailPartial", _roomLogic.GetAllRooms());
        }

        [HttpGet]
        public JsonResult EditRoom(int roomId)
        {
            return Json(_roomLogic.GetRoom(roomId), JsonRequestBehavior.AllowGet);
        }



    }
}