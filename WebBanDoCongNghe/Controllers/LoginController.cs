using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WebBanDoCongNghe.Models;

namespace WebBanDoCongNghe.Controllers
{
    public class LoginController : Controller
    {
        private DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tb_Customer model)
        {
            if (ModelState.IsValid)
            {
                var check = db.tb_Customer.FirstOrDefault(x => x.TaiKhoan == model.TaiKhoan);
                if (check == null)
                {
                    model.MatKhau = GetMD5(model.MatKhau);
                    model.IsAdmin = false;
                    model.IsActive = true;
                    model.CreateDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    db.tb_Customer.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại";
                    return View("Register", model); // Trả về view Register với model để hiển thị lại dữ liệu đã nhập
                }
            }
            ViewBag.error = "Tạo tài khoản thất bại xin vui lòng thử lại";
            return View("Register", model); // Trả về view Register với model để hiển thị lại dữ liệu đã nhập
        }

        public static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string taikhoan, string matkhau)
        {
            if (ModelState.IsValid)
            {
                var Password = GetMD5(matkhau);
                var customer = db.tb_Customer.FirstOrDefault(x => x.TaiKhoan.Equals(taikhoan) && x.MatKhau.Equals(Password));

                if (customer != null && (bool)customer.IsActive)
                {
                    Session["taikhoan"] = customer;
                    Session["MaKH"] = customer.MaKH;
                    Session["HoTen"] = customer.HoTen;
                    Session["Email"] = customer.Email;

                    if ((bool)customer.IsAdmin)
                    {
                        // Nếu là admin, chuyển hướng đến trang admin
                        return Redirect("/Admin/Home/Index");

                    }
                    else
                    {
                        // Nếu không phải admin, chuyển hướng đến trang người dùng thường
                        return RedirectToAction("IndexHome", "Home");
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không đúng hoặc tài khoản đã bị khóa";
                    return View("Login");
                }
            }

            ViewBag.error = "Đăng nhập thất bại xin vui lòng thử lại";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("IndexHome", "Home");
        }
    }
}
