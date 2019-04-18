using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCHaberV2.Models;

namespace MVCHaberV2.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            if (UserName == "Admin" && Password == "123456") 
            {
                FormsAuthentication.SetAuthCookie(UserName, false);
                return RedirectToAction("Index", "AddNews");
            }
            else
            {
                ViewBag.Hata = "Kullanıcı adı veya parola hatalı";
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}