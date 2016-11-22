using StudentsRegister.DataContexts;
using StudentsRegister.Models.Home;
using StudentsRegister.Models.Student;
using StudentsRegister.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentsRegister.Controllers
{
    public class HomeController : Controller
    {
        int? status = null;
        string statusText = null;

        public ActionResult Index()
        {
            var user = UserModel.GetLoggedInUser();

            if (user == null)
            {
                return View();
            }
            else
            {
                if (user.AccountType == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.AccountType == 2)
                {
                    var showMarksUserModel = new ShowMarksStudentModel()
                    {
                        GroupedMarks = GetGroupedMarksStudent(user.Id)
                    };

                    return View("IndexLoggedInUser", showMarksUserModel);
                }
                else
                {
                    //return View("IndexLoggedInUser", homeModel);
                    return View();
                }
            }
        }

        public List<GroupedMarksModel> GetGroupedMarksStudent(int? userId)
        {
            using (var db = new StudentsRegisterDataContext())
            {
                return db.WWW_GetUserMarks(userId, ref status, ref statusText)
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
            }
        }
    }
}