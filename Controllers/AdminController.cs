using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
namespace WebsiteBanHang.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult HomeAdmin()
        {
            return View();
        }
    }
}