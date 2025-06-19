using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apps_.Models;

namespace Apps_.Controllers
{
    public class Apps_REF_processesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_REF_processes
        public ActionResult Index()
        {
            var apps_REF_processes = db.Apps_REF_processes.Include(a => a.Apps_processes);
            return View(apps_REF_processes.ToList());
        }
        public ActionResult IndexPartial(string n)
        {
            var vCAS_REF_processes = db.Apps_REF_processes.Include(v => v.Apps_processes).Where(x => x.Apps_processes.sp_name == n);
            return PartialView("_processesParamsIndex", vCAS_REF_processes.ToList());
        }

        // GET: Apps_REF_processes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_REF_processes apps_REF_processes = db.Apps_REF_processes.Find(id);
            if (apps_REF_processes == null)
            {
                return HttpNotFound();
            }
            return View(apps_REF_processes);
        }

        // GET: Apps_REF_processes/Create
        public ActionResult Create()
        {
            ViewBag.FK_processesId = new SelectList(db.Apps_processes, "Id", "sp_name");
            return View();
        }
        public ActionResult CreateCombined(int? id)
        {
            ViewBag.FK_processesId = new SelectList(db.Apps_processes.Where(x => x.Id == id), "Id", "sp_name");
            return View();
        }

        // POST: Apps_REF_processes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,param_key,param_value,param_dataType,FK_processesId")] Apps_REF_processes apps_REF_processes)
        {
            if (ModelState.IsValid)
            {
                db.Apps_REF_processes.Add(apps_REF_processes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_processesId = new SelectList(db.Apps_processes, "Id", "sp_name", apps_REF_processes.FK_processesId);
            return View(apps_REF_processes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCombined([Bind(Include = "Id,param_key,param_value,param_dataType,FK_processesId")] Apps_REF_processes apps_REF_processes)
        {
            if (ModelState.IsValid)
            {
                db.Apps_REF_processes.Add(apps_REF_processes);
                db.SaveChanges();
                return RedirectToAction("CreateCombined");
            }

            ViewBag.FK_processesId = new SelectList(db.Apps_processes, "Id", "sp_name", apps_REF_processes.FK_processesId);
            return View(apps_REF_processes);
        }

        // GET: Apps_REF_processes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_REF_processes apps_REF_processes = db.Apps_REF_processes.Find(id);
            if (apps_REF_processes == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_processesId = new SelectList(db.Apps_processes, "Id", "sp_name", apps_REF_processes.FK_processesId);
            return View(apps_REF_processes);
        }

        // POST: Apps_REF_processes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,param_key,param_value,param_dataType,FK_processesId")] Apps_REF_processes apps_REF_processes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_REF_processes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_processesId = new SelectList(db.Apps_processes, "Id", "sp_name", apps_REF_processes.FK_processesId);
            return View(apps_REF_processes);
        }

        // GET: Apps_REF_processes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_REF_processes apps_REF_processes = db.Apps_REF_processes.Find(id);
            if (apps_REF_processes == null)
            {
                return HttpNotFound();
            }
            return View(apps_REF_processes);
        }

        // POST: Apps_REF_processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_REF_processes apps_REF_processes = db.Apps_REF_processes.Find(id);
            db.Apps_REF_processes.Remove(apps_REF_processes);
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
