using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.BLL.UserRoleBLLs;
using Taizhou.PLE.BLL.RoleBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.FunItemBLL;

namespace Web.Controllers.SysManagement.BasicManagement
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class PermissionManagementController : Controller
    {
        //
        // GET: /PermissionManagement/

        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/PermissionManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public JsonResult GetUsers()
        {
            List<UserTreeModel> treeModelList = new List<UserTreeModel>();

            List<UserTreeModel> usersList = UserBLL.GetAllUsers().ToList()
                .Select(t => new UserTreeModel
            {
                id = "UserID" + t.USERID.ToString(),
                name = t.USERNAME,
                open = false,
                pId = "UnitID" + t.UNITID.ToString(),
                type = "User",
                value = t.USERID.ToString()
            }).ToList();

            foreach (UserTreeModel TUM in usersList)
            {
                treeModelList.Add(TUM);
            }

            List<UserTreeModel> unitsList = UnitBLL.GetAllUnits().ToList()
                .Select(t => new UserTreeModel
            {
                id = "UnitID" + t.UNITID.ToString(),
                name = t.UNITNAME,
                open = t.PARENTID == null ? true : false,
                pId = "UnitID" + t.PARENTID.ToString(),
                type = "Unit"
            }).ToList();

            foreach (UserTreeModel TUM in unitsList)
            {
                treeModelList.Add(TUM);
            }

            return Json(treeModelList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoles(int? iDisplayStart
      , int? iDisplayLength, int? secho)
        {
            string strUserID = Request.QueryString["UserID"];

            decimal userID = Convert.ToDecimal(strUserID);

            IQueryable<UserRole> result = UserRoleBLL.GetUserRoleByUserID(userID);

            var list = result
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value);

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = result.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        public bool submitUserRole()
        {
            string strUserID = Request.QueryString["userID"];
            string strRoleIDs = Request.QueryString["userRole"];

            if (string.IsNullOrWhiteSpace(strUserID))
            {
                return false;
            }

            decimal userID = decimal.Parse(strUserID);

            string[] arryRoleIDs = strRoleIDs.Split(',');

            List<USERROLE> userRoles = new List<USERROLE>();
            foreach (var RoleID in arryRoleIDs)
            {
                if (string.IsNullOrWhiteSpace(RoleID))
                {
                    continue;
                }

                userRoles.Add(new USERROLE
                {
                    ROLEID = decimal.Parse(RoleID),
                    USERID = userID
                });
            }

            bool result = UserRoleBLL.AddUserRoles(userID, userRoles);

            return result;
        }

        public JsonResult GetUserRoleTreeData()
        {
            string strRoleIDs = Request.QueryString["roleIDs"];

            string[] arrayRoleIDs = strRoleIDs.Split(',');

            List<decimal> userRoleIDs = new List<decimal>();

            foreach (string str in arrayRoleIDs)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    continue;
                }
                userRoleIDs.Add(decimal.Parse(str));
            }

            List<FunctionTreeModel> UserRoleTreeData =
                             FunctionBLL.GetTotalFunctionsByRoleID(userRoleIDs);

            return Json(UserRoleTreeData, JsonRequestBehavior.AllowGet);
        }
    }
}
