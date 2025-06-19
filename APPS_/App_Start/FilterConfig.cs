﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Apps_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public class CustomAuthorize : AuthorizeAttribute
        {
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Home/AccessDenied" }));
                }
            }
        }
    }
}
