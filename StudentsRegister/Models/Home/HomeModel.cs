using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;

namespace StudentsRegister.Models.Home
{
    public class HomeModel
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public int? AccountType { get; set; }

        public List<GroupedMarksModel> GroupedMarks { get; set; }
        public static HomeModel GetLoggedInUser()
        {
            return JsonConvert.DeserializeObject<HomeModel>(HttpContext.Current.Request.Cookies["user"].Value);
        }
    }
}