﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkingOut.DAL;
using WorkingOut.Models;

namespace WorkingOut.Controllers
{
    public class WorkoutController : Controller
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: Workout
        public ActionResult Index()
        {
            return View(db.Workouts.ToList());
        }

        // GET: Workout/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Workout workout = db.Workouts.Find(id);
            Workout workout = db.Workouts.Include("Routine.Sets").Single(w => w.ID == id);

            if (workout == null)
            {
                return HttpNotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        public ActionResult Create()
        {
            Workout workout = new Workout();
            workout.WorkoutDate = DateTime.Now;
            workout.WorkoutType = "Weight Lifting";
            
            ViewBag.Exercises = PopulateExerciseList();
            ViewBag.ExerciseTypes = PopulateTypeList();

            return View(workout);
        }

        // POST: Workout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workout workout)
        {
            if (ModelState.IsValid)
            {
                Workout w = new Workout();
                w.WorkoutDate = workout.WorkoutDate;
                w.ScaleWeight = workout.ScaleWeight;
                w.Duration = workout.Duration;
                w.WorkoutType = workout.WorkoutType;
                w.Notes = workout.Notes;
                db.Workouts.Add(w);

                foreach (WorkoutExercise exercise in workout.Routine.ToList())
                {
                    exercise.WorkoutID = w.ID;
                    db.WorkoutExercises.Add(exercise);

                    foreach (Set set in exercise.Sets.ToList())
                    {
                        set.WorkoutExerciseID = exercise.ID;
                        db.Sets.Add(set);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Exercises = PopulateExerciseList();

            return View(workout);
        }

        // GET: Workout/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
                        
            if (workout == null)
            {
                return HttpNotFound();
            }

            EditWorkoutViewModel model = new EditWorkoutViewModel();
            model.AddedExercises = new List<AddedExerciseViewModel>();

            model.Workout = workout;

            List<WorkoutExercise> exercises = db.WorkoutExercises.Where(x => x.WorkoutID == id).ToList();

            for (int i = 0; i < exercises.Count(); i++)
            {
                AddedExerciseViewModel addedExercise = new AddedExerciseViewModel();

                addedExercise.Delete = false;
                addedExercise.Exercise = exercises[i];
                addedExercise.ExerciseIndex = i;
                var temp = exercises[i].ExerciseID;
                addedExercise.ExerciseName = db.Exercises.Where(x => x.ID == temp).Single().Name;
                temp = exercises[i].ID;
                addedExercise.Sets = db.Sets.Where(x => x.WorkoutExerciseID == temp ).ToList();

                model.AddedExercises.Add(addedExercise);
            }

            model.ExerciseList = PopulateExerciseList();
            ViewBag.ExerciseTypes = PopulateTypeList();

            return View(model);
        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Workout workout, String[] DeleteExercise)
        {
            if (ModelState.IsValid)
            {
                int index = 0;
                foreach (var exercise in workout.Routine.ToList())
                {
                    if (DeleteExercise[index] == "true")
                    {
                        if (exercise.ID != 0)
                        {
                            WorkoutExercise w = db.WorkoutExercises.Where(x => x.ID == exercise.ID && x.WorkoutID == exercise.WorkoutID).Single();
                            db.WorkoutExercises.Remove(w);
                        }
                        workout.Routine.Remove(exercise);
                    }
                    else
                    {
                        if (exercise.ID == 0)
                        {
                            exercise.WorkoutID = workout.ID;
                            db.WorkoutExercises.Add(exercise);

                            foreach (Set set in exercise.Sets.ToList())
                            {
                                set.WorkoutExerciseID = exercise.ID;
                                db.Sets.Add(set);
                            }
                        }
                        else
                        {                            
                            foreach (Set set in exercise.Sets.ToList())
                            {
                                if (set.ID == 0)
                                {
                                    set.WorkoutExerciseID = exercise.ID;
                                    db.Sets.Add(set);
                                }
                                else
                                {
                                    db.Entry(set).State = EntityState.Modified;
                                }
                            }

                            db.Entry(exercise).State = EntityState.Modified;
                        }
                    }
                    index++;
                }

                db.Entry(workout).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(workout);
        }

        // GET: Workout/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }

            workout.Routine = db.WorkoutExercises.Where(x => x.WorkoutID == id).ToList();

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            List<WorkoutExercise> exercises = db.WorkoutExercises.Where(x => x.WorkoutID == workout.ID).ToList();
            foreach (var exercise in exercises)
            {
                db.WorkoutExercises.Remove(exercise);
            }
            db.Workouts.Remove(workout);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddToRoutine(int exerciseID, Dictionary<string, int> setsToAdd)
        {
            var w = new WorkoutExercise();

            return PartialView("~/Views/Workout/EditorTemplates/WorkoutExercise.cshtml", w);
        }

        [HttpPost]
        public ActionResult AddNewExercise(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                exercise.User = "me";

                db.Exercises.Add(exercise);

                db.SaveChanges();

                var json = new { exerciseList = PopulateExerciseList(), addedID = exercise.ID };

                return Json(json);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            List<string> errors = new List<string>();

            errors.Add("There was an error while adding exercise, please try again.");

            return Json(errors);
        }
               
        private IEnumerable<Exercise> PopulateExerciseList()
        {
            IEnumerable<Exercise> exercises = db.Exercises.Where(n => n.Name != null && n.User == null).Union(db.Exercises.Where(n => n.Name != null && n.User == "me")).OrderBy(u => u.Name).ToList();

            //List<SelectListItem> items = new List<SelectListItem>();
                        
            /*foreach (Exercise exercise in exercises)
            {
                items.Add(new SelectListItem
                {
                    Text = exercise.Name,
                    Value = exercise.ID.ToString(),
                    Selected = (exercise.ID == selectID),

                });
            }*/

            return exercises;
        }

        private IEnumerable<SelectListItem> PopulateTypeList()
        {
            return new SelectList(new[] { "Abs", "Back", "Biceps", "Calves", "Chest", "Forearms", "Legs", "Other", "Shoulders", "Triceps" }.Select(x => new { value = x, text = x }), "value", "text"); 
        }
    }
}
