using StudentsRegister.DataContexts;
using StudentsRegister.Models.Admin;
using StudentsRegister.Models.User;
using StudentsRegister.Utilities;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        int? status = 0;
        string statusText;
        int StudentAccountType_Id = 2;

        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Admin/AddUserAdmin.cshtml");
        }

        public ActionResult RegisterUser(AddUserAdminModel addUserAdminModel)
        {
            if (ModelState.IsValid == true)
            {
                string salt;
                string password;

                Utility.SetSaltAndPassword(out salt, out password, addUserAdminModel.Password);

                using (var db = new StudentsRegisterDataContext())
                {
                    try
                    {
                        var user = db.WWW_RegisterUser(addUserAdminModel.FirstName, addUserAdminModel.LastName, salt, password, DateTime.Now, StudentAccountType_Id, addUserAdminModel.Email, ref status, ref statusText)
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
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}