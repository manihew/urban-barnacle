using System.ComponentModel.DataAnnotations;

namespace Company1.Department1.Project1.AuthenticationWebsite.Models
{
    public class LogonViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}