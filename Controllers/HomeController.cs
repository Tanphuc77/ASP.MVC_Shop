using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Home
        public ActionResult DanhSachSanPham()
        {
            // lần lượt lấy các viewbag để lấy list các sản phẩm từ csdl 
            // Danh sách điện thoại
            var listDienThoai = db.SanPhams.Where(m => m.MaLoaiSP == 1).Take(12).ToList();
            return View(listDienThoai);
        }
        public ActionResult Menupartial()
        {
            // Truy vấn lấy về 1 list các sản phẩm
            var listSanPham = db.SanPhams;
            return PartialView(listSanPham);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien model, FormCollection f)
        {
            // Kiem tra capcha 
            if (this.IsCaptchaValid("Capcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Đăng ký thành công";
                    db.ThanhViens.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký không thành công";
                }
                return View();
            }
            ViewBag.ThongBao = "Sai mã Captcha";
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            // Kiểm tra tên đăng nhập và mật khẩu 
            string Username = f["Username"].ToString();
            string Passwork = f["Password"].ToString();

            ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.TaiKhoan == Username && m.MatKhau == Passwork);
            if (thanhVien != null)
            {
                Session["TaiKhoan"] = thanhVien;
                return RedirectToAction("DanhSachSanPham");
            }
            ViewBag.ThongBao = ("Tài khoản hoặc mật khẩu không đúng");
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("DanhSachSanPham");
        }
        public ActionResult SanPhamTheoLoai(int id)
        {
            var sanpham = db.SanPhams.Where(s => s.MaLoaiSP == id).ToList();
            return View(sanpham);
        }
        public ActionResult SanPhamTheoNhaSanXuat(int id)
        {
            var sanpham = db.SanPhams.Where(s => s.MANSX == id).ToList();
            return View(sanpham);
        }
    }
}