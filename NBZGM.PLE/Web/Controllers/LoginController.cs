using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Login()
        {
            string strAccount = this.Request.Form["account"];
            string strPassword = this.Request.Form["password"];

            UserInfo user = UserBLL.Login(strAccount.ToUpper(), strPassword);

            if (user != null)
            {
                SessionManager.User = user;
                return RedirectToAction("Index", "EnforceLaw");
            }
            else
            {
                user = UserBLL.Login(strAccount.ToLower(), strPassword);
                if (user != null)
                {
                    SessionManager.User = user;
                    return RedirectToAction("Index", "EnforceLaw");
                }
                else
                {
                    user = UserBLL.Login(strAccount, strPassword);
                    if (user != null)
                    {
                        SessionManager.User = user;
                        return RedirectToAction("Index", "EnforceLaw");
                    }
                }
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <returns></returns>
        public bool CheckExist()
        {
            string strAccount = this.Request.Form["account"];

            string strPassword = this.Request.Form["password"];

            bool res = UserBLL.CheckUserIsExist(strAccount.ToUpper(), strPassword);
            if (!res)
            {
                res = UserBLL.CheckUserIsExist(strAccount.ToLower(), strPassword);
            }
            if (!res)
            {
                res = UserBLL.CheckUserIsExist(strAccount, strPassword);
            }
            return res;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            SessionManager.User = null;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public bool ModifyPassword()
        {
            string ysmm = this.Request.Form["ysmm"];
            string xmm = this.Request.Form["xmm"];
            decimal userID = SessionManager.User.UserID;

            return UserBLL.ModifyUserPassword(ysmm, xmm, userID);
        }
    }
}
