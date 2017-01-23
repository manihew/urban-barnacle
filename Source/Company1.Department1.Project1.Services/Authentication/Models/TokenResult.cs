using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Company1.Department1.Project1.Services.Authentication.Models
{
    public class TokenResult
    {
        public String Token { get; set; }
        public String UserName { get; set; }
        public String Roles { get; set; }

        public static explicit operator ClaimsPrincipal(TokenResult authToken)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, authToken.UserName));

            if (authToken.Roles != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, authToken.Roles));
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }
    }
}
