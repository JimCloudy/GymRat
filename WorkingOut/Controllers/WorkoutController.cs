using System;
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
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }

            workout.Routine = db.WorkoutExercises.Where(x => x.WorkoutID == id).ToList();


            return View(workout);
        }

        // GET: Workout/Create
        public ActionResult Create()
        {
            Workout workout = new Workout();
            workout.WorkoutDate = DateTime.Now;
            
            ViewBag.Exercises = PopulateExerciseList();

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
                foreach (var exercise in workout.Routine.ToList())
                {
                    if (exercise.DeleteExercise == true)
                    {
                        workout.Routine.Remove(exercise);
                    }
                }
                db.Workouts.Add(workout);
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

            workout.Routine = db.WorkoutExercises.Where(x => x.WorkoutID == id).ToList();

            ViewBag.Exercises = PopulateExerciseList();

            return View(workout);
        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Workout workout)
        {
            if (ModelState.IsValid)
            {
                foreach (var exercise in workout.Routine.ToList())
                {
                    if (exercise.DeleteExercise == true)
                    {
                        workout.Routine.Remove(exercise);
                        if (exercise.ID != 0)
                        {
                            WorkoutExercise w = db.WorkoutExercises.Where(x => x.ID == exercise.ID && x.WorkoutID == exercise.WorkoutID).Single();
                            db.WorkoutExercises.Remove(w);
                        }
                    }
                    else
                    {
                        if (exercise.ID == 0)
                        {
                            exercise.WorkoutID = workout.ID;
                            db.WorkoutExercises.Add(exercise);
                        }
                        else
                        {
                            db.Entry(exercise).State = EntityState.Modified;
                        }
                    }
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

        public ActionResult AddToRoutine()
        {
            WorkoutExercise w = new WorkoutExercise();

            ViewBag.Exercises = PopulateExerciseList();

            return PartialView("~/Views/Workout/EditorTemplates/WorkoutExercise.cshtml", w);
        }
               
        private IEnumerable<SelectListItem> PopulateExerciseList()
        {
            IEnumerable<Exercise> exercises = db.Exercises.ToList();

            IEnumerable<SelectListItem> items =
                from exercise in exercises
                select new SelectListItem
                {
                    Text = exercise.Name,
                    Value = exercise.ID.ToString()
                };

            return items;
        }
    }
}
