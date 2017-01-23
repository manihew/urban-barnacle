using System;
using System.ComponentModel.DataAnnotations;

namespace Company1.Department1.Project1.AuthenticationService.Models
{
    public class Application
    {
        [Key]
        public String AppId { get; set; }
        public String AppKey { get; set; }
    }
}