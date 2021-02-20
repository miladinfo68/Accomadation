using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Enums
{
    public enum ConstantValues
    {
        There_Is_No_Empty_Capacity = 1,
        StarDate_EndDate_Empty = 2,
        StartDate_GreaterThan_EndDate = 3,
        Success = 4,
        Current_Room_Before_Reserved = 5,
        OverLapRangeDate_Current_And_Past = 6,
        InvalidSexuality = 7,
    }
}
