using StudentsRegister.Models.Login;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(LoginModel loginModel)
        {
            return View();
        }
    }
}