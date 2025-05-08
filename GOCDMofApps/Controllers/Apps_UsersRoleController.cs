using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GOCDMofApps.Models;
using static GOCDMofApps.FilterConfig;

namespace GOCDMofApps.Controllers
{
    [CustomAuthorize(Roles = "admin-issu")]
    public class Apps_UsersRoleController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_UsersRole
        public ActionResult Index()
        {
            return View(db.Apps_UsersRole.ToList());
        }

        // GET: Apps_UsersRole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_UsersRole apps_UsersRole = db.Apps_UsersRole.Find(id);
            if (apps_UsersRole == null)
            {
                return HttpNotFound();
            }
            return View(apps_UsersRole);
        }

        // GET: Apps_UsersRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps_UsersRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] Apps_UsersRole apps_UsersRole)
        {
            if (ModelState.IsValid)
            {
                db.Apps_UsersRole.Add(apps_UsersRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apps_UsersRole);
        }

        // GET: Apps_UsersRole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_UsersRole apps_UsersRole = db.Apps_UsersRole.Find(id);
            if (apps_UsersRole == null)
            {
                return HttpNotFound();
            }
            return View(apps_UsersRole);
        }

        // POST: Apps_UsersRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] Apps_UsersRole apps_UsersRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_UsersRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apps_UsersRole);
        }

        // GET: Apps_UsersRole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_UsersRole apps_UsersRole = db.Apps_UsersRole.Find(id);
            if (apps_UsersRole == null)
            {
                return HttpNotFound();
            }
            return View(apps_UsersRole);
        }

        // POST: Apps_UsersRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_UsersRole apps_UsersRole = db.Apps_UsersRole.Find(id);
            db.Apps_UsersRole.Remove(apps_UsersRole);
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
