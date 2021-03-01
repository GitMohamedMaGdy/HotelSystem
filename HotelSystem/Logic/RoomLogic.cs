using HotelSystem.Models;
using HotelSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSystem.Logic
{
    public class RoomLogic
    {
        HotelDBEntities dbContext;

        // GET: Room

        public RoomLogic()
        {
            dbContext = new HotelDBEntities();
        }


        public RoomViewModel GetRoomTypesAndBookingTypes()
        {
            var roomVM = new RoomViewModel();
            roomVM.ListOfBookingStatus = (from obj in dbContext.BookingStatus
                                          select new SelectListItem()
                                          {
                                              Text = obj.BookingStatusName,
                                              Value = obj.BookingStatusId.ToString()
                                          }).ToList();

            roomVM.ListOfRoomTypes = (from obj in dbContext.RoomTypes
                                      select new SelectListItem()
                                      {
                                          Text = obj.RoomTypeName,
                                          Value = obj.RoomTypeId.ToString()
                                      }).ToList();
            return roomVM;
        }

        public Room AddRoom(RoomViewModel roomViewModel)
        {
            string ImageUnique = Guid.NewGuid().ToString();
            string actualImage = ImageUnique + Path.GetExtension(roomViewModel.Image.FileName);
            roomViewModel.Image.SaveAs(HttpContext.Current.Server.MapPath("~/Images/" + actualImage));

            Room room = new Room()
            {

                RoomNumber = roomViewModel.RoomNumber,
                RoomDescription = roomViewModel.RoomDescription,
                RoomPrice = roomViewModel.RoomPrice,
                BookingStatusId = roomViewModel.BookingStatusId,
                RoomTypeId = roomViewModel.RoomTypeId,
                RoomCapacity = roomViewModel.RoomCapacity,
                RoomImage = actualImage,
                isActive = true

            };

            dbContext.Rooms.Add(room);
            if (dbContext.SaveChanges() > 0) return room;
            return null;
        }

        public Room GetRoom(int roomId)
        {
            return dbContext.Rooms.SingleOrDefault(r => r.RoomId == roomId);
        }

        public bool EditRoom(RoomViewModel newRoom)
        {
            var room = dbContext.Rooms.SingleOrDefault(r => r.RoomId == newRoom.RoomId);
            if (room.RoomImage != null)
            {
                string ImageUnique = Guid.NewGuid().ToString();
                string actualImage = ImageUnique + Path.GetExtension(newRoom.Image.FileName);
                newRoom.Image.SaveAs(HttpContext.Current.Server.MapPath("~/Images/" + actualImage));
                newRoom.RoomImage = actualImage;

            }
            room.RoomCapacity = newRoom.RoomCapacity;
            room.RoomDescription = newRoom.RoomDescription;
            room.RoomImage = newRoom.RoomImage;
            room.RoomNumber = newRoom.RoomNumber;
            room.RoomPrice = newRoom.RoomPrice;
            room.RoomTypeId = newRoom.RoomTypeId;
            room.BookingStatusId = newRoom.BookingStatusId;

            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public List<RoomDetailViewModel> GetAllRooms()
        {
            var listOfRooms = (from room in dbContext.Rooms
                               join booking in dbContext.BookingStatus
                               on room.BookingStatusId equals booking.BookingStatusId
                               join roomType in dbContext.RoomTypes
                               on room.RoomTypeId equals roomType.RoomTypeId
                               where room.isActive == true
                               select new RoomDetailViewModel()
                               {
                                   RoomNumber = room.RoomNumber,
                                   RoomCapacity = room.RoomCapacity,
                                   RoomImage = room.RoomImage,
                                   RoomDescription = room.RoomDescription,
                                   RoomPrice = room.RoomPrice,
                                   BookingStatus = booking.BookingStatusName,
                                   RoomType = roomType.RoomTypeName,
                                   RoomId = room.RoomId

                               }).ToList();

            return listOfRooms;

        }


        public bool DeleteRoom(int roomId)
        {
            Room room = GetRoom(roomId);
            room.isActive = false;
            if (dbContext.SaveChanges() > 0) return true;
            return false;
        }
    }
}