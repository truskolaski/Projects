using Newtonsoft.Json;
using StudentsRegister.DataContexts;
using StudentsRegister.Models;
using StudentsRegister.Models.Home;
using StudentsRegister.Models.Login;
using StudentsRegister.Models.User;
using StudentsRegister.Utilities;
using System;
using System.Linq;
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

                try
                {
                    Utility.GetHashedPassword(loginModel.Email, salt, ref password, loginModel.Password, ref status, ref statusText);

                    if (status == 0)
                    {
                        using (var db = new StudentsRegisterDataContext())
                        {
                            var user = db.WWW_LoginUser(loginModel.Email, password, ref status, ref statusText)
                                 .Select(x => new UserModel()
                                 {
                                     Id = x.Id,
                                     FirstName = x.FirstName,
                                     LastName = x.LastName,
                                     Email = x.Email,
                                     AccountType = x.AccountType_Id
                                 })
                            .ToList()
                            .FirstOrDefault();

                            if (status == 0)
                            {
                                if (user == null)
                                {
                                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                                }
                                else
                                {
                                    FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject(user), true);

                                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                                }
                            }
                            else
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            }
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

            return RedirectToAction("Index", "Home");
        }
    }
}