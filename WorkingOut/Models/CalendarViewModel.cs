using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class CalendarViewModel
    {
        public int workoutID { get; set; }
        public string workoutDate { get; set; }
        public decimal workoutWeight { get; set; }
        public int workoutExercises { get; set; }
        public string workoutNotes { get; set; }
    }
}