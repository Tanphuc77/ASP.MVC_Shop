using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        public ActionResult ThongKeSanPham()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); // Lấy số lượng người truy cập từ application đã được tạo 
            return View();
        }
    }
}