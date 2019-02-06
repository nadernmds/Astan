using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public class CurrentUser
    {
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["Admin"] != null)
                    return HttpContext.Current.Request.Cookies["Admin"].Values["U"].FromBase64();
                return string.Empty;
            }
        }

        public static string Name
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["Admin"] != null)
                    return HttpContext.Current.Request.Cookies["Admin"].Values["N"].FromBase64();
                return string.Empty;
            }
        }

        public static int School
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["Admin"] != null)
                    return HttpContext.Current.Request.Cookies["Admin"].Values["school"].ToInt();
                return -1;
            }
        }

        public static bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["Admin"] != null)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// Check This Entity is For This School Or Not
        /// </summary>
        /// <param name="schoolID"></param>
        public static void HavePermission(int schoolID)
        {
            if (schoolID != School)
                HttpContext.Current.Response.Redirect("/Error/403");
        }


    }
}