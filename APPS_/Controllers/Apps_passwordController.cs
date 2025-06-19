using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apps_.Models;
using AuthenticatedEncryption;
using System.Text;
using static Apps_.FilterConfig;

namespace Apps_.Controllers
{
    [CustomAuthorize(Roles = "admin-issu")]
    public class Apps_passwordController : Controller
    {
        private ModelContainer db = new ModelContainer();

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        // GET: Apps_password
        public ActionResult Index()
        {
            var apps_password = db.Apps_password.Include(a => a.Apps_UsersRole);
            return View(apps_password.ToList());
        }
        List<Apps_password> apps_password;
        public ActionResult IndexPartial(string t)
        {            
            if (!String.IsNullOrEmpty(t))
            {
                if (t.Equals("ALL"))
                {
                    apps_password = db.Apps_password.Include(a => a.Apps_UsersRole).ToList();
                } else
                {
                    apps_password = db.Apps_password.Include(a => a.Apps_UsersRole).Where(x => x.tags.Equals(t)).ToList();
                }
            }       


            return PartialView("_partial__Apps_password", apps_password);
        }

        // GET: Apps_password/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_password apps_password = db.Apps_password.Find(id);
            if (apps_password == null)
            {
                return HttpNotFound();
            }
            return View(apps_password);
        }

        public static string Copy(int? id)
        {
            // Decrypt passwords
            // ======================
            ModelContainer db = new ModelContainer();
            Apps_password apps_password = db.Apps_password.Find(id);
            string pwd = Encryption.Decrypt(apps_password.password, GetBytes(apps_password.crypt), GetBytes(apps_password.auth));


            return pwd;
        }

        // GET: Apps_password/Create
        public ActionResult Create()
        {
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name");
            return View();
        }

        // POST: Apps_password/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,password,title,notes,tags,Apps_UsersRoleId,crypt,auth")] Apps_password apps_password)
        {
            if (ModelState.IsValid)
            {
                // Encrypt passwords
                // ==============

                byte[] cryptKey = Encryption.NewKey();
                byte[] authKey = Encryption.NewKey();
                string pwd = Encryption.Encrypt(apps_password.password, cryptKey, authKey);

                db.Apps_password.Add(new Apps_password { 
                    username = apps_password.username,
                    password = pwd,
                    title = apps_password.title,
                    notes = apps_password.notes,
                    tags =  apps_password.tags,
                    Apps_UsersRoleId = apps_password.Apps_UsersRoleId,
                    crypt = GetString(cryptKey),
                    auth = GetString(authKey),
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_password.Apps_UsersRoleId);
            return View(apps_password);
        }

        // GET: Apps_password/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_password apps_password = db.Apps_password.Find(id);
            if (apps_password == null)
            {
                return HttpNotFound();
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_password.Apps_UsersRoleId);
            return View(apps_password);
        }

        // POST: Apps_password/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,password,title,notes,tags,Apps_UsersRoleId,crypt,auth")] Apps_password apps_password)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_password).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Apps_UsersRoleId = new SelectList(db.Apps_UsersRole, "Id", "name", apps_password.Apps_UsersRoleId);
            return View(apps_password);
        }

        // GET: Apps_password/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_password apps_password = db.Apps_password.Find(id);
            if (apps_password == null)
            {
                return HttpNotFound();
            }
            return View(apps_password);
        }

        // POST: Apps_password/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apps_password apps_password = db.Apps_password.Find(id);
            db.Apps_password.Remove(apps_password);
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
