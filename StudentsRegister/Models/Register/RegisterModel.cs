using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentsRegister.Models.Register
{
    public class RegisterModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "You must type your e-mail")]
        [EmailAddress(ErrorMessage = "You must type proper e-mail address")]
        public string Email { get; set; }

        [DisplayName("Firstname")]
        [Required(ErrorMessage = "You must type your firstname")]
        public string FirstName { get; set; }

        [DisplayName("Lastname")]
        [Required(ErrorMessage = "You must type your lastname")]
        public string LastName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You must type in your password")]
        public string Password { get; set; }
    }
}