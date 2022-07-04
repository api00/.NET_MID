using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroEF.Auth
{
    public class Logged : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authenticated = base.AuthorizeCore(httpContext);
            if (!authenticated)
            {
                return false;
            }

            if  (httpContext.Session["role"] != null && httpContext.Session["role"].ToString().Equals("Admin"))
            {
                 return true;
            }
            //else if (httpContext.Session["role"] != null && httpContext.Session["role"].ToString().Equals("Buyer"))
            //{
            //    return true;
            //}
            //else if (httpContext.Session["role"] != null && httpContext.Session["role"].ToString().Equals("Seller"))
            //{
            //    return true;
            //}
            //else if (httpContext.Session["role"] != null && httpContext.Session["role"].ToString().Equals("Tenent"))
            //{
            //    return true;
            //}
            return false;
        }

    }
}