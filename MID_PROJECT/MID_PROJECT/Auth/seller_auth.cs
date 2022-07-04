using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MID_PROJECT.Auth
{
    public class seller_auth : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authenticated = base.AuthorizeCore(httpContext);
            if (!authenticated)
            {
                return false;
            }

            if (httpContext.Session["role"] != null && httpContext.Session["role"].ToString().Equals("Seller"))
            {
                return true;
            }
            return false;
        }
    }
}