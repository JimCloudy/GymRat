using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingOut.Models;

namespace WorkingOut.Models
{
    public class Workout
    {
        public Workout()
        {
            this.Routine = new List<WorkoutExercise>();
            WorkoutExercise workoutExercise = new WorkoutExercise();
            this.Routine.Add(workoutExercise);
        }

        public int ID { get; set; }
        public DateTime WorkoutDate { get; set; }

        public virtual List<WorkoutExercise> Routine { get; set; }

        internal void CreateWorkoutExercises(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Routine.Add(new WorkoutExercise());
            }
        }
    }
}