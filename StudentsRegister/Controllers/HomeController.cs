using StudentsRegister.DataContexts;
using StudentsRegister.Models.Home;
using StudentsRegister.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = UserModel.GetLoggedInUser();

            if (user == null)
            {
                return View();
            }
            else
            {
                int? status = null;
                string statusText = null;

                using (var db = new StudentsRegisterDataContext())
                {
                    var recordSet = db.WWW_GetUserMarks(user.Id, ref status, ref statusText)
                        .Select(x => new MarkModel()
                        {
                            Mark = x.Mark,
                            TutorName = x.FirstName,
                            TutorLastName = x.LastName,
                            SubjectName = x.SubjectName,
                            MarkDate = x.MarkDate
                        })
                        .GroupBy(x => x.SubjectName)
                        .Select(grp =>
                        new GroupedMarksModel()
                        {
                            SubjectName = grp.Key,
                            Marks = grp.ToList()
                        })
                        .OrderBy(x => x.SubjectName)
                        .ToList();

                    HomeModel homeModel = new HomeModel()
                    {
                        GroupedMarks = recordSet
                    };

                    return View("IndexLoggedInUser", homeModel);
                }
            }
        }
    }
}