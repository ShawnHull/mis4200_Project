using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using mis4200_Project.DAL;
using mis4200_Project.Models;

namespace mis4200_Project.Controllers
{
    public class RecognitionsController : Controller
    {
        private CContext db = new CContext();

        // GET: Recognitions
        [Authorize]
        public ActionResult Index()
        {
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            
            var recognition = db.recognition.Where(r => r.profileID == memberId);
            var reclist = recognition.ToList();
            var prolist = db.profile.Where(r => r.profileID == memberId);
            ViewBag.pro = prolist.ToList();


            var dep = prolist.Select(r => r.Department).Single();
            ViewBag.dep = dep;

            var phone = prolist.Select(r => r.phone).Single();
            ViewBag.phone = phone;
            var email = prolist.Select(r => r.email).Single();
            ViewBag.email = email;
            var last = prolist.Select(r => r.employeeLastName).Single();
            ViewBag.last = last;
            var first = prolist.Select(r => r.employeeFirstName).Single();
            ViewBag.first = first;
            var ava = prolist.Select(r => r.avatar).Single();
            ViewBag.ava = ava;
            
            //var userlist =db.profile.Where(r => r.profileID == Recognition.)
            return View(db.recognition.ToList());
        }

        // GET: Recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: Recognitions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.value = new SelectList(Enum.GetValues(typeof(Recognition.CoreValue)),"CoreValue");
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFullName");
            ViewBag.UserID = new SelectList(db.profile, "profileID", "employeeFullName");
            return View();
        }

        // POST: Recognitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecognitionID,value,recognitionDescription,recognitionDate,profileID")] Recognition recognition)
        {
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            if (memberId == recognition.profileID)
            {
                return View("selfRecognition");
            }
            else
            {
           
                {
                    if (ModelState.IsValid)
                 {
                    db.recognition.Add(recognition);
                        recognition.userName = HttpContext.User.Identity.GetUserId();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                 }

                }    
                
            }  

            

            ViewBag.CoreValueTypeID = new SelectList(db.recognition, "Value", "CoreValue", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFullName", recognition.profileID);
            return View(recognition);
        }

        // GET: Recognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoreValueTypeID = new SelectList(db.recognition, "Value", "CoreValue", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFullName", recognition.profileID);
            return View(recognition);
        }

        // POST: Recognitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecognitionID,Value,recognitionDescription,recognitionDate,profileID")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoreValueTypeID = new SelectList(db.recognition, "Value", "CoreValue", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFullName", recognition.profileID);
            return View(recognition);
        }

        // GET: Recognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: Recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recognition recognition = db.recognition.Find(id);
            db.recognition.Remove(recognition);
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
    }
}
