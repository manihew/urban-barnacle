﻿using System.Web.Mvc;

namespace Company1.Department1.Project1.AuthenticationService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
