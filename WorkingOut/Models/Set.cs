using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class Set
    {
        public int ID { get; set; }
        public int WorkoutExerciseID { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }

        public virtual WorkoutExercise WorkoutExercise { get; set; }
    }
}