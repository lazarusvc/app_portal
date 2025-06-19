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
    public class Apps_TypeController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_Type
        public ActionResult Index()
        {
            return View(db.Apps_Type.ToList());
        }

        // GET: Apps_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Type apps_Type = db.Apps_Type.Find(id);
            if (apps_Type == null)
            {
                return HttpNotFound();
            }
            return View(apps_Type);
        }

        // GET: Apps_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] Apps_Type apps_Type)
        {
            if (ModelState.IsValid)
            {
                db.Apps_Type.Add(apps_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apps_Type);
        }

        // GET: Apps_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Type apps_Type = db.Apps_Type.Find(id);
            if (apps_Type == null)
            {
                return HttpNotFound();
            }
            return View(apps_Type);
        }

        // POST: Apps_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] Apps_Type apps_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apps_Type);
        }

        // GET: Apps_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Type apps_Type = db.Apps_Type.Find(id);
            if (apps_Type == null)
            {
                return HttpNotFound();
            }
            return View(apps_Type);
        }

        // POST: Apps_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_Type apps_Type = db.Apps_Type.Find(id);
            db.Apps_Type.Remove(apps_Type);
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
