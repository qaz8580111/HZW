using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taizhou.PLE.BLL.FunItemBLL;
using Taizhou.PLE.BLL.RoleBLLs;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Web.ViewModels;

namespace Web.Controllers.SysManagement.BasicManagement
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManagementController : Controller
    {
        public const string THIS_VIEW_PATH =
            @"~/Views/SysManagement/BasicManagement/RoleManagement/";

        //角色管理首页
        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "index.cshtml");
        }

        //获取 Json 格式的角色分页数据
        public JsonResult GetRoleJsonData(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {

            IQueryable<ROLE> roles = RoleBLL.GetRoles();

            var list = roles
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    RoleID = t.ROLEID,
                    RoleName = t.ROLENAME,
                    Description = t.DESCRIPTION,
                    SeqNo = t.SEQNO
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = roles.Count(),
                iTotalDisplayRecords = roles.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

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

        //跳转至添加角色页面
        public ActionResult AddRole()
        {
            return View(THIS_VIEW_PATH + "AddRole.cshtml");
        }

        //处理添加角色请求
        [HttpPost]
        public ActionResult CommitAddRole(ViewModels.VMRole VMRole)
        {
            //该角色选中的功能项标识集合
            string strCheckedFunctionIDs =
                this.Request.Form["CheckedFunctionIDs"];

            //添加角色
            ROLE role = new ROLE
            {
                ROLEID = RoleBLL.GetNewRoleID(),
                ROLENAME = VMRole.RoleName,
                DESCRIPTION = VMRole.Description,
                SEQNO = VMRole.SeqNo,
                STATUSID = (decimal)StatusEnum.Normal
            };

            RoleBLL.AddRole(role);

            //添加角色和功能项的关系
            if (!string.IsNullOrWhiteSpace(strCheckedFunctionIDs))
            {
                string[] arryCheckedFunctionIDs =
                    strCheckedFunctionIDs.Split(',');

                List<ROLEFUNCTION> roleFunctions = new List<ROLEFUNCTION>();
                foreach (var functionID in arryCheckedFunctionIDs)
                {
                    roleFunctions.Add(new ROLEFUNCTION
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
            string strRoleID = this.Request.QueryString["RoleID"];

            if (string.IsNullOrWhiteSpace(strRoleID))
            {
                return null;
            }

            decimal roleID = decimal.Parse(strRoleID);

            ROLE role = RoleBLL.GetRoleByRoleID(roleID);
            VMRole vmRole = new VMRole
            {
                RoleID = role.ROLEID,
                RoleName = role.ROLENAME,
                Description = role.DESCRIPTION,
                SeqNo = role.SEQNO
            };

            return View(THIS_VIEW_PATH + "EditRole.cshtml", vmRole);
        }

        //处理修改角色请求
        [HttpPost]
        public ActionResult CommitEditRole(ViewModels.VMRole VMRole)
        {
            //该角色选中的功能项标识集合
            string strCheckedFunctionIDs =
                this.Request.Form["CheckedFunctionIDs"];

            //修改角色
            ROLE role = new ROLE
            {
                ROLEID = VMRole.RoleID,
                ROLENAME = VMRole.RoleName,
                DESCRIPTION = VMRole.Description,
                SEQNO = VMRole.SeqNo
            };

            RoleBLL.EditRole(role);

            //添加角色和功能项的关系
            if (!string.IsNullOrWhiteSpace(strCheckedFunctionIDs))
            {
                string[] arryCheckedFunctionIDs =
                    strCheckedFunctionIDs.Split(',');

                List<ROLEFUNCTION> roleFunctions = new List<ROLEFUNCTION>();
                foreach (var functionID in arryCheckedFunctionIDs)
                {
                    if (string.IsNullOrWhiteSpace(functionID))
                    {
                        continue;
                    }

                    roleFunctions.Add(new ROLEFUNCTION
                    {
                        ROLEID = role.ROLEID,
                        FUNCTIONID = decimal.Parse(functionID)
                    });
                }

                RoleBLL.AddRoleFunctions(role.ROLEID, roleFunctions);
            }

            return RedirectToAction("Index"); ;
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

            List<ROLEFUNCTION> roleFunctions = new List<ROLEFUNCTION>();
         
            foreach (var functionID in arryFunctionIDs)
            {
                if (string.IsNullOrWhiteSpace(functionID))
                {
                    continue;
                }

                roleFunctions.Add(new ROLEFUNCTION
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
