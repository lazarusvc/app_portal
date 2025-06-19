using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apps_.Models;
using static Apps_.FilterConfig;

namespace Apps_.Controllers
{
    [CustomAuthorize(Roles = "admin-issu")]
    public class Apps_processesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        [HttpPost]
        public ActionResult Action(FormCollection fm)
        {
            string spName = fm["n"].ToString();
            string spDATASOURCE = fm["datasource"].ToString();
            string spDB = fm["db"].ToString();
            string spU = fm["user"].ToString();
            string spP = fm["pass"].ToString();

            var model = db.Apps_REF_processes.Include(v => v.Apps_processes).Where(x => x.Apps_processes.sp_name == spName);
            List<string> pKEYS = model.Select(x => x.param_key).ToList();
            List<string> pVALS = model.Select(x => x.param_value).ToList();
            List<string> pDTYPE = model.Select(x => x.param_dataType).ToList();

            // EXEC Stored Procedures
            // =================================================
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ModelContainerCustom"].ConnectionString;
            cnnString = cnnString.Replace("_DATASOURCE_", spDATASOURCE);
            cnnString = cnnString.Replace("_DB_", spDB);
            cnnString = cnnString.Replace("_U_", spU);
            cnnString = cnnString.Replace("_P_", spP);
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName;

            for (int i = 0; i < pKEYS.Count; i++)
            {
                string key = pKEYS[i].ToString();
                string val = pVALS[i].ToString();
                string dtype = pDTYPE[i];

                switch (dtype)
                {
                    case "Int":
                        cmd.Parameters.Add(key, SqlDbType.Int).Value = int.Parse(val);
                        break;
                    case "Float":
                        cmd.Parameters.Add(key, SqlDbType.Float).Value = float.Parse(val);
                        break;
                    case "Text":
                        cmd.Parameters.Add(key, SqlDbType.Text).Value = val;
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported data type: {dtype}");                        
                }
                ViewData["Error"] = "Unsupported data type:" + dtype;
            }

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery(); // EXECUTE COMMAND
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Error Detected: " + ex;
            }

            object rw = null;       // EXECUTE W/ RESULTS
            object cl = null;
            DataTable t1 = new DataTable();
            try
            {
                using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                {
                    a.Fill(t1);
                    for (int i = 0; i < t1.Rows.Count; i++)
                    {
                        for (int j = 0; j < t1.Columns.Count; j++)
                        {
                            rw = t1.Rows[i].ItemArray[j];
                            cl = t1.Columns[j].ColumnName;
                        }
                    }
                }
            } catch(Exception ex)
            {
                ViewData["Error"] = "Error Detected: " + ex;
            }

            Session["spCol"] = cl;
            Session["spRow"] = rw;

            cnn.Close();
            return RedirectToAction("ActionComplete");
        }
        public ActionResult ActionParams(string n, string ds, string db, string u, string p)
        {
            ViewBag.n = n;
            ViewBag.ds = ds;
            ViewBag.db = db;
            ViewBag.u = u;
            ViewBag.p = p;
            return View();
        }

        [HttpPost]
        public ActionResult ActionParamsUpdate(int? id, FormCollection fm)
        {
            string pVAL = Convert.ToString(fm["param_value"]);
            string spName = Convert.ToString(fm["spName"]);

            db.Database.ExecuteSqlCommand(String.Format(@"
             UPDATE dbo.Apps_REF_processes
             SET param_value = '{0}'
             WHERE Id = {1} 
            ", pVAL, id));
            return RedirectToAction("ActionParams", new { n = spName });
        }
        public ActionResult ActionComplete()
        {
            ViewData["spCol"] = Session["spCol"];
            ViewData["spRow"] = Session["spRow"];
            return View();
        }

        // GET: Apps_processes
        public ActionResult Index()
        {
            return View(db.Apps_processes.OrderBy(x => x.sp_name).ToList());
        }

        // GET: Apps_processes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_processes apps_processes = db.Apps_processes.Find(id);
            if (apps_processes == null)
            {
                return HttpNotFound();
            }
            return View(apps_processes);
        }

        // GET: Apps_processes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apps_processes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,sp_name,sp_desc,sp_database,sp_databse__user,sp_databse__pass,sp_datasource")] Apps_processes apps_processes)
        {
            if (ModelState.IsValid)
            {
                db.Apps_processes.Add(apps_processes);
                db.SaveChanges();
                return RedirectToAction("CreateCombined", "Apps_REF_processes", new { id = apps_processes.Id });
            }

            return View(apps_processes);
        }

        // GET: Apps_processes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_processes apps_processes = db.Apps_processes.Find(id);
            if (apps_processes == null)
            {
                return HttpNotFound();
            }
            return View(apps_processes);
        }

        // POST: Apps_processes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,sp_name,sp_desc,sp_database,sp_databse__user,sp_databse__pass,sp_datasource")] Apps_processes apps_processes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps_processes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apps_processes);
        }

        // GET: Apps_processes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apps_processes apps_processes = db.Apps_processes.Find(id);
            if (apps_processes == null)
            {
                return HttpNotFound();
            }
            return View(apps_processes);
        }

        // POST: Apps_processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            db.Database.ExecuteSqlCommand(String.Format(@"
            DELETE dbo.Apps_REF_processes 
            WHERE FK_processesId = {0}

            DELETE dbo.Apps_processes
            WHERE Id = {0}
            ", id));

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
