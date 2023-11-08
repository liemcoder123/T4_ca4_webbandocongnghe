using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{
    public class MenuController : Controller
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult MenuTop()
        {
            var items = db.tb_Category.Where(n=>n.IsActive==true).OrderBy(n => n.Position).ToList();
            return PartialView(items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = db.tb_ProductCategory.Where(n => n.IsActive == true).OrderBy(n => n.Position).ToList();
            return PartialView(items);
        }
    }
}
