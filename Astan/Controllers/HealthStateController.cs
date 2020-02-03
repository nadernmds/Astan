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
    [RequsetLogin(1,2)]

    public class HealthStateController : Controller
    {
        private AstanEntities db = new AstanEntities();

        // GET: HealthState
        public ActionResult Index()
        {
            return View(db.HealthStates.ToList());
        }

        // GET: HealthState/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthState healthState = db.HealthStates.Find(id);
            if (healthState == null)
            {
                return HttpNotFound();
            }
            return View(healthState);
        }

        // GET: HealthState/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HealthState/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "healthStateID,healthStateType")] HealthState healthState)
        {
            if (ModelState.IsValid)
            {
                db.HealthStates.Add(healthState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(healthState);
        }

        // GET: HealthState/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthState healthState = db.HealthStates.Find(id);
            if (healthState == null)
            {
                return HttpNotFound();
            }
            return View(healthState);
        }

        // POST: HealthState/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "healthStateID,healthStateType")] HealthState healthState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(healthState);
        }

        // GET: HealthState/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthState healthState = db.HealthStates.Find(id);
            if (healthState == null)
            {
                return HttpNotFound();
            }
            return View(healthState);
        }

        // POST: HealthState/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            HealthState healthState = db.HealthStates.Find(id);
            db.HealthStates.Remove(healthState);
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
