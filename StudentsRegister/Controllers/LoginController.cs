using StudentsRegister.DataContexts;
using StudentsRegister.Models.Login;
using StudentsRegister.Utilities;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentsRegister.Controllers
{
    public class LoginController : Controller
    {
        int? status = 0;
        string statusText = null;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel)
        {
            if (ModelState.IsValid == true)
            {
                string salt = null;
                string password;

                try
                {
                    using (var db = new StudentsRegisterDataContext())
                    {
                        db.WWW_GetSalt(loginModel.Email, ref salt, ref status, ref statusText);

                        if(status == 0)
                        {
                            Utility.GetHashedPassword(salt, out password, loginModel.Password);

                            db.WWW_LoginUser(loginModel.Email, password, ref status, ref statusText);

                            if(status == 0)
                            {
                                FormsAuthentication.SetAuthCookie("login", true);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home"); // ERROR
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home"); // ERROR
                        }
                    }

                    return RedirectToAction("Index", "Home"); // OK
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}