using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apps_.Models;
using static Apps_.FilterConfig;

namespace Apps_.Controllers
{
    [CustomAuthorize(Roles = "admin-issu")]
    public class Apps_reports_paramsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_reports_params
        public ActionResult Index()
        {
            var apps_reports_params = db.Apps_reports_params.Include(a => a.Apps_reports);
            return View(apps_reports_params.ToList());
        }

        // GET: Apps_reports_params/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports_params apps_reports_params = db.Apps_reports_params.Find(id);
            if (apps_reports_params == null)
            {
                return HttpNotFound();
            }
            return View(apps_reports_params);
        }

        // GET: Apps_reports_params/Create
        public ActionResult Create()
        {
            ViewBag.FK_REF_reportsId = new SelectList(db.Apps_reports, "Id", "name");
            return View();
        }

        // POST: Apps_reports_params/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,param_key,param_value,param_dataType,FK_REF_reportsId")] Apps_reports_params apps_reports_params)
        {
            if (ModelState.IsValid)
            {
                db.Apps_reports_params.Add(apps_reports_params);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_REF_reportsId = new SelectList(db.Apps_reports, "Id", "name", apps_reports_params.FK_REF_reportsId);
            return View(apps_reports_params);
        }

        // GET: Apps_reports_params/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports_params apps_reports_params = db.Apps_reports_params.Find(id);
            if (apps_reports_params == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_REF_reportsId = new SelectList(db.Apps_reports, "Id", "name", apps_reports_params.FK_REF_reportsId);
            return View(apps_reports_params);
        }

        // POST: Apps_reports_params/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,param_key,param_value,param_dataType,FK_REF_reportsId")] Apps_reports_params apps_reports_params)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_reports_params).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_REF_reportsId = new SelectList(db.Apps_reports, "Id", "name", apps_reports_params.FK_REF_reportsId);
            return View(apps_reports_params);
        }

        // GET: Apps_reports_params/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_reports_params apps_reports_params = db.Apps_reports_params.Find(id);
            if (apps_reports_params == null)
            {
                return HttpNotFound();
            }
            return View(apps_reports_params);
        }

        // POST: Apps_reports_params/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_reports_params apps_reports_params = db.Apps_reports_params.Find(id);
            db.Apps_reports_params.Remove(apps_reports_params);
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
