using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Areas.Admin.Controllers
{
    public class OrderTravelerController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();

        public ActionResult IndexOrderTraveler()
        {
            return View();
        }
        public ActionResult OrderTraveler(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Traveler.OrderByDescending(n => n.CreateDate).Where(n=>n.IsHoanThanh==false && n.IsHuyDon==false).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult OrderTravelerDetail(int id)
        {

            var item = db.tb_Traveler.Find(id);
            return View(item);
        }
        public ActionResult Partial_OrderTravelerDetail(int id)
        {
            var item = db.tb_ChiTietOrder_Traveler.Where(x => x.MaDonHang == id).ToList();
            return PartialView(item);
        }
        public ActionResult CompleteOrderTraveler(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Traveler.OrderByDescending(n => n.CreateDate).Where(n => n.IsHoanThanh == true).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult CancelOrderTraveler(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Traveler.OrderByDescending(n => n.CreateDate).Where(n => n.IsHuyDon == true).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult EditOrderTraveler(int id)
        {
            var item = db.tb_Traveler.Find(id);
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrderTraveler(tb_Traveler model)
        {
            if (ModelState.IsValid)
            {
                if(model.IsHoanThanh==true && model.IsHuyDon==true)
                {
                    return RedirectToAction("EditOrderTraveler");
                }
                else
                {
                    db.tb_Traveler.Attach(model);
                    model.UpdatedDate = DateTime.Now;

                    db.Entry(model).Property(x => x.IsThanhToan).IsModified = true;
                    db.Entry(model).Property(x => x.IsHoanThanh).IsModified = true;
                    db.Entry(model).Property(x => x.IsHuyDon).IsModified = true;
                    db.Entry(model).Property(x => x.UpdatedDate).IsModified = true;
                    db.Entry(model).Property(x => x.UpdatedBy).IsModified = true;
                    db.SaveChanges();

                    return RedirectToAction("IndexOrderTraveler");
                }
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