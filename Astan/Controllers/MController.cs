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
    public class MController : Controller
    {
        private AstanEntities db = new AstanEntities();

        // GET: M
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.HealthState).Include(c => c.Mosque).Include(c => c.Piority).Include(c => c.User).Include(c => c.HomeState);
            return View(clients.ToList());
        }

        // GET: M/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: M/Create
        public ActionResult Create()
        {
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType");
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName");
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType");
            ViewBag.userID = new SelectList(db.Users, "userID", "username");
            ViewBag.homeStateID = new SelectList(db.HomeStates, "homeStateID", "homeStateName");
            return View();
        }

        // POST: M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientID,name,fatherName,nationalCode,jobtitle,birthDay,homeAdress,healthStateID,mobile,mosqueID,pirorityID,need,maried,userID,registerDate,educationID,sex,homeStateID,homeStateDescription,phone,Continus,goneShrine,moneySource")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            ViewBag.homeStateID = new SelectList(db.HomeStates, "homeStateID", "homeStateName", client.homeStateID);
            return View(client);
        }

        // GET: M/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            ViewBag.homeStateID = new SelectList(db.HomeStates, "homeStateID", "homeStateName", client.homeStateID);
            return View(client);
        }

        // POST: M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,name,fatherName,nationalCode,jobtitle,birthDay,homeAdress,healthStateID,mobile,mosqueID,pirorityID,need,maried,userID,registerDate,educationID,sex,homeStateID,homeStateDescription,phone,Continus,goneShrine,moneySource")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            ViewBag.homeStateID = new SelectList(db.HomeStates, "homeStateID", "homeStateName", client.homeStateID);
            return View(client);
        }

        // GET: M/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
