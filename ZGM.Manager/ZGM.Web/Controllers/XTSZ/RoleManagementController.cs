using Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZGM.BLL.FunItemBLLs;
using ZGM.BLL.RoleBLLs;
using ZGM.Common.Enums;
using ZGM.Model;

namespace ZGM.Web.Controllers.XTSZ
{
    public class RoleManagementController : Controller
    {
        //
        // GET: /RoleManagement/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        //获取 Json 格式的角色分页数据
        public JsonResult GetRoleJsonData(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {

            IQueryable<SYS_ROLES> roles = RoleBLL.GetAllRoles();

            var list = roles
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    ROLEID = t.ROLEID,
                    ROLENAME = t.ROLENAME,
                    DESCRIPTION = t.DESCRIPTION,
                    SEQNUM = t.SEQNUM
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = roles.Count(),
                iTotalDisplayRecords = roles.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        //跳转至添加角色页面
        public ActionResult AddRole()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        //处理添加角色请求
        [HttpPost]
        public ActionResult CommitAddRole(SYS_ROLES VMRole)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //该角色选中的功能项标识集合
            string strCheckedFunctionIDs =
                this.Request.Form["CheckedFunctionIDs"];

            //添加角色
            SYS_ROLES role = new SYS_ROLES
            {
                ROLEID = RoleBLL.GetNewRoleID(),
                ROLENAME = VMRole.ROLENAME,
                DESCRIPTION = VMRole.DESCRIPTION,
                SEQNUM = VMRole.SEQNUM,
                STATUSID = (decimal)StatusEnum.Normal
            };

            RoleBLL.AddRole(role);

            //添加角色和功能项的关系
            if (!string.IsNullOrWhiteSpace(strCheckedFunctionIDs))
            {
                string[] arryCheckedFunctionIDs =
                    strCheckedFunctionIDs.Split(',');

                List<SYS_ROLEFUNCTIONS> roleFunctions = new List<SYS_ROLEFUNCTIONS>();
                foreach (var functionID in arryCheckedFunctionIDs)
                {
                    roleFunctions.Add(new SYS_ROLEFUNCTIONS
                    {
                        ROLEID = role.ROLEID,
                        FUNCTIONID = decimal.Parse(functionID)
                    });
                }

                RoleBLL.AddRoleFunctions(role.ROLEID, roleFunctions);
            }

            return RedirectToAction("Index"); ;
        }

        //跳转至修改角色页面
        public ActionResult EditRole()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string strRoleID = this.Request.QueryString["ROLEID"];

            if (string.IsNullOrWhiteSpace(strRoleID))
            {
                return null;
            }

            decimal roleID = decimal.Parse(strRoleID);

            SYS_ROLES role = RoleBLL.GetRoleByRoleID(roleID);
            SYS_ROLES vmRole = new SYS_ROLES
            {
                ROLEID = role.ROLEID,
                ROLENAME = role.ROLENAME,
                DESCRIPTION = role.DESCRIPTION,
                SEQNUM = role.SEQNUM
            };

            return View(vmRole);
        }

        //处理修改角色请求
        [HttpPost]
        public ActionResult CommitEditRole(SYS_ROLES VMRole)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //该角色选中的功能项标识集合
            string strCheckedFunctionIDs =
                this.Request.Form["CheckedFunctionIDs"];

            //修改角色
            SYS_ROLES role = new SYS_ROLES
            {
                ROLEID = VMRole.ROLEID,
                ROLENAME = VMRole.ROLENAME,
                DESCRIPTION = VMRole.DESCRIPTION,
                SEQNUM = VMRole.SEQNUM
            };

            RoleBLL.EditRole(role);

            //添加角色和功能项的关系
            if (!string.IsNullOrWhiteSpace(strCheckedFunctionIDs))
            {
                string[] arryCheckedFunctionIDs =
                    strCheckedFunctionIDs.Split(',');

                List<SYS_ROLEFUNCTIONS> roleFunctions = new List<SYS_ROLEFUNCTIONS>();
                foreach (var functionID in arryCheckedFunctionIDs)
                {
                    if (string.IsNullOrWhiteSpace(functionID))
                    {
                        continue;
                    }

                    roleFunctions.Add(new SYS_ROLEFUNCTIONS
                    {
                        ROLEID = role.ROLEID,
                        FUNCTIONID = decimal.Parse(functionID)
                    });
                }

                RoleBLL.AddRoleFunctions(role.ROLEID, roleFunctions);
            }

            return RedirectToAction("Index"); ;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        public string DeleteRole()
        {
            //用户标识
            string ROLEID = this.Request.QueryString["ROLEID"];
            decimal roleid;
            decimal.TryParse(ROLEID, out roleid);
            RoleBLL.DeleteRole(roleid);
            return "删除成功!";
        }

        /// <summary>
        /// 获取功能项树
        /// </summary>
        /// <returns></returns>
        public string GetFunctionTreeData()
        {
            string strRole = this.Request.QueryString["RoleID"];

            decimal? roleID = null;

            if (!string.IsNullOrWhiteSpace(strRole))
            {
                roleID = decimal.Parse(strRole);
            }

            List<FunctionTreeModel> funTreeModels =
               FunctionBLL.GetTotalFunctionsByRoleID(roleID);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(funTreeModels);

            //return jsonData.Replace("Checked", "checked");
            return jsonData;
        }

        [HttpPost]
        public bool CommitSaveRoleFunctions()
        {
            string strRoleID = this.Request.Form["RoleID"];
            string strFunctionIDs = this.Request.Form["FunctionIDs"];

            if (string.IsNullOrWhiteSpace(strRoleID))
                return false;

            decimal roleID = decimal.Parse(strRoleID);

            string[] arryFunctionIDs =
                  strFunctionIDs.Split(',');

            List<SYS_ROLEFUNCTIONS> roleFunctions = new List<SYS_ROLEFUNCTIONS>();

            foreach (var functionID in arryFunctionIDs)
            {
                if (string.IsNullOrWhiteSpace(functionID))
                {
                    continue;
                }

                roleFunctions.Add(new SYS_ROLEFUNCTIONS
                {
                    ROLEID = roleID,
                    FUNCTIONID = decimal.Parse(functionID)
                });
            }

            RoleBLL.AddRoleFunctions(roleID, roleFunctions);
            return true;
        }
    }
}
