using System.ComponentModel;

namespace StudentsRegister.Models.Login
{
    public class LoginModel
    {
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}