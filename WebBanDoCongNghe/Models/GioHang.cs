using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDoCongNghe.Models
{
    public class GioHang
    {
        DBQuanLyBanDoCongNgheEntities db = new DBQuanLyBanDoCongNgheEntities();
        public int iMaSanPham { get; set; }
        public string iTenSanPham { get; set; }
        public string iHinhAnh { get; set; }
        public string iLink { get; set; }
        public double iGiaSanPham { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * iGiaSanPham; }
        }
        //Hàm tạo giỏ hàng
        public GioHang(int MaSanPham)
        {
            iMaSanPham = MaSanPham;
            var product = db.tb_Product.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            iTenSanPham = product.TenSanPham;
            iHinhAnh = db.tb_ProductImages.FirstOrDefault(n=>n.IsDefault==true).Image;
            if (product.IsSale == true)
            {
                iGiaSanPham = Double.Parse(product.PriceSale.ToString());
            }
            else
            {
                iGiaSanPham = Double.Parse(product.Price.ToString());
            }

            iSoLuong = 1;
        }
    }
}