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
                new Exercise{Name="Pushup",BodyPart="Chest", imageUrl=""},
                new Exercise{Name="Pullup",BodyPart="Back", imageUrl=""},
                new Exercise{Name="Bench Press",BodyPart="Chest", imageUrl=""},
                new Exercise{Name="Curl",BodyPart="Bicep", imageUrl=""},
                new Exercise{Name="Squat",BodyPart="Quad", imageUrl=""},
                new Exercise{Name="Situp",BodyPart="Abs", imageUrl=""},
                new Exercise{Name="Skull Crusher",BodyPart="Tricep", imageUrl=""}
            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();
        }
    }
}