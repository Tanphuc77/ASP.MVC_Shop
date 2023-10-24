using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using PagedList;
using BCrypt.Net;
namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Home
        public ActionResult DanhSachSanPham(int? page)
        {
            // lần lượt lấy các viewbag để lấy list các sản phẩm từ csdl 
            // Danh sách điện thoại
            var sanPham = db.SanPhams.Where(m => m.MaLoaiSP == 1).OrderBy(m => m.MaSP).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanPham.ToPagedList(pageNumber, pageSize));
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

                if (ModelState.IsValid)//ModelState.IsValid Nó được sử dụng để kiểm tra xem liệu dữ liệu được submit từ trang web có hợp lệ hay không. 
                {
                    ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.TaiKhoan == model.TaiKhoan);
                    if (thanhVien == null)
                    {
                        string salt = BCrypt.Net.BCrypt.GenerateSalt();
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.MatKhau, salt);
                        model.MatKhau = hashedPassword;
                        model.ConfirmPassword = hashedPassword;
                        db.ThanhViens.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("DangNhap");
                    }
                    else
                    {
                        ViewBag.TrungLap = "Tên tài khoản giống với tài khoản khác";
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký không thành công";
                }
                return View();
            }
            ViewBag.Captcha = "Sai mã Captcha";
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
            string Password = f["Password"].ToString();

            ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(m => m.TaiKhoan == Username);
            if (thanhVien != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(Password, thanhVien.MatKhau);
                if (isPasswordCorrect)
                {
                Session["TaiKhoan"] = thanhVien;
                return RedirectToAction("DanhSachSanPham");
                }
            }
            ViewBag.ThongBao = ("Tài khoản hoặc mật khẩu không đúng");
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("DanhSachSanPham");
        }
        public ActionResult SanPhamTheoLoai(int id, int? page)
        {
            var sanpham = db.SanPhams.Where(s => s.MaLoaiSP == id).ToList();

            // Thực hiện phân trang theo loại 
            int pageSize = 9;// Số sản phẩm có trên trang 
            int pageNumbber = (page ?? 1); // số trang hiện tại

            ViewBag.MaLoai = id;
            return View(sanpham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
        public ActionResult SanPhamTheoNhaSanXuat(int id, int? Page)
        {
            var sanpham = db.SanPhams.Where(s => s.MANSX == id).ToList();
            int pageSize = 9;
            int pageNumbber = (Page ?? 1);

            ViewBag.MaNSX = id;
            return View(sanpham.OrderBy(m => m.MaSP).ToPagedList(pageNumbber, pageSize));
        }
    }
}