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
    public class Apps_CategoryController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_Category
        public ActionResult Index()
        {
            return View(db.Apps_Category.ToList());
        }

        // GET: Apps_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Category apps_Category = db.Apps_Category.Find(id);
            if (apps_Category == null)
            {
                return HttpNotFound();
            }
            return View(apps_Category);
        }

        // GET: Apps_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] Apps_Category apps_Category)
        {
            if (ModelState.IsValid)
            {
                db.Apps_Category.Add(apps_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apps_Category);
        }

        // GET: Apps_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Category apps_Category = db.Apps_Category.Find(id);
            if (apps_Category == null)
            {
                return HttpNotFound();
            }
            return View(apps_Category);
        }

        // POST: Apps_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] Apps_Category apps_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apps_Category);
        }

        // GET: Apps_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Category apps_Category = db.Apps_Category.Find(id);
            if (apps_Category == null)
            {
                return HttpNotFound();
            }
            return View(apps_Category);
        }

        // POST: Apps_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_Category apps_Category = db.Apps_Category.Find(id);
            db.Apps_Category.Remove(apps_Category);
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
