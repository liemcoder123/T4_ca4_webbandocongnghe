using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: Admin/UserAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Customer.OrderBy(n => n.MaKH).Where(n=>n.IsAdmin==false).ToPagedList(pageNumber, pageSize);
            return View(item);
        }

        public ActionResult EditCustomer(int id)
        {
            var item = db.tb_Customer.Find(id);
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(tb_Customer model)
        {
            if (ModelState.IsValid)
            {
                db.tb_Customer.Attach(model);
                model.UpdatedDate = DateTime.Now;
               
                db.Entry(model).Property(x => x.IsActive).IsModified = true;
                db.Entry(model).Property(x => x.IsAdmin).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedDate).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedBy).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Customer");
            }
            return View(model);
        }

        public ActionResult AdminCustomer(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Customer.OrderBy(n => n.MaKH).Where(n => n.IsAdmin == true).ToPagedList(pageNumber, pageSize);
            return View(item);
        }

        public ActionResult EditAdminCustomer(int id)
        {
            var item = db.tb_Customer.Find(id);
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminCustomer(tb_Customer model)
        {
            if (ModelState.IsValid)
            {
                db.tb_Customer.Attach(model);
                model.UpdatedDate = DateTime.Now;

                db.Entry(model).Property(x => x.IsActive).IsModified = true;
                db.Entry(model).Property(x => x.IsAdmin).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedDate).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedBy).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Customer");
            }
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}