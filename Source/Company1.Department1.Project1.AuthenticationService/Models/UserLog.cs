using System;
using System.ComponentModel.DataAnnotations;

namespace Company1.Department1.Project1.AuthenticationService.Models
{
    public class UserLog
    {
        [Key]
        public String UserName { get; set; }
        public String LatestAppId { get; set; }
    }
}