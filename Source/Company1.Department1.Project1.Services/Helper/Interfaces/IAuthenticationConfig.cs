using System;

namespace Company1.Department1.Project1.Services.Helper.Interfaces
{
    public interface IAuthenticationConfig
    {
         String RestApiUrl     { get; set; }
         String LoginUrl       { get; set; }
         String ApiKey         { get; set; }
         String AppId          { get; set; }
         String LogoutAction   { get; set; }
         String HomePageUrl    { get; set; }

         String GetTokenUrl(String token);
    }
}
