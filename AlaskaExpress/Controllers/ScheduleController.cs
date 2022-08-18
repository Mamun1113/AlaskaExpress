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
    public class ScheduleController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Bus).Include(s => s.Seller);
            return View(schedules.ToList());
        }

        public ActionResult CheckUser(int id)
        {
            if (Session["userEmail"] == null)
            {
                Response.Write("<script>alert('You must logged in as a seller or a customer.');</script>");
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View("SeatAvailability", id);
            }
        }

        static int addhoise = 0;
        static int schedule_id;
        static List<ButtonModel> btmodel = new List<ButtonModel>();

        public ActionResult SeatAvailability(int id)
        {
            schedule_id = id;

            if (Session["userEmail"] != null)
            {
                var sql = "SELECT * FROM Schedule WHERE schedule_id= '" + schedule_id + "'";

                List<Schedule> busSeats = db.Schedules.SqlQuery(sql).ToList();

                if (addhoise != id)
                {
                    btmodel.Clear();
                    btmodel.Add(new ButtonModel((int)busSeats[0].A1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].A2));

                    btmodel.Add(new ButtonModel((int)busSeats[0].B1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].B2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].B3));
                    btmodel.Add(new ButtonModel((int)busSeats[0].B4));

                    btmodel.Add(new ButtonModel((int)busSeats[0].C1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].C2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].C3));
                    btmodel.Add(new ButtonModel((int)busSeats[0].C4));

                    btmodel.Add(new ButtonModel((int)busSeats[0].D1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].D2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].D3));
                    btmodel.Add(new ButtonModel((int)busSeats[0].D4));

                    btmodel.Add(new ButtonModel((int)busSeats[0].E1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].E2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].E3));
                    btmodel.Add(new ButtonModel((int)busSeats[0].E4));

                    btmodel.Add(new ButtonModel((int)busSeats[0].F1));
                    btmodel.Add(new ButtonModel((int)busSeats[0].F2));
                    btmodel.Add(new ButtonModel((int)busSeats[0].F3));
                    btmodel.Add(new ButtonModel((int)busSeats[0].F4));


                    addhoise = id;
                }
                return View("SeatAvailability", btmodel);
            }
            else
            {
                ViewBag.returnUrl = Request.UrlReferrer;
                Response.Write("<script>alert('You must logged in as a seller or a customer.');</script>");
                return View("~/Views/Home/Login.cshtml");
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

            return View("~/Views/Shared/SeatAvailability.cshtml", btmodel);
        }

        //            return View("~/Views/Shared/SeatAvailability.cshtml", btmodel);






















        // GET: Schedules/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.Bus_id = new SelectList(db.Buses, "Bus_id", "Bus_start_location");
            ViewBag.Schedule_addedby = new SelectList(db.Sellers, "Seller_email", "Seller_password");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Schedule_id,Bus_journey_time,Bus_journet_day,A1,A2,B1,B2,B3,B4,C1,C2,C3,C4,D1,D2,D3,D4,E1,E2,E3,E4,F1,F2,F3,F4,Bus_id,Schedule_addedby")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bus_id = new SelectList(db.Buses, "Bus_id", "Bus_start_location", schedule.Bus_id);
            ViewBag.Schedule_addedby = new SelectList(db.Sellers, "Seller_email", "Seller_password", schedule.Schedule_addedby);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bus_id = new SelectList(db.Buses, "Bus_id", "Bus_start_location", schedule.Bus_id);
            ViewBag.Schedule_addedby = new SelectList(db.Sellers, "Seller_email", "Seller_password", schedule.Schedule_addedby);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Schedule_id,Bus_journey_time,Bus_journet_day,A1,A2,B1,B2,B3,B4,C1,C2,C3,C4,D1,D2,D3,D4,E1,E2,E3,E4,F1,F2,F3,F4,Bus_id,Schedule_addedby")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bus_id = new SelectList(db.Buses, "Bus_id", "Bus_start_location", schedule.Bus_id);
            ViewBag.Schedule_addedby = new SelectList(db.Sellers, "Seller_email", "Seller_password", schedule.Schedule_addedby);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
