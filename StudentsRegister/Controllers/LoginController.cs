using StudentsRegister.Models.Login;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentsRegister.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel)
        {
            if(ModelState.IsValid == true)
            {
                FormsAuthentication.SetAuthCookie("dupa", true);

                return RedirectToAction("Index", "Home");
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