using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public ActionResult SeatAvailability(long? id)
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

        public ActionResult ScheduleUpdate(long scheduleID, string selectedAllSeats,int seatCounter, string startLocation, string endLocation, string coach, int costPerSeat, string numberplate, int inputA1, int inputA2, int inputB1, int inputB2, int inputB3, int inputB4, int inputC1, int inputC2, int inputC3, int inputC4, int inputD1, int inputD2, int inputD3, int inputD4, int inputE1, int inputE2, int inputE3, int inputE4, int inputF1, int inputF2, int inputF3, int inputF4)
        {
            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                if (inputA1 == 1) inputA1 = 2;
                if (inputA2 == 1) inputA2 = 2;
                if (inputB1 == 1) inputB1 = 2;
                if (inputB2 == 1) inputB2 = 2;
                if (inputB3 == 1) inputB3 = 2;
                if (inputB4 == 1) inputB4 = 2;
                if (inputC1 == 1) inputC1 = 2;
                if (inputC2 == 1) inputC2 = 2;
                if (inputC3 == 1) inputC3 = 2;
                if (inputC4 == 1) inputC4 = 2;
                if (inputD1 == 1) inputD1 = 2;
                if (inputD2 == 1) inputD2 = 2;
                if (inputD3 == 1) inputD3 = 2;
                if (inputD4 == 1) inputD4 = 2;
                if (inputE1 == 1) inputE1 = 2;
                if (inputE2 == 1) inputE2 = 2;
                if (inputE3 == 1) inputE3 = 2;
                if (inputE4 == 1) inputE4 = 2;
                if (inputF1 == 1) inputF1 = 2;
                if (inputF2 == 1) inputF2 = 2;
                if (inputF3 == 1) inputF3 = 2;
                if (inputF4 == 1) inputF4 = 2;

                System.Data.SqlClient.SqlConnection con = new SqlConnection(@"Data Source=MEGATRONM609\SQLEXPRESS;Initial Catalog=AlaskaExpress; Integrated Security=True");
                SqlCommand sql;
                con.Open();

                sql = new SqlCommand("UPDATE Schedule SET [A1] = '"+ inputA1 + "', [A2]='" + inputA2 + "', [B1]='" + inputB1 + "', [B2]='" + inputB2 + "', [B3]='" + inputB3 + "', [B4]='" + inputB4 + "', [C1]='" + inputC1 + "', [C2]='" + inputC2 + "', [C3]='" + inputC3 + "', [C4]='" + inputC4 + "', [D1]='" + inputD1 + "', [D2]='" + inputD2 + "', [D3]='" + inputD3 + "', [D4]='" + inputD4 + "', [E1]='" + inputE1 + "', [E2]='" + inputE2 + "', [E3]='" + inputE3 + "', [E4]='" + inputE4 + "', [F1]='" + inputF1 + "', [F2]='" + inputF2 + "', [F3]='" + inputF3 + "', [F4]='" + inputF4 + "' WHERE Schedule_id = '" + scheduleID +"'", con);
                sql.ExecuteNonQuery();
                //con.Close();

                //var sql2 = "SELECT * FROM Schedule WHERE Schedule_id= '" + scheduleID + "'";
                //List<Schedule> updatedSchedule = db.Schedules.SqlQuery(sql2).ToList();

                Schedule schedule2 = db.Schedules.Find(scheduleID);

                ViewBag.scheduleIDforTicket = scheduleID;
                ViewBag.seatCounterforTicket = seatCounter;
                ViewBag.selectedAllSeatsforTicket = selectedAllSeats;
                ViewBag.startLocation = startLocation;
                ViewBag.endLocation = endLocation;
                ViewBag.coach = coach;
                ViewBag.costPerSeat = costPerSeat;
                ViewBag.numberplate = numberplate;
                return View("~/Views/Customer/TicketPending.cshtml", schedule2);
            }
        }















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
