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
    public class ClientMemberController : Controller
    {
        private AstanEntities db = new AstanEntities();
        User user = new User();
        public ClientMemberController()
        {
            if (System.Web.HttpContext.Current.Session["RPG"] != null)
            {
                user = System.Web.HttpContext.Current.Session["RPG"] as User;
            }
        }
        // GET: ClientMember
        public ActionResult Index(long? id)
        {
            IEnumerable<ClientMember> clientMembers;
            if (id.HasValue)
            {
                clientMembers = db.ClientMembers.Include(c => c.Client).Include(c => c.HealthState).Where(c => c.clientID == id);
            }
            else
            {
                clientMembers = db.ClientMembers.Include(c => c.Client).Include(c => c.HealthState);
            }
            if (!user.isAdmin())
            {
                return View(clientMembers.Where(c => c.Client?.userID == user.userID).ToList());
            }
            return View(clientMembers.ToList());

        }

        // GET: ClientMember/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientMember clientMember = db.ClientMembers.Find(id);
            if (clientMember == null)
            {
                return HttpNotFound();
            }
            return View(clientMember);
        }

        // GET: ClientMember/Create
        public ActionResult Create()
        {
            var isAdmin = user.isAdmin();
            ViewBag.clientID = new SelectList(db.Clients.Where(c=> c.userID==user.userID||isAdmin).DecodeClients(), "clientID", "name");
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType");
            return View();
        }

        // POST: ClientMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientMemberID,clientID,name,age,healthStateID,need")] ClientMember clientMember)
        {
            if (ModelState.IsValid)
            {
                db.ClientMembers.Add(clientMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var isAdmin = user.isAdmin();
            ViewBag.clientID = new SelectList(db.Clients.Where(c => c.userID == user.userID || isAdmin).DecodeClients(), "clientID", "name",clientMember.clientID);

            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", clientMember.healthStateID);
            return View(clientMember);
        }

        // GET: ClientMember/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientMember clientMember = db.ClientMembers.Find(id);
            if (clientMember == null)
            {
                return HttpNotFound();
            }

            var isAdmin = user.isAdmin();
            ViewBag.clientID = new SelectList(db.Clients.Where(c => c.userID == user.userID || isAdmin).DecodeClients(), "clientID", "name", clientMember.clientID);
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", clientMember.healthStateID);
            return View(clientMember);
        }

        // POST: ClientMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientMemberID,clientID,name,age,healthStateID,need")] ClientMember clientMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var isAdmin = user.isAdmin();
            ViewBag.clientID = new SelectList(db.Clients.Where(c => c.userID == user.userID || isAdmin).DecodeClients(), "clientID", "name", clientMember.clientID);
            ViewBag.healthStateID = new SelectList(db.HealthStates, "healthStateID", "healthStateType", clientMember.healthStateID);
            return View(clientMember);
        }

        // GET: ClientMember/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientMember clientMember = db.ClientMembers.Find(id);
            if (clientMember == null)
            {
                return HttpNotFound();
            }
            return View(clientMember);
        }

        // POST: ClientMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ClientMember clientMember = db.ClientMembers.Find(id);
            db.ClientMembers.Remove(clientMember);
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
