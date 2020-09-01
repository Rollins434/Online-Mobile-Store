using MobileStore.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        dbMobileStoreEntities context = new dbMobileStoreEntities();
        
        [HttpGet]
        public ActionResult Register()
        {
            
            return View();
      
            
        }
        public ActionResult Land()
        {
            return View();
        }
        public ActionResult Register(User user)
        {
            if(context.Users.Any(e=>e.Email == user.Email))
            {
                ViewBag.Message = "User already Exists";
                return View("Register",user);
            }
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
           
            ModelState.Clear();
            ViewBag.Success = "User registered";
            return View("Register", new User());
        }

        public ActionResult LogOut()
        {
            Session.Remove("user");
            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Login(User user)
        {
            var res = context.Users.Where(e => e.Email == user.Email && e.Password == user.Password).FirstOrDefault();
            if (res != null)
            {

                Session["user"] = res.FirstName.ToString().ToUpper();
                //HttpCookie kt = new HttpCookie("name", res.FirstName);
                //Response.Cookies.Add(kt);
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }
        
    }
}