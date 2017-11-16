using Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.FunItemBLLs;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.UserRoleBLLs;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Controllers.XTSZ
{
    public class PermissionManagementController : Controller
    {
        //
        // GET: /PermissionManagement/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
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

            List<SYS_USERROLES> userRoles = new List<SYS_USERROLES>();
            foreach (var RoleID in arryRoleIDs)
            {
                if (string.IsNullOrWhiteSpace(RoleID))
                {
                    continue;
                }

                userRoles.Add(new SYS_USERROLES
                {
                    ROLEID = decimal.Parse(RoleID),
                    USERID = userID
                });
            }

            bool result = UserRoleBLL.AddUserRoles(userID, userRoles);

            return result;
        }

        /// <summary>
        /// 获取用户树json
        /// </summary>
        /// <returns></returns>
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

            IQueryable<UserRoleModel> result = UserRoleBLL.GetUserRoleByUserID(userID);

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
