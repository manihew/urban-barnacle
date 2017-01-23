using System;
using System.Configuration;

namespace Company1.Department1.Project1.AuthenticationWebsite.Models
{
    public static class Config
    {
        public static String AppId
        {
            get { return ConfigurationManager.AppSettings["AppId"]; }
            set { ConfigurationManager.AppSettings["AppId"] = value; }
        }
        public static String ApiKey 
        { 
            get { return ConfigurationManager.AppSettings["ApiKey"]; }
            set { ConfigurationManager.AppSettings["ApiKey"] = value; }
        }
        public static String AuthenticationServiceUrl 
        { 
            get { return ConfigurationManager.AppSettings["AuthenticationServiceUrl"]; }
            set { ConfigurationManager.AppSettings["AuthenticationServiceUrl"] = value; } 
        }
    }
}