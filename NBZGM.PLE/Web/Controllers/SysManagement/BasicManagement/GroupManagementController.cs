using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.GroupBLLs;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model;

namespace Web.Controllers.SysManagement.BasicManagement
{
    public class GroupManagementController : Controller
    {
        //
        // GET: /GroupManagement/
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/GroupManagement/";

        public ActionResult Index(string groupID)
        {
            ViewData["GroupParentID"] = "";

            if (!string.IsNullOrWhiteSpace(groupID))
            {
                ViewData["GroupParentID"] = groupID;
            }

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 获取用户组树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGroupManageTree()
        {
            string userID = this.Request.QueryString["userID"];
            List<TreeModel> treeModels = GroupBLL.GetTreeNodes(userID);

            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户组列表数据并且进行分页
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGroups(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            //父类单位标识
            string strGroupParentID = this.Request.QueryString["GroupParentID"];

            decimal? groupID = null;

            if (!string.IsNullOrWhiteSpace(strGroupParentID))
            {
                groupID = decimal.Parse(strGroupParentID);
            }

            List<GROUP> groups = GroupBLL.GetChildGroup(groupID);

            var list = groups
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    GroupID = t.GROUPID,
                    GroupName = t.GROUPNAME,
                    CreateTime = t.CREATEDATE.Value.ToString("yyyy-MM-dd HH:mm:ss")
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = groups.Count(),
                iTotalDisplayRecords = groups.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示添加用户组表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddGroup()
        {
            //父类用户组标识标识
            ViewBag.groupParentID = this.Request.QueryString["GroupParentID"];

            return PartialView(THIS_VIEW_PATH + "AddGroup.cshtml");
        }

        /// <summary>
        /// 提交添加表单
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CommitAddGroup()
        {
            string groupParentID = this.Request.Form["groupParentID"];

            decimal? groupID = null;
            GROUP _group = null;

            GROUP group = new GROUP();

            if (string.IsNullOrWhiteSpace(groupParentID))
            {
                _group = GroupBLL.GetGroupByParentID(groupID);
                group.PARENTID = decimal.Parse(_group.GROUPID.ToString());
            }
            else
            {
                group.PARENTID = decimal.Parse(groupParentID);
            }

            group.GROUPNAME = this.Request.Form["GroupName"];
            group.CREATEDATE = DateTime.Now;
            group.CREATEUSERID = SessionManager.User.UserID;

            GroupBLL.AddGroup(group);

            return RedirectToAction("Index", new { groupID = groupParentID });
        }

        /// <summary>
        /// 显示修改组表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditGroup()
        {
            string strGroupID = this.Request.QueryString["GroupID"];

            //根据单位标识获取对象用来初始化
            GROUP group = GroupBLL.GetGroupByGroupID(decimal.Parse(strGroupID));
            ViewBag.GroupID = group.GROUPID;
            ViewBag.ParentID = group.PARENTID;

            return PartialView(THIS_VIEW_PATH + "EditGroup.cshtml", group);
        }

        /// <summary>
        /// 提交修改的行政单位表单
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditGroup(GROUP group)
        {
            //获取父类UnitID
            string parentID = this.Request.Form["ParentID"];
            GroupBLL.ModifyGroup(group);

            return RedirectToAction("Index", new { groupID = parentID });
        }

        /// <summary>
        /// 验证该组是否可以删除
        /// </summary>
        /// <returns></returns>
        public string ValidateGroupDelete()
        {
            //自身用户组标识
            string groupID = this.Request.QueryString["GroupID"];
            string count = GroupBLL.GetChildGroupCount(decimal.Parse(groupID));

            return count;

        }

        /// <summary>
        /// 删除组织
        /// </summary>
        public ActionResult DeleteGroup()
        {
            string groupID = this.Request.QueryString["GroupID"];
            string groupParentID = this.Request.QueryString["GroupParentID"];
            GroupBLL.DeleteGroup(decimal.Parse(groupID));

            return RedirectToAction("Index", new { groupID = groupParentID });

        }
    }
}
