using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZGM.BLL.UserBLLs;
using ZGM.Model.CustomModels;
using ZGM.Model;
using ZGM.BLL.FunItemBLLs;

namespace ZGM.Web
{
    public class SessionManager
    {
        public static UserInfo User
        {
            get
            {
                HttpContext context = HttpContext.Current;

                UserInfo user = (UserInfo)context.Session["User"];
                HttpCookie userID = context.Request.Cookies["UserID"];
                string UserID = context.Request.Cookies["UserID"] == null ? "" : context.Request.Cookies["UserID"].Value;
                if (!string.IsNullOrEmpty(UserID))
                {
                    int LoginOut = Convert.ToInt32(ConfigManager.LoginOut);
                    userID.Expires = DateTime.Now.AddMinutes(LoginOut);
                    context.Response.AppendCookie(userID);
                }
                if (user != null)
                {
                    return user;
                }

                if (userID == null)
                    return null;

                if (string.IsNullOrWhiteSpace(userID.Value))
                    return null;

                decimal id = Convert.ToDecimal(userID.Value);

                user = UserBLL.GetUserInfoByUserID(id);

                context.Session["User"] = user;

                return user;
            }
            set
            {
                try
                {
                    HttpContext context = HttpContext.Current;

                    HttpCookie userID = null;

                    if (value == null)
                    {
                        userID = context.Request.Cookies["UserID"];
                        userID.Expires = DateTime.MinValue;
                        userID.Value = null;
                    }
                    else
                    {
                        userID = new HttpCookie("UserID");
                        int LoginOut = Convert.ToInt32(ConfigManager.LoginOut);
                        userID.Expires = DateTime.Now.AddMinutes(LoginOut);
                        // userID.Expires = DateTime.MaxValue;
                        userID.Value = value.UserID.ToString();
                    }

                    context.Response.AppendCookie(userID);


                    context.Session["User"] = value;

                    HttpContext.Current.Session["User"] = value;
                }
                catch (Exception e)
                {
                    DateTime errorDateTime = DateTime.Now;
                    string errorPath = @"C:\SessionManager\ErrorLogs\";

                    if (!Directory.Exists(errorPath))
                    {
                        Directory.CreateDirectory(errorPath);
                        StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);
                        errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
                        errorSW.WriteLine("ExceptionMessage:" + e.Message);
                        errorSW.WriteLine("ExceptionSource:" + e.Source);

                        if (e.InnerException != null)
                        {
                            errorSW.WriteLine("InnerExceptionMessage:" + e.InnerException.Message);
                            errorSW.WriteLine("InnerExceptionSource:" + e.InnerException.Source);

                        }

                        errorSW.Close();
                    }
                }
            }
        }
    }
}