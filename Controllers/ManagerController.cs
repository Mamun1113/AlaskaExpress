using AlaskaExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AlaskaExpress.Controllers
{
    public class ManagerController : Controller
    {
        private Models.AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Manager
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