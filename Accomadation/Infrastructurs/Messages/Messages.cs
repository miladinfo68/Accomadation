using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Infrastructurs.Messages
{
    public static class Messages
    {        
        public static string There_Is_No_Empty_Capacity => "There is no empty capacity in this room !";
        public static string StarDate_EndDate_Empty => "Start date and end date can't be empty !";
        public static string StartDate_GreaterThan_EndDate => "StartDate can't be greater than endDate !";
        public static string Current_Room_Before_Reserved => "Current room before reserved !";
        public static string SuccessMessage => "Record successfuly inserted !";
        public static string OverLapRangeDate_Current_And_Past => "There is an overlap between current range date and existed range date in before!";
        public static string InvalidSexuality => "Selected gender is different from existed in before !";
        public static string InvalidModelData => "Invalid model data !";
    }
}
