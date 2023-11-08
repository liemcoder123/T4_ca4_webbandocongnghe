using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{
    public class ProductDetailsController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: ProductDetails
        public ActionResult ProductDetailsIndex(string link, int id)
        {
            var item = db.tb_Product.Find(id);
            return View(item);
        }
    }
}