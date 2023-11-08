using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{
    public class OrderUserController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: OrderUser
        public ActionResult IndexOrderUser()
        {
            return View();
        }
        public ActionResult OrderUser(int? page)
        {
            if (Session["MaKH"] != null)
            {
                tb_Customer customer = (tb_Customer)Session["taikhoan"];
                int pageNumber = (page ?? 1);
                int pageSize = 5;
                var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n =>n.MaKH==customer.MaKH&& n.IsHoanThanh == false && n.IsHuyDon == false).ToPagedList(pageNumber, pageSize);
                return PartialView(item);
            }
            return PartialView();
        }
        public ActionResult OrderUserDetail(int id)
        {

            var item = db.tb_Order.Find(id);
            return View(item);
        }
        public ActionResult Partial_OrderUserDetail(int id)
        {
            var item = db.tb_ChiTietOrder.Where(x => x.MaDonHang == id).ToList();
            return PartialView(item);
        }
        public ActionResult Partial_SoTaiKhoan()
        {
            var item = db.tb_TaiKhoanNganHang.OrderBy(n => n.MaSoTaiKhoan).ToList();
            return PartialView(item);
        }
        public ActionResult CompleteOrderUser(int? page)
        {
            if (Session["MaKH"] != null)
            {
                tb_Customer customer = (tb_Customer)Session["taikhoan"];
                int pageNumber = (page ?? 1);
                int pageSize = 5;
                var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n => n.MaKH == customer.MaKH && n.IsHoanThanh == true).ToPagedList(pageNumber, pageSize);
                return PartialView(item);
            }
            return PartialView();
        }
        public ActionResult CancelOrderUser(int? page)
        {
            if (Session["MaKH"] != null)
            {
                tb_Customer customer = (tb_Customer)Session["taikhoan"];
                int pageNumber = (page ?? 1);
                int pageSize = 5;
                var item = db.tb_Order.OrderByDescending(n => n.CreateDate).Where(n => n.MaKH == customer.MaKH && n.IsHuyDon == true).ToPagedList(pageNumber, pageSize);
                return PartialView(item);
            }
            return PartialView();
      
        }
    }
}