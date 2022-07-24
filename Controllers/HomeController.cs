using AlaskaExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlaskaExpress.Controllers
{
    public class HomeController : Controller
    {
        AlaskaExpressEntities db = new AlaskaExpressEntities();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetBus(string fromJourney, string toJourney, string journeyDate)
        {

            var sql = "SELECT * FROM BusDetails WHERE busFrom= '" + fromJourney + "' AND busTo='" + toJourney + "' AND journeyDate='" + journeyDate + "'";
            List<BusDetail> busDetails = db.BusDetails.SqlQuery(sql).ToList();

            if (!isValidDate(journeyDate) || !isValidPlace(fromJourney) || !isValidPlace(toJourney) || fromJourney==toJourney)
            {
                return RedirectToAction("Index", "Home");
            }
            else if(busDetails.Count!=0)
            {
                return View("GetBus", busDetails);
               
            }
            else
            {

                return RedirectToAction("Index", "Home");

            }
            //Lisu
            /*ViewBag.fromJourney = fromJourney;
            ViewBag.toJourney = toJourney;
            ViewBag.journeyDate = journeyDate;

            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                var BusDetails = db.BusDetails.Where(user => user.busFrom == fromJourney && user.busTo == toJourney && user.journeyDate==journeyDate).FirstOrDefault();

                if (BusDetails != null)
                {
                    var sql1 = "SELECT * FROM BusDetails where busFrom='" + fromJourney + "'";
                    List<BusDetail> busdet = db.BusDetails.SqlQuery(sql1).ToList();
                    return View("GetBus", busdet);
                }
            }*/

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Signup()
        {
            
            ViewBag.Message = "Your Signup page.";

            return View();
        }

        public ActionResult Login()
        {
            var sql = "SELECT * FROM Admin";
            List<Admin> admin = db.Admins.SqlQuery(sql).ToList();

            ViewBag.admin = admin;

            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            return View();
        }

        public ActionResult AuthorizeAdmin(string inputEmailForSignin, string inputPasswordForSignin)
        {

            ViewBag.emailForSignin = inputEmailForSignin;
            ViewBag.passwordForSignin = inputPasswordForSignin;

            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                var adminDetails = db.Admins.Where(user => user.Admin_email == inputEmailForSignin && user.Admin_password == inputPasswordForSignin).FirstOrDefault();

                if (adminDetails != null)
                {
                    Session["userEmail"] = adminDetails.Admin_email;
                    return RedirectToAction("ManagerUpdate", "Admin");
                }
            }

            ViewBag.test = "try";

            return RedirectToAction("Login", "Home");
        }

        public Boolean isValidPlace(string s)
        {
            if (s == "Dhaka" || s == "dhaka") return true;
            if (s == "Chittagoan" || s == "chittagoan") return true;
            if (s == "Rajshahi" || s == "rajshahi") return true;
            if (s == "Khulna" || s == "khulna") return true;
            if (s == "Barisal" || s == "barisal") return true;
            if (s == "Sylhet" || s == "sylhet") return true;
            if (s == "Rangpur" || s == "rangpur") return true;
            return false;
        }

        public Boolean isValidDate(string s)
        {
            if (s.Length != 10) return false;
            for (int i = 0; i < 2; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            if (s[2] != '/') return false;
            for (int i = 3; i < 5; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            if (s[5] != '/') return false;
            for (int i = 6; i < 10; i++)
            {
                if (!isNumber(s[i])) return false;
            }
            return true;
        }

        public Boolean isNumber(char c)
        {
            if (c < '0' || c > '9') return false;
            return true;
        }
    }
}