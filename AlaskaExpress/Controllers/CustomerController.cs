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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

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

        public ActionResult TicketPending()
        {
            return View();
        }

        public ActionResult TicketComplete(string scheduleID, string selectedAllSeatsforTicket, string txnId, string calculatedTotalprice)
        {
            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                System.Data.SqlClient.SqlConnection con = new SqlConnection(@"Data Source=MEGATRONM609\SQLEXPRESS;Initial Catalog=AlaskaExpress; Integrated Security=True");
                SqlCommand sql;
                con.Open();

                string userEmail = (string)Session["userEmail"];

                sql = new SqlCommand("INSERT INTO Ticket (Bus_seats,Schedule_id,Customer_email,Total_price,TXN_id,Ticket_state) VALUES('" + selectedAllSeatsforTicket + "'," + scheduleID + ",'" + userEmail + "'," + calculatedTotalprice + ",'" + txnId + "', "+0+")", con);
                sql.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("MyTickets", "Customer");  
            }
        }

        public ActionResult MyTickets()
        {
            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {

                var sql = "SELECT * FROM Ticket WHERE Customer_email = '" + Session["userEmail"] + "'";
                List<Ticket> ticketDetails = db.Tickets.SqlQuery(sql).ToList();

                return View(ticketDetails);
            }
        }



        public ActionResult TicketDownload(long ticketId)
        {
            string ticket_id = ticketId.ToString();

            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                var ticketValues = db.Tickets.Where(user => user.Ticket_id == ticketId).FirstOrDefault();

                string cusName =  ticketValues.Customer.Customer_fullname;

            


            //Create an instance of PdfDocument.
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("AlaskaExpress", font, PdfBrushes.Blue, new PointF(0, 0));
                graphics.DrawString("Ticket ID: "+ticket_id, font, PdfBrushes.Brown, new PointF(0, 20));
                graphics.DrawString("Customer Name: "+ ticketValues.Customer.Customer_fullname, font, PdfBrushes.Brown, new PointF(0, 40));
                graphics.DrawString("Phone: "+ ticketValues.Customer.Customer_phone, font, PdfBrushes.Brown, new PointF(0, 60));
                graphics.DrawString("Email: "+ ticketValues.Customer.Customer_email, font, PdfBrushes.Brown, new PointF(0, 80));
                graphics.DrawString("Bus: "+ ticketValues.Schedule.Bus.Bus_numberplate, font, PdfBrushes.Green, new PointF(0, 100));
                graphics.DrawString("From: "+ ticketValues.Schedule.Bus.Bus_start_location, font, PdfBrushes.Green, new PointF(0, 120));
                graphics.DrawString("To: "+ ticketValues.Schedule.Bus.Bus_end_location, font, PdfBrushes.Green, new PointF(0, 140));
                graphics.DrawString("Date: "+ ticketValues.Schedule.Bus_journet_day, font, PdfBrushes.Coral, new PointF(0, 160));
                graphics.DrawString("Time: "+ ticketValues.Schedule.Bus_journey_time, font, PdfBrushes.Coral, new PointF(0, 180));
                graphics.DrawString("Seats: "+ ticketValues.Bus_seats, font, PdfBrushes.Coral, new PointF(0, 200));

                string ticketName = "Ticket" + ticket_id + ".pdf";
                // Open the document in browser after saving it
                document.Save(ticketName, HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }



                var sql = "SELECT * FROM Ticket WHERE Customer_email = '" + Session["userEmail"] + "'";
                List<Ticket> ticketDetails = db.Tickets.SqlQuery(sql).ToList();

                return View("MyTickets", ticketDetails);
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
