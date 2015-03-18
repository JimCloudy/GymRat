using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class WorkoutExercise
    {
        public int ID { get; set; }
        public int WorkoutID { get; set; }
        public int ExerciseID { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public bool DeleteExercise { get; set; }

        public virtual Workout Workout { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}