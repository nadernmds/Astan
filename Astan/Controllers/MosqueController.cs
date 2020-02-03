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
    [RequsetLogin(1, 2)]

    public class MosqueController : Controller
    {
        private AstanEntities db = new AstanEntities();

        // GET: Mosque
        public ActionResult Index()
        {
            return View(db.Mosques.ToList());
        }

        // GET: Mosque/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mosque mosque = db.Mosques.Find(id);
            if (mosque == null)
            {
                return HttpNotFound();
            }
            return View(mosque);
        }

        // GET: Mosque/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mosque/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mosqueID,mosqueName,adress,bossName,phone,bossMobile")] Mosque mosque)
        {
            if (ModelState.IsValid)
            {
                db.Mosques.Add(mosque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mosque);
        }

        // GET: Mosque/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mosque mosque = db.Mosques.Find(id);
            if (mosque == null)
            {
                return HttpNotFound();
            }
            return View(mosque);
        }

        // POST: Mosque/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mosqueID,mosqueName,adress,bossName,phone,bossMobile")] Mosque mosque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mosque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mosque);
        }

        // GET: Mosque/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mosque mosque = db.Mosques.Find(id);
            if (mosque == null)
            {
                return HttpNotFound();
            }
            return View(mosque);
        }

        // POST: Mosque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Mosque mosque = db.Mosques.Find(id);
            db.Mosques.Remove(mosque);
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
