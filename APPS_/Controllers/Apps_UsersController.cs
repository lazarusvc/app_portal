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
    [CustomAuthorize(Roles = "admin")]
    public class Apps_UsersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Apps_Users
        public ActionResult Index()
        {
            var apps_Users = db.Apps_Users.Include(a => a.Apps_UsersRole);
            return View(apps_Users.ToList());
        }

        // GET: Apps_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Users apps_Users = db.Apps_Users.Find(id);
            if (apps_Users == null)
            {
                return HttpNotFound();
            }
            return View(apps_Users);
        }

        // GET: Apps_Users/Create
        public ActionResult Create()
        {
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,firstName,lastName,username,password,Apps_UsersRoleId")] Apps_Users apps_Users)
        {
            if (ModelState.IsValid)
            {
                // Hash passwords
                // ==============
                var hash_pwd = BCrypt.Net.BCrypt.HashPassword(apps_Users.password);

                db.Apps_Users.Add(new Apps_Users
                {
                    firstName = apps_Users.firstName,
                    lastName = apps_Users.lastName,
                    username = apps_Users.username,
                    password = hash_pwd,
                    Apps_UsersRoleId = apps_Users.Apps_UsersRoleId
                });
                db.SaveChanges();

                return View();
            }

            ViewBag.FK_userRolesId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Users.Apps_UsersRoleId);
            return View(apps_Users);
        }

        // GET: Apps_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Users apps_Users = db.Apps_Users.Find(id);
            if (apps_Users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Users.Apps_UsersRoleId);
            return View(apps_Users);
        }

        // POST: Apps_Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstName,lastName,username,Apps_UsersRoleId")] Apps_Users apps_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_Users.Apps_UsersRoleId);
            return View(apps_Users);
        }

        // GET: Apps_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_Users apps_Users = db.Apps_Users.Find(id);
            if (apps_Users == null)
            {
                return HttpNotFound();
            }
            return View(apps_Users);
        }

        // POST: Apps_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_Users apps_Users = db.Apps_Users.Find(id);
            db.Apps_Users.Remove(apps_Users);
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
