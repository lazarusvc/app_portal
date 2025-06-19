using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apps_.Models;
using static Apps_.FilterConfig;

namespace Apps_.Controllers
{
    public class Apps_DocumentController : Controller
    {
        private string fileName;
        private ModelContainer db = new ModelContainer();

        // GET: Apps_Document
        [CustomAuthorize(Roles = "admin-issu")]
        public ActionResult Index()
        {
            var apps_Document = db.Apps_Document.Include(a => a.Apps_UsersRole);
            return View(apps_Document.ToList());
        }

        // GET: Apps_Document/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Document apps_Document = db.Apps_Document.Find(id);
            if (apps_Document == null)
            {
                return HttpNotFound();
            }
            return View(apps_Document);
        }

        // GET: Apps_Document/Create
        [CustomAuthorize(Roles = "admin-issu")]
        public ActionResult Create()
        {
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name");
            return View();
        }

        // POST: Apps_Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,media,media_type,Apps_UsersRoleId,tags")] Apps_Document apps_Document, HttpPostedFileBase media)
        {
            // Attach File
            if (media != null && media.ContentLength > 0)
            {
                try
                {
                    fileName = Path.GetFileNameWithoutExtension(media.FileName);
                    string extension = Path.GetExtension(media.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                    apps_Document.media = "/Content/Uploads/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                    media.SaveAs(fileName);
                }
                catch (Exception ex)
                {
                    ViewBag.Alert = "ERROR:" + ex.Message.ToString();
                }


            }
            else
            {
                ViewBag.Alert = "You have not specified a file.";
            }

            if (ModelState.IsValid)
            {
                db.Apps_Document.Add(new Apps_Document {
                    Id = apps_Document.Id,
                    name = apps_Document.name,
                    desc = apps_Document.desc,
                    media = apps_Document.media,
                    media_type = apps_Document.media_type,
                    Apps_UsersRoleId = apps_Document.Apps_UsersRoleId,
                    tags = apps_Document.tags
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Document.Apps_UsersRoleId);
            return View(apps_Document);
        }

        // GET: Apps_Document/Edit/5
        [CustomAuthorize(Roles = "admin-issu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Document apps_Document = db.Apps_Document.Find(id);
            if (apps_Document == null)
            {
                return HttpNotFound();
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Document.Apps_UsersRoleId);
            return View(apps_Document);
        }

        // POST: Apps_Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,media,media_type,Apps_UsersRoleId,tags")] Apps_Document apps_Document, HttpPostedFileBase media)
        {
            // Attach File
            if (media != null && media.ContentLength > 0)
            {
                try
                {
                    fileName = Path.GetFileNameWithoutExtension(media.FileName);
                    string extension = Path.GetExtension(media.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                    apps_Document.media = "/Content/Uploads/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                    media.SaveAs(fileName);
                }
                catch (Exception ex)
                {
                    ViewBag.Alert = "ERROR:" + ex.Message.ToString();
                }


            }
            else
            {
                ViewBag.Alert = "You have not specified a file.";
            }

            if (ModelState.IsValid)
            {
                db.Entry(apps_Document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Document.Apps_UsersRoleId);
            return View(apps_Document);
        }

        // GET: Apps_Document/Delete/5
        [CustomAuthorize(Roles = "admin-issu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Document apps_Document = db.Apps_Document.Find(id);
            if (apps_Document == null)
            {
                return HttpNotFound();
            }
            return View(apps_Document);
        }

        // POST: Apps_Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_Document apps_Document = db.Apps_Document.Find(id);
            db.Apps_Document.Remove(apps_Document);
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
