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
    public class UserInfo : Controller
    {
        private CContext db = new CContext();

        // GET: UserInfo
        public ActionResult Index()
        {
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            
            var recognition = db.recognition.Include(r => r.CoreValueType).Include(r => r.Profile);
            return View(recognition.ToList());
        }

        // GET: UserInfo/Details/5
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

        // GET: UserInfo/Create
        public ActionResult Create()
        {
            ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName");
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName");
            return View();
        }

        // POST: UserInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecognitionID,CoreValueTypeID,recognitionDescription,recognitionDate,profileID")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.recognition.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.CoreValueTypeID);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // GET: UserInfo/Edit/5
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
            ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.CoreValueTypeID);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // POST: UserInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecognitionID,CoreValueTypeID,recognitionDescription,recognitionDate,profileID")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.CoreValueTypeID);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // GET: UserInfo/Delete/5
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

        // POST: UserInfo/Delete/5
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
