using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
namespace WebsiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Sanphamstyle1Partial()
        {
            return PartialView();
        }
        public ActionResult Sanphamstyle2Partial()
        {
            return PartialView();
        }
        // xây dựng trang xem chi tiết
        public ActionResult ChiTietSanPham(int? id,string tensp)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.MaSP == id);
            return View(sanpham);
        }
        public ActionResult SidebarSanPhamPartial()
        {
            var sanPham = db.SanPhams.ToList();
            return View(sanPham);
        }
    }
}