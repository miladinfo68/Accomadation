using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.DTOs
{
    public class RoomReadDto
    {
        public string RoomId { get; set; }
        public string Capacity { get; set; }
        public string Filled { get; set; }
        public string Remain { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
