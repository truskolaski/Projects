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

        [DisplayName("Name")]
        [Required(ErrorMessage = "You must type your name")]
        public string Name { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "You must type your surname")]
        public string Surname { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You must type in your password")]
        public string Password { get; set; }
    }
}