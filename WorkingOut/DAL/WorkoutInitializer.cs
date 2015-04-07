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
                new Exercise{Name="Sit-Up",BodyPart="Abs",WeightInfo="Body Weight"},
                new Exercise{Name="Leg Raise",BodyPart="Abs",WeightInfo="Body Weight"},
                new Exercise{Name="Back Extension",BodyPart="Back",WeightInfo="Body Weight"},
                new Exercise{Name="Bent Over Row",BodyPart="Back",WeightInfo="Barbell Weight"},
                new Exercise{Name="Chin Up",BodyPart="Back",WeightInfo="Body Weight"},
                new Exercise{Name="Pulldown",BodyPart="Back",WeightInfo="Cable Weight"},
                new Exercise{Name="Seated Row",BodyPart="Back",WeightInfo="Cable Weight"},
                new Exercise{Name="Barbell Curl",BodyPart="Biceps",WeightInfo="Barbell Weight"},
                new Exercise{Name="Barbell Preacher Curl",BodyPart="Biceps",WeightInfo="Barbell Weight"},
                new Exercise{Name="Concentration Curl",BodyPart="Biceps",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Dumbbell Curl",BodyPart="Biceps",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Dumbbell Preacher Curl",BodyPart="Biceps",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Seated Calf Raise",BodyPart="Calves",WeightInfo="Cable Weight"},
                new Exercise{Name="Standing Calf Raise",BodyPart="Calves",WeightInfo="Cable Weight"},
                new Exercise{Name="Bench Press",BodyPart="Chest",WeightInfo="Barbell Weight"},
                new Exercise{Name="Inclined Bench Press",BodyPart="Chest",WeightInfo="Barbell Weight"},
                new Exercise{Name="Dumbbell Fly",BodyPart="Chest",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Inclined Dumbbell Press",BodyPart="Chest",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Dumbbell Press",BodyPart="Chest",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Declined Bench Press",BodyPart="Chest",WeightInfo="Barbell Weight"},
                new Exercise{Name="Declined Barbell Press",BodyPart="Chest",WeightInfo="Barbell Weight"},
                new Exercise{Name="Reverse Wrist Curl",BodyPart="Forearms",WeightInfo="Barbell Weight"},
                new Exercise{Name="Wrist Curl",BodyPart="Forearms",WeightInfo="Barbell Weight"},
                new Exercise{Name="Leg Curl",BodyPart="Legs",WeightInfo="Cable Weight"},
                new Exercise{Name="Leg Extension",BodyPart="Legs",WeightInfo="Cable Weight"},
                new Exercise{Name="Leg Press",BodyPart="Legs",WeightInfo="Cable Weight"},
                new Exercise{Name="Dumbbell Lunge",BodyPart="Legs",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Squat",BodyPart="Legs",WeightInfo="Barbell Weight"},
                new Exercise{Name="Dumbbell Lateral Raise",BodyPart="Shoulders",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Dumbbell Shoulder Press",BodyPart="Shoulders",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Military Press",BodyPart="Shoulders",WeightInfo="Barbell Weight"},
                new Exercise{Name="Upright Row",BodyPart="Shoulders",WeightInfo="Barbell Weight"},
                new Exercise{Name="Dip",BodyPart="Triceps",WeightInfo="Body Weight"},
                new Exercise{Name="Body-Up",BodyPart="Triceps",WeightInfo="Body Weight"},
                new Exercise{Name="Dumbbell Kickback",BodyPart="Triceps",WeightInfo="Dumbbell Weight"},
                new Exercise{Name="Pushdown",BodyPart="Triceps",WeightInfo="Cable Weight"},
                new Exercise{Name="Blah",BodyPart="Triceps",WeightInfo="Cable Weight",User="you"},
                new Exercise{Name="Blah blah",BodyPart="Triceps",WeightInfo="Cable Weight",User="you"},
                new Exercise{Name="Blah blah blah",BodyPart="Triceps",WeightInfo="Cable Weight",User="you"}
            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();
        }
    }
}