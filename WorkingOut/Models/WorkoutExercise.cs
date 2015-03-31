using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingOut.Models
{
    public class WorkoutExercise
    {
        public int ID { get; set; }
        public int WorkoutID { get; set; }

        [Display(Name="Exercise")]
        public int ExerciseID { get; set; }

        public virtual Workout Workout { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual ICollection<Set> Sets { get; set;}
    }
}