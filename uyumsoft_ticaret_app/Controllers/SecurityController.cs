using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using uyumsoft_ticaret_app.Models;

namespace uyumsoft_ticaret_app.Controllers
{
    public class SecurityController : Controller
    {
        uyumticaret2 utc = new uyumticaret2();
        // GET: Security
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserRecord userRecord)
        {
            var user = utc.UserRecords.FirstOrDefault(x => x.Username == userRecord.Username && x.Password == userRecord.Password);
            if (user != null){
                FormsAuthentication.SetAuthCookie(user.Username, false);
               if(user.RoleID == 1 || user.RoleID == 2)
               {
                    return RedirectToAction("Index", "Admin");
               }
                return RedirectToAction("Index", "Home");
            }
            else{
                ViewBag.Message = "Gecersiz kullanici adi ya da sifre!!";
                return View();
            }           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}