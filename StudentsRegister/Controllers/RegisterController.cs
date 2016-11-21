using Newtonsoft.Json;
using StudentsRegister.DataContexts;
using StudentsRegister.Models.Home;
using StudentsRegister.Models.Register;
using StudentsRegister.Models.User;
using StudentsRegister.Utilities;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentsRegister.Controllers
{
    public class RegisterController : Controller
    {
        int? status = 0;
        string statusText;

        public ActionResult RegisterUser(RegisterModel registerModel)
        {
            if (ModelState.IsValid == true)
            {
                string salt;
                string password;

                Utility.SetSaltAndPassword(out salt, out password, registerModel.Password);

                using (var db = new StudentsRegisterDataContext())
                {
                    try
                    {
                        var user = db.WWW_RegisterUser(registerModel.FirstName, registerModel.LastName, salt, password, DateTime.Now, 2, registerModel.Email, ref status, ref statusText)
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

                        if(status == 0)
                        {
                            if(user == null)
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
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}