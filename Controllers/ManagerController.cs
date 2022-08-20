﻿using System;
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
    public class ManagerController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SellerList()
        {
            return View(db.Sellers.ToList());
        }

        public ActionResult CreateSeller()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeller([Bind(Include = "Seller_email,Seller_password,Seller_fullname,Seller_address,Seller_nid,Seller_phone,Seller_image,Seller_addedby")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("SellerList");
            }

            return View();
        }

        public ActionResult DeleteSeller()
        {
            ViewBag.sellers = db.Sellers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DeleteSeller(string Seller_email)
        {
            Seller seller = db.Sellers.Where(temp => temp.Seller_email == Seller_email).FirstOrDefault();
            db.Sellers.Remove(seller);
            db.SaveChanges();
            ViewBag.sellers = db.Sellers.ToList();
            return View();
        }

        public ActionResult BusList()
        {
            return View(db.Buses.ToList());
        }

        public ActionResult CreateBus()
        {
            //ViewBag.Bus_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password");
            return View();
        }

        // POST: Bus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBus([Bind(Include = "Bus_start_location,Bus_end_location,Bus_cost_per_seat,Bus_total_seat,Bus_coach,Bus_numberplate,Bus_addedby")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("BusList");
            }

            ViewBag.Bus_addedby = new SelectList(db.Managers, "Manager_email", "Manager_password", bus.Bus_addedby);
            return View(bus);
        }

        public ActionResult DeleteBus()
        {
            ViewBag.buses = db.Buses.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBus(long Bus_id)
        {
            long id = Bus_id;
            Bus bus = db.Buses.Where(temp => temp.Bus_id == Bus_id).FirstOrDefault();
            db.Buses.Remove(bus);
            db.SaveChanges();
            ViewBag.buses = db.Buses.ToList();
            return View();
        }




















        // GET: Manager/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            ViewBag.Manager_addedby = new SelectList(db.Admins, "Admin_email", "Admin_password");
            return View();
        }

        // POST: Manager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Manager_email,Manager_password,Manager_fullname,Manager_address,Manager_nid,Manager_phone,Manager_addedby")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Manager_addedby = new SelectList(db.Admins, "Admin_email", "Admin_password", manager.Manager_addedby);
            return View(manager);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.Manager_addedby = new SelectList(db.Admins, "Admin_email", "Admin_password", manager.Manager_addedby);
            return View(manager);
        }

        // POST: Manager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Manager_email,Manager_password,Manager_fullname,Manager_address,Manager_nid,Manager_phone,Manager_addedby")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Manager_addedby = new SelectList(db.Admins, "Admin_email", "Admin_password", manager.Manager_addedby);
            return View(manager);
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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