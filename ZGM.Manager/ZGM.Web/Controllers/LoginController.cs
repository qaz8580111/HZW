using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.UserRoleBLLs;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            //System.Net.IPAddress addr;
            // 获得本机局域网IP地址 
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }

            //addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            ViewBag.IP = AddressIP.ToString().Substring(0, 3);
            return View();
        }
        [HttpPost, ActionName("Index")]
        public ActionResult Login()
        {
            string strAccount = this.Request.Form["account"];
            string strPassword = this.Request.Form["password"];
            string LinkType = Request["LinkType"];//登录后台还是管控平台  0后台 1 管控平台
            if (string.IsNullOrEmpty(LinkType))
            {
                return null;
            }

            string ffilepath = ConfigurationManager.AppSettings["UserImageFilesPath"];
            UserInfo user = UserBLL.Login(strAccount.ToUpper(), strPassword);
            Response.Cookies["account"].Value = strAccount;
            Response.Cookies["account"].Expires = DateTime.Now.AddDays(1);

            Response.Cookies["password"].Value = strPassword;
            Response.Cookies["password"].Expires = DateTime.Now.AddDays(1);

            string PageURL = System.Configuration.ConfigurationManager.AppSettings["LoginPage"];

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.UserPhoto))
                {
                    user.UserPhoto = ffilepath + user.UserPhoto;
                }
                SessionManager.User = user;
                string RoleName = string.Empty;
                string RoleId = string.Empty;
                if (user.RoleIDS != null && user.RoleIDS.Count() > 0)
                {
                    foreach (ZGM.Model.SYS_USERROLES item in user.RoleIDS)
                    {
                        RoleId += item.ROLEID.Value.ToString() + ",";
                        string rname = UserRoleBLL.GetRoleNameByRoleID(item.ROLEID.Value);//根据角色ID获取角色名称
                        RoleName += rname + ",";
                    }
                }
                Response.Cookies["RoleID"].Value = RoleId;
                Response.Cookies["RoleID"].Expires = DateTime.Now.AddDays(1);
                RoleName = System.Web.HttpUtility.UrlEncode(RoleName, System.Text.Encoding.UTF8);
                Response.Cookies["RoleName"].Value = RoleName;
                Response.Cookies["RoleName"].Expires = DateTime.Now.AddDays(1);
                if (LinkType == "0")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (RoleId.Contains(ConfigManager.ROLEID.ToString()))
                    {
                        return Redirect(PageURL);
                    }
                    else
                    {
                        return Content("<script>alert('该用户没有权限！');location.href=location.href;</script>");
                    }
                }

            }
            else
            {
                user = UserBLL.Login(strAccount.ToLower(), strPassword);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.UserPhoto))
                    {
                        user.UserPhoto = ffilepath + user.UserPhoto;
                    }
                    SessionManager.User = user;
                    string RoleName = string.Empty;
                    if (user.RoleIDS != null && user.RoleIDS.Count() > 0)
                    {
                        foreach (ZGM.Model.SYS_USERROLES item in user.RoleIDS)
                        {
                            string rname = UserRoleBLL.GetRoleNameByRoleID(item.ROLEID.Value);//根据角色ID获取角色名称
                            RoleName += rname + ",";
                        }
                    }

                    Response.Cookies["RoleName"].Value = RoleName;
                    Response.Cookies["RoleName"].Expires = DateTime.Now.AddDays(1);
                    if (LinkType == "0")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (user.RoleIDS.Count(a => a.ROLEID.Value == ConfigManager.ROLEID) > 0)
                        {
                            return Redirect(PageURL);
                        }
                        else
                        {
                            return Content("<script>alert('该用户没有权限！');location.href=location.href;</script>");
                        }
                    }
                }
                else
                {
                    user = UserBLL.Login(strAccount, strPassword);
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.UserPhoto))
                        {
                            user.UserPhoto = ffilepath + user.UserPhoto;
                        }
                        SessionManager.User = user;
                        string RoleName = string.Empty;
                        if (user.RoleIDS != null && user.RoleIDS.Count() > 0)
                        {
                            foreach (ZGM.Model.SYS_USERROLES item in user.RoleIDS)
                            {
                                string rname = UserRoleBLL.GetRoleNameByRoleID(item.ROLEID.Value);//根据角色ID获取角色名称
                                RoleName += rname + ",";
                            }
                        }

                        Response.Cookies["RoleName"].Value = RoleName;
                        Response.Cookies["RoleName"].Expires = DateTime.Now.AddDays(1);
                        if (LinkType == "0")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            if (user.RoleIDS.Count(a => a.ROLEID.Value == ConfigManager.ROLEID) > 0)
                            {
                                return Redirect(PageURL);
                            }
                            else
                            {
                                return Content("<script>alert('该用户没有权限！');location.href=location.href;</script>");
                            }
                        }
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
            Response.Cookies["account"].Value = "";
            return RedirectToAction("Index");
        }


        public int Jurisdiction()
        {
            string account = Request["account"];
            UserInfo model = UserBLL.GetUserInfo(account);
            if (model != null)
            {
                ZGM.Model.SYS_USERROLES rolemodel = model.RoleIDS.Where(a => a.ROLEID == ConfigManager.ROLEID).FirstOrDefault();
                if (rolemodel != null)
                {
                    return 1;//有前端权限
                }
                else if (model.PositionID == 1)
                {
                    return 2;//是街道人员
                }
                else
                {
                    return 3;//除去街道人员以外的人员
                }
            }
            else
            {
                return 0;
            }

        }


    }
}
