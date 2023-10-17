using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
namespace WebsiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Sanpham
        public ActionResult Sanphamstyle1Partial()
        {
            return View();
        }
        public ActionResult Sanphamstyle2Partial()
        {
            return View();
        }
        // xây dựng trang xem chi tiết
        public ActionResult ChiTietSanPham(int? id,string tensp)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.MaSP == id);
            return View(sanpham);

        }
    }
}