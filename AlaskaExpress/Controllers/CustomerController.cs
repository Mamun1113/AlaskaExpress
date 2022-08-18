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
    public class CustomerController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindBus()
        {
            var sql = "SELECT * FROM Schedule";
            List<Schedule> searchedBus = db.Schedules.SqlQuery(sql).ToList();

            List<string> startLocation = new List<string>();
            List<string> endLocation = new List<string>();

            foreach (var item in searchedBus)
            {
                if (!startLocation.Contains(item.Bus.Bus_start_location))
                {
                    startLocation.Add(item.Bus.Bus_start_location);
                }

                if (!endLocation.Contains(item.Bus.Bus_end_location))
                {
                    endLocation.Add(item.Bus.Bus_end_location);
                }
            }

            ViewBag.startLocation = startLocation;
            ViewBag.endLocation = endLocation;
            return View();
        }

        public ActionResult SearchedBus(string inputJourneyFrom, string inputJourneyTo, string inputJourneyDate)
        {
            var sql = "SELECT * FROM Schedule INNER JOIN Bus ON Schedule.Bus_id = Bus.Bus_id WHERE Bus_start_location= '" + inputJourneyFrom + "' AND Bus_end_location='" + inputJourneyTo + "' AND Bus_journet_day='" + inputJourneyDate + "' ";
            List<Schedule> busDetails = db.Schedules.SqlQuery(sql).ToList();

            if (busDetails.Count != 0)
            {
                return View("SearchedBus", busDetails);
            }
            else
            {
                Response.Write("<script>alert('No bus found');</script>");

                var sql2 = "SELECT * FROM Schedule";
                List<Schedule> searchedBus = db.Schedules.SqlQuery(sql2).ToList();

                List<string> startLocation = new List<string>();
                List<string> endLocation = new List<string>();

                foreach (var item in searchedBus)
                {
                    if (!startLocation.Contains(item.Bus.Bus_start_location))
                    {
                        startLocation.Add(item.Bus.Bus_start_location);
                    }

                    if (!endLocation.Contains(item.Bus.Bus_end_location))
                    {
                        endLocation.Add(item.Bus.Bus_end_location);
                    }
                }

                ViewBag.startLocation = startLocation;
                ViewBag.endLocation = endLocation;
                return View("FindBus");
            }
        }



















        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_email,Customer_password,Customer_fullname,Customer_dob,Customer_address,Customer_phone,Customer_nid")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_email,Customer_password,Customer_fullname,Customer_dob,Customer_address,Customer_phone,Customer_nid")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
