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
    public class ManagersController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Managers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SellerList()
        {
            return View(db.Sellers.ToList());
        }

        // GET: Managers/Create
        public ActionResult CreateSeller()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Managers/Delete/5

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
