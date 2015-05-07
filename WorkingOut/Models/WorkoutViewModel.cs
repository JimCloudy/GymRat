using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkingOut.Models
{
    public class WorkoutViewModel
    {
        public Workout Workout { get; set; }
        public List<AddedExerciseViewModel> AddedExercises { get; set; }
        public IEnumerable<Exercise> ExerciseList { get; set; }
        public Exercise Exercise { get; set; }
    }
}