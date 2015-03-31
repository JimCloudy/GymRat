using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingOut.Models;

namespace WorkingOut.DAL
{
    public class WorkoutInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WorkoutContext>
    {
        protected override void Seed(WorkoutContext context)
        {
            var exercises = new List<Exercise>{
                new Exercise{Name="Pushup",BodyPart="Chest"},
                new Exercise{Name="Pullup",BodyPart="Back"},
                new Exercise{Name="Bench Press",BodyPart="Chest"},
                new Exercise{Name="Curl",BodyPart="Bicep"},
                new Exercise{Name="Squat",BodyPart="Quad"},
                new Exercise{Name="Situp",BodyPart="Abs"},
                new Exercise{Name="Skull Crusher",BodyPart="Tricep"}
            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();
        }
    }
}