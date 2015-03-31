using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingOut.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingOut.Models
{
    public class Workout
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Workout Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkoutDate { get; set; }

        [Display(Name = "Weight On Scale(lbs)")]
        public decimal ScaleWeight { get; set; }

        [Display(Name = "Duration(mins)")]
        public int Duration { get; set; }

        public string WorkoutType { get; set; }

        [MaxLength(256)]
        public string Notes { get; set; }

        public virtual ICollection<WorkoutExercise> Routine { get; set; }

    }
}