using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Astan.Models;

namespace Astan.Controllers
{
    [RequsetLogin]
    public class Faq55Controller : Controller
    {
        private AstanEntities db = new AstanEntities();
        User user = new User();
        public Faq55Controller()
        {
            if (System.Web.HttpContext.Current.Session["RPG"] != null)
            {
                user = System.Web.HttpContext.Current.Session["RPG"] as User;
            }
        }
        // GET: Faq
        public ActionResult Index()
        {
            var faqs = db.Faqs.Include(f => f.User);
            return View(faqs.ToList());
        }

        // GET: Faq/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // GET: Faq/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Users, "userID", "username");
            return View();
        }

        // POST: Faq/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "faqID,question,userID")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.userID = user.userID;
                db.Faqs.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.Users, "userID", "username", faq.userID);
            return View(faq);
        }

        // GET: Faq/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Users, "userID", "username", faq.userID);
            return View(faq);
        }

        // POST: Faq/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "faqID,question,userID")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.userID = user.userID;

                db.Entry(faq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Users, "userID", "username", faq.userID);
            return View(faq);
        }

        // GET: Faq/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Faq/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faq faq = db.Faqs.Find(id);
            db.Faqs.Remove(faq);
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
