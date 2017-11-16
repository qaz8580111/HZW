using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Common;
using Taizhou.PLE.BLL.FunItemBLL;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model;
using Web.ViewModels;

namespace Web.Controllers.SysManagement.BasicManagement
{
    /// <summary>
    /// 功能项管理
    /// </summary>
    public class FunItemManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/FunItemManagement/";

        public ActionResult Index()
        {
            ViewData["ParentFunctionID"] = Request.QueryString["ParentFunctionID"];
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        [HttpGet]
        public JsonResult GetFunctionTree()
        {
            List<TreeModel> functionTree = FunctionBLL.GetTreeNodes();
            return Json(functionTree, JsonRequestBehavior.AllowGet);
        }

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

            IQueryable<FUNCTION> functions = FunctionBLL.GetFunctionList(parentID);

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
                        SeqNo = t.SEQNO,
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

        public JsonResult GetFunction()
        {
            string strFunctionID = Request.QueryString["FunctionID"];
            decimal functionID = Convert.ToDecimal(strFunctionID);

            FUNCTION function = FunctionBLL.GetFunctionByID(functionID);

            var customFunction = new
            {
                FUNCTIONID = function.FUNCTIONID,
                PARENTID = function.PARENTID,
                PATH = function.PATH,
                URL = function.URL,
                STATUSID = function.STATUSID,
                ICONPATH = function.ICONPATH,
                SEQNO = function.SEQNO,
                NAME = function.NAME
            };

            return Json(customFunction, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CommitEditFunction(VMFuntion vmFunction)
        {
            string parentFunctionID = this.Request.Form["ParentID"];

            string FunctionID = this.Request.Form["FunctionID"];

            FUNCTION function = new FUNCTION
            {
                FUNCTIONID = vmFunction.FunctionID,
                NAME = vmFunction.Name,
                CODE = vmFunction.Code,
                PARENTID = vmFunction.ParentID,
                URL = vmFunction.URL,
                STATUSID = vmFunction.StatusID,
                ICONPATH = vmFunction.IconPath,
                SEQNO = vmFunction.SeqNO
            };

            FunctionBLL.EditFunction(function);

            return RedirectToAction("Index", new { parentFunctionID = parentFunctionID });
        }

        public ActionResult AddFunction()
        {
            string strParentFunctionID = Request.QueryString["ParentFunctionID"];

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
            ViewData["ParentID"] = strParentFunctionID;

            return PartialView(THIS_VIEW_PATH + "AddFunction.cshtml");
        }

        public ActionResult EditFunction()
        {
            string strFunctionID = this.Request.QueryString["functionID"];

            decimal FunctionID = Convert.ToDecimal(strFunctionID);
            FUNCTION function = FunctionBLL.GetFunctionByID(FunctionID);

            VMFuntion vm = new VMFuntion();
            vm.FunctionID = function.FUNCTIONID;
            vm.Name = function.NAME;
            vm.Code = function.CODE;
            vm.ParentID = function.PARENTID;
            vm.URL = function.URL;
            vm.StatusID = function.STATUSID;
            vm.IconPath = function.ICONPATH;
            vm.SeqNO = function.SEQNO;

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
            ViewBag.a="display:none";
            return PartialView(THIS_VIEW_PATH + "EditFunction.cshtml", vm);
        }

        public ActionResult CommitAddFunction(VMFuntion vmFunction)
        {
            FUNCTION newFunction = new FUNCTION
            {
                FUNCTIONID = FunctionBLL.GetNewFunctionID(),
                NAME = vmFunction.Name,
                CODE = vmFunction.Code,
                PARENTID = vmFunction.ParentID,
                URL = vmFunction.URL,
                STATUSID = vmFunction.StatusID,
                ICONPATH = vmFunction.IconPath,
                SEQNO = vmFunction.SeqNO
            };

            //设置 Path
            if (vmFunction.ParentID == null)
            {
                newFunction.PATH = "\\" + newFunction.FUNCTIONID + "\\";
            }
            else
            {
                newFunction.PATH = FunctionBLL.GetFunctionByID(vmFunction.ParentID.Value).PATH
                    + newFunction.FUNCTIONID + "\\";
            }
            FunctionBLL.AddFunction(newFunction);
            return RedirectToAction("Index", new { parentFunctionID = vmFunction.ParentID });
        }

        public ActionResult CreateFunction()
        {
            string strFunctionID = this.Request.QueryString["functionID"];

            decimal FunctionID = Convert.ToDecimal(strFunctionID);
            FUNCTION function = FunctionBLL.GetFunctionByID(FunctionID);

            VMFuntion vm = new VMFuntion();
            vm.FunctionID = function.FUNCTIONID;
            vm.Name = function.NAME;
            vm.Code = function.CODE;
            vm.ParentID = function.PARENTID;
            vm.URL = function.URL;
            vm.StatusID = function.STATUSID;
            vm.IconPath = function.ICONPATH;
            vm.SeqNO = function.SEQNO;

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
            ViewBag.a = "display:none";
            return PartialView(THIS_VIEW_PATH + "CreateFunction.cshtml", vm);
        }
    }
}
