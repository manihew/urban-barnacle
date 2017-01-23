using System;

namespace Company1.Department1.Project1.AuthenticationService.Models
{
    public class TokenResult
    {
        public String Token { get; set; }
        public String UserName { get; set; }
        public String Roles { get; set; }

        public static explicit operator TokenResult(Token token)
        {
            if (token == null) { return null; }

            var result = new TokenResult();
            result.Token = token.AuthToken;
            result.UserName = token.UserName;
            result.Roles = token.Roles;
            return result;
        }
    }
}