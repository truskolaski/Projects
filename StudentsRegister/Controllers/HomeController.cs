using System.Security.Cryptography;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}