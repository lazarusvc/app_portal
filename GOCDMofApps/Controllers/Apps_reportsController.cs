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
    public class Apps_reportsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_reports
        public ActionResult Index()
        {
            var apps_reports = db.Apps_reports.Include(a => a.Apps_UsersRole);
            return View(apps_reports.ToList());
        }

        // GET: Apps_reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports apps_reports = db.Apps_reports.Find(id);
            if (apps_reports == null)
            {
                return HttpNotFound();
            }
            return View(apps_reports);
        }

        // GET: Apps_reports/Create
        public ActionResult Create()
        {
            ViewBag.FK_REF_userRolesId = new SelectList(db.Apps_UsersRole, "Id", "name");
            return View();
        }

        // POST: Apps_reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,FK_REF_userRolesId,paramCheck")] Apps_reports apps_reports)
        {
            if (ModelState.IsValid)
            {
                db.Apps_reports.Add(apps_reports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_REF_userRolesId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_reports.FK_REF_userRolesId);
            return View(apps_reports);
        }

        // GET: Apps_reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports apps_reports = db.Apps_reports.Find(id);
            if (apps_reports == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_reports.FK_REF_userRolesId);
            return View(apps_reports);
        }

        // POST: Apps_reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,FK_REF_userRolesId,paramCheck")] Apps_reports apps_reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_reports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_reports.FK_REF_userRolesId);
            return View(apps_reports);
        }

        // GET: Apps_reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports apps_reports = db.Apps_reports.Find(id);
            if (apps_reports == null)
            {
                return HttpNotFound();
            }
            return View(apps_reports);
        }

        // POST: Apps_reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_reports apps_reports = db.Apps_reports.Find(id);
            db.Apps_reports.Remove(apps_reports);
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
