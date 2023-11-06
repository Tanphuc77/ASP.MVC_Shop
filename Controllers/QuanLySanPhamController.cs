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
            return View(db.SanPhams.Where(m => m.DaXoa == false).OrderBy(m => m.MaLoaiSP).ToList());
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
        public ActionResult ThemMoi(SanPham sanPham, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh1, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3) // giao thức truyền dữ liệu hình ảnh 
        {
            // load DropDownList
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(m => m.TenNSX), "MaNSX", "TenNSX");
            // Kiểm tra hình ảnh đã tồn tại hay chưa 
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
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
            if (HinhAnh1 != null && HinhAnh1.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh1.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product-mini"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh1.SaveAs(path);
                }
            }
            if (HinhAnh2 != null && HinhAnh2.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh2.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product-mini"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh2.SaveAs(path);
                }
            }
            if (HinhAnh3 != null && HinhAnh3.ContentLength > 0)
            {
                // Lấy tên hình ảnh 
                var fileName = Path.GetFileName(HinhAnh3.FileName);
                // Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/assets/images/product-mini"), fileName);
                // Nếu có rồi thì thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    // lấy hình ảnh đưa vào thư mục 
                    HinhAnh3.SaveAs(path);
                }
            }
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("ThongTinSanPham");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(m => m.TenNCC), "MaNCC", "TenNCC", sp.MANCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(m => m.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(m => m.TenNSX), "MaNSX", "TenNSX", sp.MANSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThôngTinSanPham");
            }
            return View(model);
        }

    }
}