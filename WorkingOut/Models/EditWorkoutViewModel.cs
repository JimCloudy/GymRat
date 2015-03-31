using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class EditWorkoutViewModel
    {
        public Workout Workout { get; set; }
        public List<AddedExerciseViewModel> AddedExercises { get; set; }
    }
}