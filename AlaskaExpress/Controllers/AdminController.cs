using AlaskaExpress.Models;
using System.Linq;
using System.Web.Mvc;

namespace AlaskaExpress.Controllers
{
    public class AdminController : Controller
    {
        private AlaskaExpressEntities db = new AlaskaExpressEntities();

        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        public ActionResult ManagerList()
        {
            return View(db.Managers.ToList());
        }

        public ActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManager([Bind(Include = "Manager_email,Manager_password,Manager_fullname,Manager_address,Manager_nid,Manager_phone,Manager_addedby")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("ManagerList");
            }

            return View();
        }

        public ActionResult SellerList()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            return View();
        }

        public ActionResult BusList()
        {
            return View();
        }

        public ActionResult SCheduleList()
        {
            return View();
        }

        public ActionResult TicketList()
        {
            return View();
        }



        /*
        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DeleteManager()
        {
            ViewBag.managers = db.Managers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult DeleteManager(string Manager_email)
        {
            Manager manager = db.Managers.Where(temp => temp.Manager_email == Manager_email).FirstOrDefault();
            db.Managers.Remove(manager);
            db.SaveChanges();
            ViewBag.managers = db.Managers.ToList();
            return View();
        }


        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admin_email,Admin_password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admin_email,Admin_password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
        */
    }
}
