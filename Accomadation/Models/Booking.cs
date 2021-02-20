using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Models
{
    public class Booking
    {
        public int UserId { get; set; }
        public bool Gender { get; set; }
        public int RoomId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
