using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{ 
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        //
        // GET: /Student/

        public ViewResult Index()
        {
            return View(db.Students.ToList());
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            catch (DataException)
            {
                // Log the error (add a variable name afer DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
           

            return View(student);
        }
        
        //
        // GET: /Student/Edit/5
 
        public ActionResult Edit(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                // Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes.  Try again, and if the problem persists see your system administrator.");
            }
            
            return View(student);
        }

        //
        // GET: /Student/Delete/5
 
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.  Try again, and if the problem persists contact your system administrator.";
            }
            return View(db.Students.Find(id));
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            catch (DataException)
            {
                // Log the error (add a variable name after DataException)
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary {
                        {"id", id},
                        {"saveChangesError", true} });
            }
                return RedirectToAction("Index");
        }
            
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}