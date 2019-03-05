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

    public class UserController : Controller
    {
        private AstanEntities db = new AstanEntities();
        User user = new User();
        public UserController()
        {
            if (System.Web.HttpContext.Current.Session["RPG"] != null)
            {
                user = System.Web.HttpContext.Current.Session["RPG"] as User;
            }
        }
        [AllowAnonymous]
        public ActionResult Login() { return View(); }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.Where(c => c.username == username && c.password == password).FirstOrDefault();
            if (user != null && user.userGroupID != 3)
            {
                Session["RPG"] = user;
                return RedirectToAction("index", "home");
            }
            else if (user != null && user.userGroupID == 3)
            {
                Session["RPG"] = user;
                return RedirectToAction("index", "home");
            }
            ViewBag.message = "نام کاربری یا کلمه عبور اشتباه است";
            return View();

        }

        public ActionResult LogOut()
        {
            Session["RPG"] = null;
            return View("Login");
        }

        // GET: User
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.userGroup);
            if (user.isAdmin())
            {
                return View(users.ToList());

            }
            return View(users.Where(c => c.userID == user.userID));
        }

        // GET: User/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.userGroupID = new SelectList(db.userGroups, "userGroupID", "userGroupName");
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,username,password,name,mobile,userGroupID,mosqueID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userGroupID = new SelectList(db.userGroups, "userGroupID", "userGroupName", user.userGroupID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName",user.mosqueID);

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.userGroupID = new SelectList(db.userGroups, "userGroupID", "userGroupName", user.userGroupID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", user.mosqueID);

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,username,password,name,mobile,userGroupID,mosqueID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userGroupID = new SelectList(db.userGroups, "userGroupID", "userGroupName", user.userGroupID);
            ViewBag.mosqueID = new SelectList(db.Mosques, "mosqueID", "mosqueName", user.mosqueID);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
