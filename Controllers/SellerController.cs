using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlaskaExpress.Models;

namespace AlaskaExpress.Controllers
{
    public class SellerController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Seller
        public ActionResult IndexCopy()
        {
            var sellers = db.Sellers.Include(s => s.Manager);
            return View(sellers.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusList()
        {
            return View(db.Buses.ToList());
        }

        public ActionResult ScheduleList()
        {
            return View(db.Schedules.ToList());
        }



















        // GET: Seller/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Seller/Create
        public ActionResult Create()
        {
            ViewBag.Seller_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password");
            return View();
        }

        // POST: Seller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Seller_email,Seller_password,Seller_fullname,Seller_address,Seller_nid,Seller_phone,Seller_addedby")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Seller_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password", seller.Seller_addedby);
            return View(seller);
        }

        // GET: Seller/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            ViewBag.Seller_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password", seller.Seller_addedby);
            return View(seller);
        }

        // POST: Seller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Seller_email,Seller_password,Seller_fullname,Seller_address,Seller_nid,Seller_phone,Seller_addedby")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Seller_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password", seller.Seller_addedby);
            return View(seller);
        }

        // GET: Seller/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Seller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
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
