using Newtonsoft.Json;
using StudentsRegister.DataContexts;
using StudentsRegister.Models;
using StudentsRegister.Models.Home;
using StudentsRegister.Models.Login;
using StudentsRegister.Utilities;
using System;
using System.Net;
using System.Web;
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
                string password = null;
                int? accountType = null;
                int? id = null;

                try
                {
                    Utility.GetHashedPassword(loginModel.Email, salt, ref password, loginModel.Password, ref status, ref statusText);

                    if(status == 0)
                    {
                        using (var db = new StudentsRegisterDataContext())
                        {
                            db.WWW_LoginUser(ref id, loginModel.Email, password, ref accountType, ref status, ref statusText);
                        }

                        if (status == 0)
                        {
                            FormsAuthentication.SetAuthCookie("login", true);

                            HomeModel homeModel = new HomeModel()
                            {
                                Id = id,
                                Email = loginModel.Email,
                                AccountType = accountType
                            };

                            HttpCookie loginCookie = new HttpCookie("user", JsonConvert.SerializeObject(homeModel));
                            loginCookie.Expires = DateTime.Now.AddDays(1d);

                            Response.Cookies.Add(loginCookie);

                            return new HttpStatusCodeResult(HttpStatusCode.OK);
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
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
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1d);

            return RedirectToAction("Index", "Home");
        }
    }
}