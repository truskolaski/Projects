using StudentsRegister.DataContexts;
using StudentsRegister.Models.Register;
using StudentsRegister.Utilities;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class RegisterController : Controller
    {
        int? status = 0;
        string statusText;

        // GET: Register
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
                        db.WWW_RegisterUser(registerModel.Name, registerModel.Surname, salt, password, DateTime.Now, 2, registerModel.Email, ref status, ref statusText);

                        if(status == 0)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.OK);
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