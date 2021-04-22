using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using mis4200_Project.Models;


namespace mis4200_Project.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        [Authorize]
        public ActionResult Index(string search)

        {
            if (search == null)
            {
                return View(db.Profiles.ToList());
            }
            else
            {
                return View(db.Profiles.Where(x => x.employeeFirstName.Contains(search)).ToList());

            }


        }
        // GET: Profiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "profileID,employeeFirstName,employeeLastName,email,phone,employeeSince,Department,socialMediaLinks,avatar")] Profile profile)
        {
            if (ModelState.IsValid)
            {

                HttpPostedFileBase file = Request.Files["UploadedImage"];
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != ".gif")
                    {
                        ViewBag.Errormsg = "The file, " + file.FileName + ",does not have a vaild image extension.";
                        return View(Profile);
                    }
                    else
                    {
                        Guid memberId;
                        Guid.TryParse(User.Identity.GetUserId(), out memberId);
                        profile.profileID = memberId;


                        profile.avatar = Guid.NewGuid().ToString() + fi.Extension;
                        file.SaveAs(Server.MapPath("~/Pictures/" + profile.avatar));

                    }
                }

                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index", "Profiles");
            }
            else
            {
                return View(profile);
            }
                          
            
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            if (memberId == id)
            {
                Profile currentProfile = db.Profiles.Find(id);
                TempData["oldPhoto"] = currentProfile.avatar;
                return View(profile);

            }
            else
            {
                return View("notAuthorized");
            }
            
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "profileID,employeeFirstName,employeeLastName,email,phone,employeeSince,Department,socialMediaLinks,avatar")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadedImage"];
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != ".gif")
                    {
                        ViewBag.Errormsg = "The file, " + file.FileName + ",does not have a vaild image extension.";
                        return View(Profile);
                    }
                    else
                    {
                        string path = Server.MapPath("~/Pictures" + TempData["oldPhoto"].ToString());
                        try
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            else
                            {

                            }

                        }
                        catch (Exception EX)
                        {
                            ViewBag.deleteFailed = EX.Message;
                            return View("DeleteFailed");

                        }
                        if (fi.Name != null && fi.Name != "")
                        {
                            profile.avatar = Guid.NewGuid().ToString() + fi.Extension;
                            file.SaveAs(Server.MapPath("~/Pictures/" + profile.avatar));
                        }

                    }
                }
                else
                {
                    profile.avatar = TempData["oldPhoto"].ToString();
                }

                
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            if (memberId == id)
            {
                return View(profile);

            }
            else
            {
                return View("notAuthorized");
            }
            
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Profile profile = db.Profiles.Find(id);
            string imageName = profile.avatar;
            string path = Server.MapPath("~/Pictures/" + imageName);
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                else
                {

                }

            }
            catch (Exception EX)
            {
                ViewBag.deleteFailed = EX.Message;
                return View("DeleteFailed");
                
            }
            db.Profiles.Remove(profile);
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
