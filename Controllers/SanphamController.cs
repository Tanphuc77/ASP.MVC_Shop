using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
using System.Web.UI;

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
        public ActionResult SidebarDonGia1_5( int? Page)
        {
            var sanPham = db.SanPhams.Where(m=> m.DonGia <= 5000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            ViewBag.DonGia = sanPham;
            return View(sanPham.OrderBy(m => m.DonGia).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGia5_10( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia > 5000000 && m.DonGia <= 10000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGia10_15( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia >= 10000000 && m.DonGia <= 15000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGia15_30( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia > 15000000 && m.DonGia <= 30000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGia30_50( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia > 30000000 && m.DonGia <= 50000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGia50_100( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia > 50000000 && m.DonGia <= 100000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SidebarDonGiaTren100( int? Page)
        {
            var sanPham = db.SanPhams.Where(m =>  m.DonGia > 100000000).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);
            return View(sanPham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
    }
}