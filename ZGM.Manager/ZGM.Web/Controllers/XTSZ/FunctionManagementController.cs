using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.FunItemBLLs;
using Common.CommonModel;
using ZGM.Model.CustomModels;
using System.Configuration;
using ZGM.Web.Process.ImageUpload;

namespace ZGM.Web.Controllers.XTSZ
{
    public class FunctionManagementController : Controller
    {
        /// <summary>
        /// 获取功能项主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewData["ParentFunctionID"] = "1";
            return View();
        }

        /// <summary>
        ///获取功能项管理树 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFunctionManageTree()
        {
            List<TreeModel> treeModels = FunctionBLL.GetTreeNodes();

            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取功能项数据并分页
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFunctions(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            //父类类型标识
            string strParentID = this.Request.QueryString["ParentFunctionID"];

            decimal? parentID = null;

            if (!string.IsNullOrWhiteSpace(strParentID))
            {
                parentID = decimal.Parse(strParentID);
            }

            IQueryable<SYS_FUNCTIONS> functions = FunctionBLL.GetFunctionListByParentID(parentID);

            if (functions.Count() == 0)
            {
                functions = FunctionBLL.GetAllFunctionList().Where(a => a.FUNCTIONID == parentID);
            }
            var list = functions
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    FunctionID = t.FUNCTIONID,
                    Name = t.NAME,
                    Code = t.CODE,
                    URL = t.URL,
                    ICONPATH = t.ICONPATH,
                    StatusID = t.STATUSID,
                    SeqNo = t.SEQNUM,
                    UNITID = SessionManager.User.UnitID,

                });


            return Json(new
            {
                sEcho = secho,
                iTotalRecords = functions.Count(),
                iTotalDisplayRecords = functions.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 提交增加功能项
        /// </summary>
        [HttpPost]
        public ActionResult CommitAddFunction(SYS_FUNCTIONS Function)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string parentFunctionID = Request["ParentFunctionID"];
            
            SYS_FUNCTIONS function = new SYS_FUNCTIONS
            {
                FUNCTIONID = FunctionBLL.GetNewFunctionID(),
                NAME = Function.NAME,
                CODE = Function.CODE,
                PARENTID = decimal.Parse(parentFunctionID),
                URL = Function.URL,
                STATUSID = 1,
                ICONPATH = Function.ICONPATH,
                SEQNUM = Function.SEQNUM
            };
            //设置 Path
            if (parentFunctionID == null)
            {
                function.PATH = "\\" + function.FUNCTIONID + "\\";
            }
            else
            {
                function.PATH = FunctionBLL.GetFunctionByID(decimal.Parse(parentFunctionID)).PATH
                    + function.FUNCTIONID + "\\";
            }

            FunctionBLL.AddFunction(function);

            return RedirectToAction("Index" );
        }

        /// <summary>
        /// 增加功能项展示
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        public ActionResult AddFunction()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewBag.ParentFunctionID = Request.QueryString["ParentFunctionID"];

            List<SelectListItem> functionStatusList = new List<SelectListItem>();

            functionStatusList.Add(new SelectListItem
            {
                Text = "已删除",
                Value = "0"
            });

            functionStatusList.Add(new SelectListItem
            {
                Text = "正常",
                Value = "1",
                Selected = true
            });

            functionStatusList.Add(new SelectListItem
            {
                Text = "禁用",
                Value = "2"
            });

            ViewBag.FunctionStatusList = functionStatusList;

            return PartialView();
        }

        /// <summary>
        /// 编辑功能项展示
        /// </summary>
        public ActionResult EditFunction()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string FunctionID = Request["functionID"];

            SYS_FUNCTIONS function = FunctionBLL.GetFunctionByID(decimal.Parse(FunctionID));

            ViewBag.ICONPATH = function.ICONPATH;
            ViewBag.Status = function.STATUSID;
            ViewBag.PARENTID = function.PARENTID;
            ViewBag.FUNCTIONID = FunctionID;
            return PartialView(function);
            
        }

        /// <summary>
        /// 提交修改的表单
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditFunction(SYS_FUNCTIONS Function)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            SYS_FUNCTIONS function = new SYS_FUNCTIONS();

            function.FUNCTIONID = Function.FUNCTIONID;
            function.PARENTID = (decimal)Function.PARENTID;
            function.NAME = Function.NAME;
            function.CODE = Function.CODE;
            function.URL = Function.URL;
            function.ICONPATH = Function.ICONPATH;
            function.STATUSID =1;
            function.SEQNUM = Function.SEQNUM;

            FunctionBLL.EditFunction(function);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 删除功能项
        /// </summary>
        public ActionResult DeleteFunction()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //用户标识
            string functionid = this.Request.QueryString["functionID"];
            FunctionBLL.DeleteFunction(decimal.Parse(functionid));

            return RedirectToAction("Index");

        }
    }
}
