using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.BusinessLayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: AuthenticationController
        //[AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[AllowAnonymous]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticateAdmin)
                {
                    //FormsAuthentication.SetAuthCookie(u.UserName, false);
                    //return RedirectToAction("Index", "Employee");
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthentucatedUser)
                {
                    //ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    //return View("Login");
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}