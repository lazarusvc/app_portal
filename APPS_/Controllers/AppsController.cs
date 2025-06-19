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
    [CustomAuthorize(Roles = "admin-issu")]
    public class AppsController : Controller
    {
        private string fileName;
        private ModelContainer db = new ModelContainer();

        // GET: Apps
        public ActionResult Index()
        {
            var apps = db.Apps.Include(a => a.Apps_Category).Include(a => a.Apps_Type).Include(a => a.Apps_UsersRole);
            return View(apps.ToList());
        }

        // GET: Apps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            return View(apps);
        }

        // GET: Apps/Create
        public ActionResult Create()
        {
            ViewBag.Apps_CategoryId = new SelectList(db.Apps_Category, "Id", "name");
            ViewBag.Apps_TypeId = new SelectList(db.Apps_Type, "Id", "name");
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name");
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,Apps_CategoryId,url,Apps_TypeId,loc,featured,active,image,Apps_UsersRoleId")] Apps apps, HttpPostedFileBase image)
        {
            // Attach File
            if (image != null && image.ContentLength > 0)
            {
                try
                {
                    fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                    apps.image = "/Content/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);
                    image.SaveAs(fileName);
                } catch (Exception ex)
                {
                    ViewBag.Alert = "ERROR:" + ex.Message.ToString();
                }


            } else
            {
                ViewBag.Alert = "You have not specified a file.";
            }

            if (ModelState.IsValid)
            {
                db.Apps.Add(new Apps {
                    Id = apps.Id,
                    name = apps.name,
                    desc = apps.desc,
                    Apps_CategoryId = apps.Apps_CategoryId,
                    url = apps.url,
                    Apps_TypeId = apps.Apps_TypeId,
                    loc = apps.loc,
                    featured = apps.featured,
                    active = apps.active,
                    image = apps.image,
                    Apps_UsersRoleId = apps.Apps_UsersRoleId
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Apps_CategoryId = new SelectList(db.Apps_Category, "Id", "name", apps.Apps_CategoryId);
            ViewBag.Apps_TypeId = new SelectList(db.Apps_Type, "Id", "name", apps.Apps_TypeId);
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps.Apps_UsersRoleId);
            return View(apps);
        }

        // GET: Apps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            ViewBag.Apps_CategoryId = new SelectList(db.Apps_Category, "Id", "name", apps.Apps_CategoryId);
            ViewBag.Apps_TypeId = new SelectList(db.Apps_Type, "Id", "name", apps.Apps_TypeId);
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps.Apps_UsersRoleId);
            return View(apps);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,Apps_CategoryId,url,Apps_TypeId,loc,featured,active,image,Apps_UsersRoleId")] Apps apps, HttpPostedFileBase image)
        {
            // Attach File
            if (image != null && image.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                apps.image = "/Content/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);
                image.SaveAs(fileName);
            }

            if (ModelState.IsValid)
            {
                db.Entry(apps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Apps_CategoryId = new SelectList(db.Apps_Category, "Id", "name", apps.Apps_CategoryId);
            ViewBag.Apps_TypeId = new SelectList(db.Apps_Type, "Id", "name", apps.Apps_TypeId);
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps.Apps_UsersRoleId);
            return View(apps);
        }

        // GET: Apps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            return View(apps);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps apps = db.Apps.Find(id);
            db.Apps.Remove(apps);
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
