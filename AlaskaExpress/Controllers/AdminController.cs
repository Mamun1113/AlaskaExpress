using AlaskaExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlaskaExpress.Controllers
{
    public class AdminController : Controller
    {
        private Models.AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Admins
        public ActionResult ManagerList()
        {
            /*
             * List<Manager> managers = db.Managers.ToList();

            var sql = "select * from Manager";
            List<Manager> managers2 = db.Managers.SqlQuery(sql).ToList();

            return View(managers);
            */
            return View(db.Managers.ToList());
        }

        // GET: Admins/Create
        public ActionResult CreateManager()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManager([Bind(Include = "Manager_email,Manager_password,Manager_fullname,Manager_address,Manager_nid,Manager_phone")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("ManagerList");
            }

            return View();
        }

        // GET: Admins/Delete/5
        public ActionResult DeleteManager()
        {
            ViewBag.managers = db.Managers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult DeleteManager(string Manager_email)
        {
            Manager manager = db.Managers.Where(temp => temp.Manager_email == Manager_email).FirstOrDefault();
            db.Managers.Remove(manager);
            db.SaveChanges();
            ViewBag.managers = db.Managers.ToList();
            return View();
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
