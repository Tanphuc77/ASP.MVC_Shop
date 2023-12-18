using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class QuyenController : Controller
    {
        // GET: Quyen
        public ActionResult DanhSachLoaiThanhVien()
        {
            return View();
        }
    }
}