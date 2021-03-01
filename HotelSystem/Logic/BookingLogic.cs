using HotelSystem.Models;
using HotelSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HotelSystem.Logic
{
    public class BookingLogic
    {
        HotelDBEntities dbContext;

        // GET: Room

        public BookingLogic()
        {
            dbContext = new HotelDBEntities();
        }

        public BookingViewModel GetBookingVmWithFreeRooms()
        {
            var bookingVM = new BookingViewModel();
            bookingVM.listOfRooms = (from room in dbContext.Rooms
                                     where room.BookingStatusId == 2
                                     select new SelectListItem()
                                     {
                                         Text = room.RoomNumber,
                                         Value = room.RoomId.ToString()
                                     }).ToList();
            bookingVM.BookingFrom = DateTime.Now;
            bookingVM.BookingTo = DateTime.Now.AddDays(1);

            return bookingVM;
        }

        public Booking AddBooking(BookingViewModel bookingViewModel)
        {
            int numberOfDays = Convert.ToInt32((bookingViewModel.BookingTo - bookingViewModel.BookingFrom).TotalDays);
            Room room = dbContext.Rooms.SingleOrDefault(m => m.RoomId == bookingViewModel.AssignRoomId);
            decimal price = room.RoomPrice;
            decimal total = price * numberOfDays;

            var roomBooking = new Booking()
            {
                BookingFrom = bookingViewModel.BookingFrom,
                BookingTo = bookingViewModel.BookingTo,
                AssignRoomId = bookingViewModel.AssignRoomId,
                CustomerAddress = bookingViewModel.CustomerAddress,
                CustomerName = bookingViewModel.CustomerName,
                CustomerPhone = bookingViewModel.CustomerPhone,
                NoOfMembers = bookingViewModel.NoOfMembers,
                totalAmount = total,

            };
            dbContext.Bookings.Add(roomBooking);
            room.BookingStatusId = 3;

            if (dbContext.SaveChanges() > 0) return roomBooking;
            return null;
        }

        public List<RoomBokingViewModel> GetAllBookings()
        {
            List<RoomBokingViewModel> listOfRoomBookings = new List<RoomBokingViewModel>();
            listOfRoomBookings = (from booking in dbContext.Bookings
                                  join room in dbContext.Rooms
                                  on booking.AssignRoomId equals room.RoomId
                                  select new RoomBokingViewModel()
                                  {
                                      BookingFrom = booking.BookingFrom,
                                      BookingTo = booking.BookingTo,
                                      CustomerPhone = booking.CustomerPhone,
                                      CustomerAddress = booking.CustomerAddress,
                                      CustomerName = booking.CustomerName,
                                      NumberOfMembers = booking.NoOfMembers,
                                      RoomNumber = room.RoomNumber,
                                      NumberOfDays = System.Data.Entity.DbFunctions.DiffDays(booking.BookingFrom, booking.BookingTo).Value,
                                      BookingId = booking.BookingId,
                                      RoomPrice = room.RoomPrice,
                                      totalAmount = booking.totalAmount

                                  }).ToList();

            return listOfRoomBookings;
        }
    }
}