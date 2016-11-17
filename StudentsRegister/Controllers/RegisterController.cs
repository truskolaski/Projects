using StudentsRegister.DataContexts;
using StudentsRegister.Models.Register;
using StudentsRegister.Utilities;
using System;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index(RegisterModel registerModel)
        {
            if (ModelState.IsValid == true)
            {
                string salt;
                string password;

                Utility.SetSaltAndPassword(out salt, out password, registerModel);

                using (var db = new StudentsRegisterDataContext())
                {
                    try
                    {
                        D_Student student = new D_Student()
                        {
                            Email = registerModel.Email,
                            Name = registerModel.Name,
                            Surname = registerModel.Surname,
                            Password = password,
                            Salt = salt
                        };

                        db.D_Students.InsertOnSubmit(student);

                        db.SubmitChanges();
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