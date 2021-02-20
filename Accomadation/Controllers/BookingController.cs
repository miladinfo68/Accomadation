using Accomadation.DTOs;
using Accomadation.Infrastructurs.Extentions;
using Accomadation.Infrastructurs.Interfaces.IServices;
using Accomadation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Accomadation.Infrastructurs.Messages;
using System;

namespace Accomadation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        public BookingController(IUserService userService
            , IRoomService roomService
            , IBookingService bookingService
            )
        {
            _userService = userService;
            _roomService = roomService;
            _bookingService = bookingService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _bookingService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var users = await _userService.GetAsync();
            TempData["Users"] = users.ToDrpUsers();
            var rooms = await _roomService.GetAsync();
            TempData["Rooms"] = rooms.ToDrpRooms();
            var model = await _bookingService.GetAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> New(BookingWriteDto data)
        {
            var message = Messages.InvalidModelData;
            if (data != null && !string.IsNullOrEmpty(data.User)
                && !string.IsNullOrEmpty(data.Room)
                && !string.IsNullOrEmpty(data.StartDate)
                && !string.IsNullOrEmpty(data.EndDate)
                && (data.Gender == "0" || data.Gender == "1")
                && (DateTime.Parse(data.StartDate) <= DateTime.Parse(data.EndDate))
                )
            {
                var b = new Booking();
                b.UserId = int.Parse(data.User);
                b.RoomId = int.Parse(data.Room);
                b.Gender = data.Gender == "0" ? false : true;// bool.Parse(data.Gender);
                b.StartDate = data.StartDate;
                b.EndDate = data.EndDate;
                message = await _bookingService.AddAsync(b);
            }
            var json = new JsonResult(message);
            return json;
        }

        [HttpGet]
        public async Task<IActionResult> FreeRooms()
        {
            var model = await _roomService.GetByDateAsync();
            return View(model);
        }

    }
}
