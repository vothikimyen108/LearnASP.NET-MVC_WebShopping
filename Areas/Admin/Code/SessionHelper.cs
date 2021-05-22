using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myapp.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["Loginsession"] = session;
        }
        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["Loginsession"];
            if (session == null)
                return null;
            else
            {
                return session as UserSession;
            }
        }
    }
}