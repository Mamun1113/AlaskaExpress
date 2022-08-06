using AlaskaExpress.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI;

namespace AlaskaExpress.Controllers
{
    public class HomeController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        public ActionResult Index()
        {

            var sql = "SELECT * FROM Bus";
            List<Bus> searchedBus = db.Buses.SqlQuery(sql).ToList();

            List<string> startLocation = new List<string>();
            List<string> endLocation = new List<string>();

            foreach (var item in searchedBus)
            {
                if (!startLocation.Contains(item.Bus_start_location))
                {
                    startLocation.Add(item.Bus_start_location);
                }

                if (!endLocation.Contains(item.Bus_end_location))
                {
                    endLocation.Add(item.Bus_end_location);
                }
            }

            ViewBag.startLocation = startLocation;
            ViewBag.endLocation = endLocation;
            return View();
        }

        public ActionResult BusDetails()
        {
            return RedirectToAction("BusDetails", "Bus");
        }


        public ActionResult TicketDownload()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["userEmail"] != null)
            {
                return RedirectToAction("", "Home");
            }
            else
            {
                var sql = "SELECT * FROM Admin";
                List<Admin> admin = db.Admins.SqlQuery(sql).ToList();

                ViewBag.admin = admin;

                return View();
            }
        }

        public ActionResult KillSession()
        {
            Session.RemoveAll();
            return RedirectToAction("", "Home");
        }

        public ActionResult AuthorizeLogin(string inputEmailForSignin, string inputPasswordForSignin, string inputRoleForSignin)
        {
            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                var adminDetails = db.Admins.Where(user => user.Admin_email == inputEmailForSignin && user.Admin_password == inputPasswordForSignin).FirstOrDefault();
                var managerDetails = db.Managers.Where(user => user.Manager_email == inputEmailForSignin && user.Manager_password == inputPasswordForSignin).FirstOrDefault();
                var sellerDetails = db.Sellers.Where(user => user.Seller_email == inputEmailForSignin && user.Seller_password == inputPasswordForSignin).FirstOrDefault();
                var customerDetails = db.Customers.Where(user => user.Customer_email == inputEmailForSignin && user.Customer_password == inputPasswordForSignin).FirstOrDefault();

                if (adminDetails != null)
                {
                    Session["userEmail"] = adminDetails.Admin_email;
                    Session["userRole"] = "Admin";
                    return RedirectToAction("ManagerList", "Admin");
                }
                else if (managerDetails != null)
                {
                    Session["userEmail"] = managerDetails.Manager_email;
                    Session["userRole"] = "Manager";
                    return RedirectToAction("Index", "Manager");
                }
                else if (sellerDetails != null)
                {
                    Session["userEmail"] = sellerDetails.Seller_email;
                    Session["userRole"] = "Seller";
                    return RedirectToAction("Index", "Seller");
                }
                else if (customerDetails != null)
                {
                    Session["userEmail"] = customerDetails.Customer_email;
                    Session["userRole"] = "Customer";
                    return RedirectToAction("Index", "Customer");
                }
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult AuthorizeSignup(string inputFullnameForSignup, string inputPhoneForSignup, string inputDobForSignup, string inputGenderForSignup, string inputNidForSignup, string inputAddressForSignup, string inputEmailForSignup, string inputPasswordForSignup)
        {
            using (AlaskaExpressEntities db = new AlaskaExpressEntities())
            {
                var customerDetails = db.Customers.Where(user => user.Customer_email == inputEmailForSignup).FirstOrDefault();

                if (customerDetails != null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else if (customerDetails == null)
                {
                    System.Data.SqlClient.SqlConnection con = new SqlConnection(@"Data Source=MEGATRONM609\SQLEXPRESS;Initial Catalog=AlaskaExpress; Integrated Security=True");
                    SqlCommand sql;
                    con.Open();

                    sql = new SqlCommand("insert into Customer values('" + inputEmailForSignup + "','" + inputPasswordForSignup + "','" + inputFullnameForSignup + "','" + inputDobForSignup + "','" + inputGenderForSignup + "','" + inputAddressForSignup + "','" + inputPhoneForSignup + "', '" + inputNidForSignup + "')", con);
                    sql.ExecuteNonQuery();
                    con.Close();

                    //Session["userEmail"] = inputEmailForSignup;
                    return RedirectToAction("Login", "Home");
                }
            }

            return RedirectToAction("Signup", "Home");
        }
    }
}


public static class MessageBox
{
    public static void Show(this Page Page, String Message)
    {
        Page.ClientScript.RegisterStartupScript(
           Page.GetType(),
           "MessageBox",
           "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }
}
