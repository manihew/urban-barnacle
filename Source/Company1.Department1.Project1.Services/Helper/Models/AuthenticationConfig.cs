using Company1.Department1.Project1.Services.Helper.Interfaces;
using System;
using System.Configuration;

namespace Company1.Department1.Project1.Services.Helper.Models
{
    /// <summary>
    /// Authentication Config class
    /// This class is used for loading the config from the client's web.config
    /// </summary>
    public class AuthenticationConfig : ConfigurationSection, IAuthenticationConfig
    {
        [ConfigurationProperty("restApiUrl",    DefaultValue = "", IsRequired = true)] public String RestApiUrl     { get { return (String)this["restApiUrl"]; }    set { this["restApiUrl"] = value; } }
        [ConfigurationProperty("loginUrl",      DefaultValue = "", IsRequired = true)] public String LoginUrl       { get { return (String)this["loginUrl"]; }      set { this["loginUrl"] = value; } }
        [ConfigurationProperty("apiKey",        DefaultValue = "", IsRequired = true)] public String ApiKey         { get { return (String)this["apiKey"]; }        set { this["apiKey"] = value; } }
        [ConfigurationProperty("appId",         DefaultValue = "", IsRequired = true)] public String AppId          { get { return (String)this["appId"]; }         set { this["appId"] = value; } }
        [ConfigurationProperty("logoutAction",  DefaultValue = "", IsRequired = true)] public String LogoutAction   { get { return (String)this["logoutAction"]; }  set { this["logoutAction"] = value; } }
        [ConfigurationProperty("homePageUrl",   DefaultValue = "", IsRequired = true)] public String HomePageUrl    { get { return (String)this["homePageUrl"]; }   set { this["homePageUrl"] = value; } }

        public String GetTokenUrl(String token)
        {
            return String.Format("{0}/api/Token/{1}", RestApiUrl, token);
        }
    }
}
