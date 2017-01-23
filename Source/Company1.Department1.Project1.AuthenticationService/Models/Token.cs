using System;
using System.ComponentModel.DataAnnotations;

namespace Company1.Department1.Project1.AuthenticationService.Models
{
    public class Token
    {
        [Key]
        public String AuthToken { get; set; }
        public String UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public String Roles { get; set; }
    }
}