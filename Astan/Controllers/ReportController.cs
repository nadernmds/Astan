using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Astan.Models;
using pep;
namespace Astan.Controllers
{
    [RequsetLogin(1, 2)]
    public class ReportController : Controller
    {
        AstanEntities db = new AstanEntities();
        // GET: Report
        public ActionResult Index()
        {

            ViewBag.userID = new SelectList(db.Users, "userID", "name");
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName");
            //  ViewBag.mosqueID = new SelectList(db.Companies, "companyID", "name");
            return View();
        }
        [HttpPost]
        public ActionResult generate(long? userID, long? mosqueID, string from, string to, string state)
        {
            DateTime fromdate = DateTime.MinValue;
            DateTime todate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(from))
                fromdate = from.toMiladiDate();
            if (!string.IsNullOrEmpty(to))
                todate = to.toMiladiDate().AddDays(1);
            var model = db.Clients.Where(c => c.registerDate >= fromdate && c.registerDate <= todate);
            if (userID.HasValue)
            {
                model = model.Where(c => c.userID == userID);
            }
            if (mosqueID.HasValue)
            {
                model = model.Where(c => c.mosqueID == mosqueID);
            }

            switch (state)
            {
                case "full":
                    var s = model.DecodeClients();
                    var m = new FullReport { ClientsCount = s.Count(), ClinetMemberCount = s.Select(c => c.ClientMembers).Count() };
                    return PartialView("FullReport", m);
                case "detailed":
                    return PartialView("DetailedReport", model);
                default:
                    return View();
            }
        }


    }
}