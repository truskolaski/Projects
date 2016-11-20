using StudentsRegister.DataContexts;
using StudentsRegister.Models.Home;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                HomeModel loggedInUser = HomeModel.GetLoggedInUser();

                if (loggedInUser == null)
                {
                    return View();
                }
                else
                {
                    int? status = null;
                    string statusText = null;

                    using (var db = new StudentsRegisterDataContext())
                    {
                        var recordSet = db.WWW_GetUserMarks(loggedInUser.Id, ref status, ref statusText)
                            .Select(x => new MarkModel()
                            {
                                Mark = x.Mark,
                                SubjectName = x.SubjectName
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

                        loggedInUser.GroupedMarks = recordSet;
                    }

                    return View("IndexLoggedInUser", loggedInUser);
                }
            }
            else
            {
                return View();
            }
        }
    }
}