using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{
    public class UserController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: User
        public ActionResult IndexUser()
        {
            if (Session["MaKH"] !=null)
            {
                tb_Customer customer = (tb_Customer)Session["taikhoan"];
                var item = db.tb_Customer.FirstOrDefault(x => x.MaKH == customer.MaKH);
                return View(item);
            }
            return View();
        }
        public ActionResult EditUser(int id)
        {
            var item = db.tb_Customer.Find(id);
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(tb_Customer model)
        {
       
            if (ModelState.IsValid)
            {
                db.tb_Customer.Attach(model);
                model.UpdatedDate = DateTime.Now;
                db.Entry(model).Property(x => x.ImageUser).IsModified = true;
                db.Entry(model).Property(x => x.HoTen).IsModified = true;
                db.Entry(model).Property(x => x.Email).IsModified = true;
                db.Entry(model).Property(x => x.Phone).IsModified = true;
                db.Entry(model).Property(x => x.Address).IsModified = true;
                db.Entry(model).Property(x => x.GioiTinh).IsModified = true;
                db.Entry(model).Property(x => x.NgaySinh).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedDate).IsModified = true;
                db.Entry(model).Property(x => x.UpdatedBy).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("IndexUser");
            }
            return View(model);
        }

    }
}