using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.DTOs
{
    public class BookingReadDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int RoomId { get; set; }
        public byte Capacity { get; set; }     
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
