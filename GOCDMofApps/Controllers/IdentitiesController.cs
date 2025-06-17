using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GOCDMofApps.Models;

namespace GOCDMofApps.Controllers
{
    public class IdentitiesController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName;

        // GET: Identities
        public ActionResult Index()
        {
            return View(db.Identities.ToList());
        }

        // GET: Identities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = db.Identities.Find(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // GET: Identities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Identities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,logo,headerColor,aboutDesc,contactDesc,footerDesc")] Identity identity, HttpPostedFileBase media)
        {
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (media != null && media.ContentLength > 0)
                {
                    // extract only the filename
                    fileName = Path.GetFileName(media.FileName);
                    // store the file inside ~/Content/Uploads folder
                    identity.logo = "/Content/Uploads/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    media.SaveAs(path);
                }

                db.Identities.Add(identity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(identity);
        }

        // GET: Identities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = db.Identities.Find(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // POST: Identities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,logo,headerColor,aboutDesc,contactDesc,footerDesc")] Identity identity, HttpPostedFileBase media)
        {
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (media != null && media.ContentLength > 0)
                {
                    // extract only the filename
                    fileName = Path.GetFileName(media.FileName);
                    // store the file inside ~/Content/Uploads folder
                    identity.logo = "/Content/Uploads/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    media.SaveAs(path);
                }

                db.Entry(identity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(identity);
        }

        // GET: Identities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity identity = db.Identities.Find(id);
            if (identity == null)
            {
                return HttpNotFound();
            }
            return View(identity);
        }

        // POST: Identities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Identity identity = db.Identities.Find(id);
            db.Identities.Remove(identity);
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
