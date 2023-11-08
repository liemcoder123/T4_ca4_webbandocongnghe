using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{

    public class HomeController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        public ActionResult IndexHome()
        {
            return View();
        }
        public ActionResult ProductByCategory()
        {
            var items = db.tb_Product.Where(n => n.IsSoldOut == false && n.IsActive == true && n.IsNew == true).ToList();
            return PartialView(items);
        }
        public ActionResult DiscountProduct()
        {
            var items = db.tb_Product.Where(n => n.IsSoldOut == false && n.IsActive == true && n.IsSale == true).ToList();
            return PartialView(items);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

    }
}