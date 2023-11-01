using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
namespace WebsiteBanHang.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult ThongTinSanPham()
        {
            return View(db.SanPhams.Where(m=>m.DaXoa == false));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(m => m.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemMoi(SanPham sanPham, HttpPostedFileBase HinhAnh) // giao thức truyền dữ liệu hình ảnh 
        {
            // load DropDownList
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(m => m.TenNSX), "MaNSX", "TenNSX");
            // Kiểm tra hình ảnh đã tồn tại hay chưa 
            if (HinhAnh.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh.SaveAs(path);
                }
            }
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("ThongTinSanPham");
        }
    }
}