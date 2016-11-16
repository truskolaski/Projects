using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentsRegister.Models.Login
{
    public class LoginModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "You must type your e-mail")]
        [EmailAddress(ErrorMessage = "You must type proper e-mail address")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You must type your password")]
        public string Password { get; set; }
    }
}