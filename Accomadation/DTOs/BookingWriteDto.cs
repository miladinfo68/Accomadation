using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.DTOs
{
    public class BookingWriteDto
    {
        public string User { get; set; }
        public string Gender { get; set; }
        public string Room { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
