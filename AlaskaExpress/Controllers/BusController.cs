using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AlaskaExpress.Models;
using DocumentFormat.OpenXml.Math;

namespace AlaskaExpress.Controllers
{
    public class BusController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();


        static int addhoise = 0;
        static int id;

        static List<ButtonModel> btmodel = new List<ButtonModel>();

        // GET: Bus
        public ActionResult Index()
        {
            return View(db.Buses.ToList());
        }

        public ActionResult BusAllDetails()
        {
            return View(db.Buses.ToList());
        }

        public ActionResult BusDetails()
        {
            return View(db.Buses.ToList());
        }

        public ActionResult SearchedBusDetails(string inputJourneyFrom, string inputJourneyTo, string inputJourneyDate)
        {
            DateTime dateTime = Convert.ToDateTime(inputJourneyDate);

            ViewBag.checkcheck = inputJourneyDate;

            //int week = Convert.ToInt32(dateTime.DayOfWeek);
            string day = Convert.ToString(dateTime.Day);

            var sql = "SELECT * FROM Bus WHERE Bus_start_location= '" + inputJourneyFrom + "' AND Bus_end_location='" + inputJourneyTo + "' AND Bus_journey_day='" + inputJourneyDate + "' ";
            List<Bus> busDetails = db.Buses.SqlQuery(sql).ToList();

            if (busDetails.Count != 0)
            {
                return View("SearchedBusDetails", busDetails);
            }
            else
            {
                if (Session["userEmail"] != null)
                {
                    return RedirectToAction("Index", "Seller");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            //return View(db.Buses.ToList());
        }

        // GET: Bus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Bus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Bus_id,Bus_start_location,Bus_end_location,Bus_total_seat,Bus_coach,Bus_cost_per_seat,Bus_journey_time,Bus_journey_day,Bus_numberplate")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index", "Manager");
            }

            return RedirectToAction("Index", "Manager");
        }

        public ActionResult AddBus()
        {
            return RedirectToAction("BusAllDetails");
        }

        // POST: Bus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBus([Bind(Include = "Bus_id,Bus_start_location,Bus_end_location,Bus_total_seat,Bus_coach,Bus_cost_per_seat,Bus_journey_time,Bus_journey_day,Bus_numberplate")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("BusAllDetails");
        }

        // GET: Bus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Bus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Bus_id,Bus_start_location,Bus_end_location,Bus_total_seat,Bus_coach,Bus_cost_per_seat,Bus_journey_time,Bus_journey_day,Bus_numberplate")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bus);
        }

        // GET: Bus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Bus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
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

        public ActionResult Seat(int idre)
        {

            if (Session["userEmail"] != null)
            {
                id = idre;
                var sql = "SELECT * FROM Seat WHERE Bus_id= '" + idre + "'";


                List<Seat> busSeats = db.Seats.SqlQuery(sql).ToList();
                //addhoise = 1;
                //Random random=new Random();
                /*if(busSeats.Count==0)
                 {
                     return RedirectToAction("GetBus", "Home");
                 }*/
                /*for(int i=0;i<16;i++)
                {
                    int val = random.Next(10);
                    btmodel.Add(new ButtonModel(val%3));

                }*/
                if (addhoise != idre)
                {
                    btmodel.Clear();
                    btmodel.Add(new ButtonModel((int)busSeats[0].A1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].A2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].B1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].B2));

                    btmodel.Add(new ButtonModel((int)busSeats[0].C1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].C2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].D1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].D2));

                    btmodel.Add(new ButtonModel((int)busSeats[0].E1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].E2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].F1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].F2));

                    btmodel.Add(new ButtonModel((int)busSeats[0].G1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].G2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].H1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].H2));
                    addhoise = idre;
                }
                return View("~/Views/Shared/Seat.cshtml", btmodel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        public ActionResult HandleSeatClick(string mine)
        {
            int stnnumber = Int32.Parse(mine);
            if (btmodel[stnnumber].State == 2)
            {
                btmodel[stnnumber].State = 2;
            }
            else
            {
                btmodel[stnnumber].State = (btmodel[stnnumber].State ^ 1);
            }

            return View("~/Views/Shared/Seat.cshtml", btmodel);
        }
    }
}
