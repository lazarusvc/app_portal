using Apps_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using static Apps_.FilterConfig;
//using Microsoft.Reporting.WebForms;

namespace Apps_.Controllers
{
    public class HomeController : Controller
    {

        // HOME PAGE
        // =========
        ModelContainer db = new ModelContainer();

        public static String global_Identity(int? id)
        {
            ModelContainer db = new ModelContainer();
            string results = "";
            try
            {
                switch (id)
                {
                    case 1: // ---------------------------------------------------------------- arg:1 - Name
                        results = db.Identities.Select(x => x.name).FirstOrDefault();
                        break;
                    case 2: // ---------------------------------------------------------------- arg2: - Logo
                        results = db.Identities.Select(x => x.logo).FirstOrDefault();
                        break;
                    case 3: // ---------------------------------------------------------------- arg3: - HeaderColor
                        results = db.Identities.Select(x => x.headerColor).FirstOrDefault();
                        break;
                    case 4: // ---------------------------------------------------------------- arg4: - FooterDesc
                        results = db.Identities.Select(x => x.footerDesc).FirstOrDefault();
                        break;
                    case 5: // ---------------------------------------------------------------- arg5: - AboutDesc
                        results = db.Identities.Select(x => x.aboutDesc).FirstOrDefault();
                        break;
                    case 6: // ---------------------------------------------------------------- arg6: - ContactDesc
                        results = db.Identities.Select(x => x.contactDesc).FirstOrDefault();
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error:" + e);
                results = "Error:" + e;
            }


            return results;
        }

        public ActionResult Index()
        {
            ViewBag.search = new SelectList((from s in db.vw_search.SqlQuery("select * from vw_search").ToList()
            select new
            {
                ID = s.url,
                name = (s.name ?? "")
            }), "Id", "name");
            return View(db.Apps.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        // REPORTS PAGE
        // ============
        [CustomAuthorize(Roles = "admin-issu, report-user")]
        [HttpGet]
        public ActionResult Reports()
        {
            ViewBag.rp = new SelectList(db.Apps_reports.ToList(), "name", "desc");

            // Default report page  
            ReportViewer reportViewer = new ReportViewer();
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        [HttpPost]
        public ActionResult Reports(FormCollection form)
        {
            string reportName = Convert.ToString(form["rp"]);
            string rn = db.Apps_reports.Where(x => x.name == reportName).Select(x => x.name).FirstOrDefault();
            bool paramCheck = db.Apps_reports.Where(x => x.name == reportName).Select(x => x.paramCheck).FirstOrDefault();
            ViewBag.rp = new SelectList(db.Apps_reports.ToList(), "name", "desc");

            // Report page configurations
            ReportViewer reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true,
                ZoomMode = ZoomMode.PageWidth,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(500),
                AsyncRendering = true
            };
            reportViewer.ServerReport.ReportServerUrl = new Uri("http://gocdssr/ReportServer/");
            reportViewer.ServerReport.ReportPath = @"/Public/" + rn.Replace(".rdl", "");

            // Fetch report parameters from DB
            foreach (Apps_reports_params arp in db.Apps_reports_params.Where(x => x.Apps_reports.name == reportName))
            {
                if (paramCheck)
                {
                    reportViewer.ServerReport.SetParameters(new ReportParameter(arp.param_key, arp.param_value));
                }
            }

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult PartialDocs()
        {
            List<Apps_Document> list = this.db.Apps_Document.ToList<Apps_Document>();
            return this.PartialView("_partial__Apps_Doc", list);
        }

    }
}