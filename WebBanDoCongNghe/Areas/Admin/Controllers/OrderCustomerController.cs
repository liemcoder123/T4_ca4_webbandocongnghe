using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Areas.Admin.Controllers
{
    public class OrderCustomerController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: Admin/OrderCustomer
        public ActionResult IndexOrderCustomer()
        {
            return View();
        }
        public ActionResult OrderCustomer(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n => n.IsHoanThanh== false && n.IsHuyDon == false).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult OrderCustomerDetail(int id)
        {

            var item = db.tb_Order.Find(id);
            return View(item);
        }
        public ActionResult Partial_OrderCustomerDetail(int id)
        {
            var item = db.tb_ChiTietOrder.Where(x => x.MaDonHang == id).ToList();
            return PartialView(item);
        }
        public ActionResult CompleteOrderCustomer(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n => n.IsHoanThanh == true).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult CancelOrderCustomer(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n => n.IsHuyDon == true).ToPagedList(pageNumber, pageSize);
            return PartialView(item);
        }
        public ActionResult EditOrderCustomer(int id)
        {
            var item = db.tb_Order.Find(id);
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrderCustomer(tb_Order model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsHoanThanh == true && model.IsHuyDon == true)
                {
                    return RedirectToAction("EditOrderCustomer");
                }
                else
                {
                    db.tb_Order.Attach(model);
                    model.UpdatedDate = DateTime.Now;

                    db.Entry(model).Property(x => x.IsThanhToan).IsModified = true;
                    db.Entry(model).Property(x => x.IsHoanThanh).IsModified = true;
                    db.Entry(model).Property(x => x.IsHuyDon).IsModified = true;
                    db.Entry(model).Property(x => x.UpdatedDate).IsModified = true;
                    db.Entry(model).Property(x => x.UpdatedBy).IsModified = true;
                    db.SaveChanges();

                    return RedirectToAction("IndexOrderCustomer");
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