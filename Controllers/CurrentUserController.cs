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
    public class CurrentUserController : Controller
    {
        private CContext db = new CContext();

        // GET: CurrentUser

        [Authorize]
        public ActionResult Index()
        {
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            

            var recognition = db.recognition.Where(r => r.profileID == memberId);
            var reclist = recognition.ToList();
            
            
            ViewBag.rec = reclist;
            var totalCnt = reclist.Count();
            ViewBag.total = totalCnt;
            var rec1Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Excellence ).Count();
            ViewBag.one = rec1Cnt;
            var rec2Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Openness).Count();
            ViewBag.two = rec2Cnt;
            var rec3Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Stewardship).Count();
            ViewBag.three = rec3Cnt;
            var rec4Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Culture).Count();
            ViewBag.four = rec4Cnt;
            var rec5Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Passion).Count();
            ViewBag.five = rec5Cnt;
            var rec6Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Innovate).Count();
            ViewBag.six = rec6Cnt;
            var rec7Cnt = reclist.Where(r => r.value == Recognition.CoreValue.Balanced).Count();
            ViewBag.seven = rec7Cnt;

            var prolist = db.profile.Where(r => r.profileID == memberId);
            ViewBag.pro = prolist.ToList();
             
           
            var dep = prolist.Select(r => r.Department).Single();
            ViewBag.dep = dep;
            var pos = prolist.Select(r => r.postion).Single();
            ViewBag.pos = pos;

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






            return View(recognition.ToList());
        }

        // GET: CurrentUser/Details/5
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

        // GET: CurrentUser/Create
        public ActionResult Create()
        {
            //ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName");
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName");
            return View();
        }

        // POST: CurrentUser/Create
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

            //ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // GET: CurrentUser/Edit/5
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
            //ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // POST: CurrentUser/Edit/5
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
            //ViewBag.CoreValueTypeID = new SelectList(db.CoreValueType, "CoreValueTypeID", "CoreValueName", recognition.value);
            ViewBag.profileID = new SelectList(db.profile, "profileID", "employeeFirstName", recognition.profileID);
            return View(recognition);
        }

        // GET: CurrentUser/Delete/5
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

        // POST: CurrentUser/Delete/5
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
