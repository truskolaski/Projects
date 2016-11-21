using Newtonsoft.Json;
using System.Web;

namespace StudentsRegister.Models.User
{
    public class UserModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? AccountType { get; set; }
        public static UserModel GetLoggedInUser()
        {
            return JsonConvert.DeserializeObject<UserModel>(HttpContext.Current.User.Identity.Name);
        }
    }
}