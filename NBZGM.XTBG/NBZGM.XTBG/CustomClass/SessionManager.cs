using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NBZGM.XTBG.CustomModels;

namespace NBZGM.XTBG.CustomClass
{
    public class SessionManager
    {
        public static UserInfo User
        {
            get
            {
                HttpContext context = HttpContext.Current;
                return (UserInfo)context.Session["User"];
            }
            set
            {
                HttpContext context = HttpContext.Current;
                context.Session["User"] = value;
            }
        }
    }
}