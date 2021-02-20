using Accomadation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Infrastructurs.Extentions
{
    public static class ExtentionMethods
    {
        public static List<SelectListItem> ToDrpUsers(this IEnumerable<User> users)
        {
            var drp =new List<SelectListItem>();
            users.ToList().ForEach(user => {
                drp.Add(new SelectListItem() {Value=user.ID.ToString() ,Text=user.FullName });
            });
            return drp;
        }

        public static List<SelectListItem> ToDrpRooms(this IEnumerable<Room> rooms)
        {
            var drp = new List<SelectListItem>();
            rooms.ToList().ForEach(room => {
                drp.Add(new SelectListItem() { Value = room.ID.ToString(), Text = $"{room.ID}-{room.Places}" });
            });
            return drp;
        }
    }
}
