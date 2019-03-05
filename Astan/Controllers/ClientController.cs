using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Astan.Models;
using pep;
namespace Astan.Controllers
{

    [RequsetLogin]
    public class ClientController : Controller
    {
        private AstanEntities db = new AstanEntities();
        User user = new User();
        public ClientController()
        {
            if (System.Web.HttpContext.Current.Session["RPG"] != null)
            {
                user = System.Web.HttpContext.Current.Session["RPG"] as User;
            }
        }
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<Client> clients;
            
            if (user.isAdmin())
            {
                clients = db.Clients.Include(c => c.HealthState).Include(c => c.Mosque).Include(c => c.Piority).Include(c => c.User);
            }
            else
            {
                clients = db.Clients.Include(c => c.HealthState).Include(c => c.Mosque).Include(c => c.Piority).Include(c => c.User).Where(c => c.userID == user.userID);
            }
            return View(clients.DecodeClients().ToList());
        }
        [HttpPost]
        public ActionResult index(string obj)
        {
            obj = (obj ?? "").EncodeItem();
            var s = db.Clients.Where(c => c.mobile == obj || c.nationalCode == obj);
            //if (s != null)
            //{
            //    return RedirectToAction("details", new { id = s.clientID });
            //}
            return View(viewName: "res", model: s.DecodeClients());
        }

        // GET: Client/Details/5
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
            if (client.userID!=user.userID|| user.isAdmin())
            {
                return HttpNotFound();

            }
            return View(client.DecodeClient());
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType");
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName");
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType");
            ViewBag.userID = new SelectList(db.Users, "userID", "username");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientID,name,fatherName,nationalCode,jobtitle,homeAdress,healthStateID,mobile,mosqueID,pirorityID,need,maried,userID")] Client client, string birthDay)
        {
            if (ModelState.IsValid)
            {
                client.birthDay = birthDay.toMiladiDate();
                client.userID = user.userID;
                db.Clients.Add(client.EncodeClient());

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            return View(client);
        }

        // GET: Client/Edit/5
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
            if (client.userID != user.userID || user.isAdmin())
            {
                return HttpNotFound();

            }
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            return View(client.DecodeClient());
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,name,fatherName,nationalCode,jobtitle,homeAdress,healthStateID,mobile,mosqueID,pirorityID,need,maried,userID")] Client client, string birthDay)
        {
            if (ModelState.IsValid)
            {
                client.birthDay = birthDay.toMiladiDate();
                client.userID = user.userID;
                db.Entry(client.EncodeClient()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", client.healthStateID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", client.mosqueID);
            ViewBag.pirorityID = new SelectList(db.Piorities, "pirorityID", "priortyType", client.pirorityID);
            ViewBag.userID = new SelectList(db.Users, "userID", "username", client.userID);
            return View(client.EncodeClient());
        }

        // GET: Client/Delete/5
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
            return View(client.DecodeClient());
        }

        // POST: Client/Delete/5
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
