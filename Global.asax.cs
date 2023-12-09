using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Application["SoNguoiTruyCap"] = 0;
            Application["SoNguoiDangHoatDong"] = 0;
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["SoNguoiTruyCap"] = (int)Application["SoNguoiTruyCap"] + 1;
            Application["SoNguoiDangHoatDong"] = (int)Application["SoNguoiDangHoatDong"] + 1;
            Application.UnLock();
        }
        protected void Session_End()
        {
            Application.Lock();
            Application["SoNguoiDangHoatDong"] = (int)Application["SoNguoiDangHoatDong"] - 1;
            Application.UnLock();
        }
    }
}
