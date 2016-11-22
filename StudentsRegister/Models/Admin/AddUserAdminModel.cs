using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentsRegister.Models.Admin
{
    public class AddUserAdminModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "You must type in student e-mail")]
        [EmailAddress(ErrorMessage = "You must type in proper student e-mail address")]
        public string Email { get; set; }

        [DisplayName("Firstname")]
        [Required(ErrorMessage = "You must type in student firstname")]
        public string FirstName { get; set; }

        [DisplayName("Lastname")]
        [Required(ErrorMessage = "You must type in student lastname")]
        public string LastName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You must type in student password")]
        public string Password { get; set; }
    }
}