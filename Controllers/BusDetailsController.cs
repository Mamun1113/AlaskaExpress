using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlaskaExpress.Models
{
    public class BusDetailsController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: BusDetails
        public ActionResult Index()
        {
            return View(db.BusDetails.ToList());
        }

        // GET: BusDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetail busDetail = db.BusDetails.Find(id);
            if (busDetail == null)
            {
                return HttpNotFound();
            }
            return View(busDetail);
        }

        // GET: BusDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "busID,busFrom,busTo,totalSeat,busCoach,CostPerSeat,journeyDate")] BusDetail busDetail)
        {
            if (ModelState.IsValid)
            {
                db.BusDetails.Add(busDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(busDetail);
        }

        // GET: BusDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetail busDetail = db.BusDetails.Find(id);
            if (busDetail == null)
            {
                return HttpNotFound();
            }
            return View(busDetail);
        }

        // POST: BusDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "busID,busFrom,busTo,totalSeat,busCoach,CostPerSeat,journeyDate")] BusDetail busDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(busDetail);
        }

        // GET: BusDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDetail busDetail = db.BusDetails.Find(id);
            if (busDetail == null)
            {
                return HttpNotFound();
            }
            return View(busDetail);
        }

        // POST: BusDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusDetail busDetail = db.BusDetails.Find(id);
            db.BusDetails.Remove(busDetail);
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
