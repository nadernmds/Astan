using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public class CurrentPage
    {

        public static string ID
        {
          get
            {
                if(HttpContext.Current.Request.RequestContext.RouteData.Values["id"] != null)
                return HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();
                return "";
            }
        }

        public static string Action
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            }
        }

        public static string Controller
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            }
        }

       
    }
}