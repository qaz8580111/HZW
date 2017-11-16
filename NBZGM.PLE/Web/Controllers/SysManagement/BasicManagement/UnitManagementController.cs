using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UnitTypeBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Common.Enums;
using Web.ViewModels;

namespace Web.Controllers.SysManagement.BasicManagement
{
    /// <summary>
    /// 组织管理
    /// </summary>
    public class UnitManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/UnitManagement/";

        public ActionResult Index(string UnitID)
        {
            ViewData["ParentUnitID"] = "1";

            if (!string.IsNullOrWhiteSpace(UnitID))
            {

                ViewData["ParentUnitID"] = UnitID;
            }

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 获取单位树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUnitManageTree()
        {
            List<TreeModel> treeModels = UnitBLL.GetTreeNodes();
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取单位列表数据并且进行分页
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUnits(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            //父类单位标识
            string strParentID = this.Request.QueryString["ParentUnitID"];

            decimal? unitID = null;

            if(!string.IsNullOrWhiteSpace(strParentID))
            {
                unitID = decimal.Parse(strParentID);
            }

            IQueryable<UNIT> units = UnitBLL.GetChildUnit(unitID);

            var list = units
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    UnitID = t.UNITID,
                    UnitName = t.UNITNAME,
                    UnitTypeName = t.UNITTYPE.UNITTYPENAME,
                    SeqNo = t.SEQNO
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = units.Count(),
                iTotalDisplayRecords = units.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示添加组织表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUnit()
        {
            //父类行政单位标识
            ViewBag.ParentUnitID = this.Request.QueryString["ParentUnitID"];

            //单位类型名称下拉框
            List<SelectListItem> unitTypeNameList = UnitTypeBLL.
                GetAllUnitType().ToList().Select(c => new SelectListItem
                {
                    Text = c.UNITTYPENAME,
                    Value = c.UNITTYPEID.ToString()
                }).ToList();

            unitTypeNameList.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = ""
            });

            //单位名称下拉框
            List<SelectListItem> unitNameList = UnitBLL.GetAllUnits().ToList()
                .Select(c => new SelectListItem()
            {
                Text = c.UNITNAME,
                Value = c.UNITID.ToString()
            }).ToList();

            ViewBag.unitTypeNameList = unitTypeNameList;

            ViewBag.unitNameList = unitNameList;

            return PartialView(THIS_VIEW_PATH + "AddUnit.cshtml");
        }

        /// <summary>
        /// 提交添加表单
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CommitAddUnit(VMUnit vmUnit)
        {
            UNIT unit = new UNIT
            {
                UNITID = UnitBLL.GetNewUnitID(),
                //单位名称
                UNITNAME = vmUnit.UnitName,
                //简称
                ABBREVIATION=vmUnit.UnitJC,
                //单位类型标识
                UNITTYPEID = vmUnit.UnitTypeID,
                //排序号
                SEQNO = vmUnit.SeqNo,
                //单位说明
                DESCRIPTION=vmUnit.Description,
                STATUSID=(decimal)StatusEnum.Normal,
                DWZC=vmUnit.DWZC
            };

            
            string parentUnitID = this.Request.Form["ParentUnitID"];
            //父类行政单位标识
            unit.PARENTID = decimal.Parse(parentUnitID);

            //设置单位的 Path
            if (parentUnitID == "1")
            {
                unit.PATH = @"\1\" + unit.UNITID.ToString() + "\\";
            }
            else
            {
                unit.PATH = UnitBLL.GetUnitByUnitID(decimal.Parse(parentUnitID))
                                .First().PATH + unit.UNITID.ToString() + "\\";
            }

            UnitBLL.AddUnit(unit);

            return RedirectToAction("Index", new { UnitID = parentUnitID });
        }

        /// <summary>
        /// 显示修改行政单位表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUnit()
        {
            string strUnit = this.Request.QueryString["UnitID"];

            //根据单位标识获取对象用来初始化
            UNIT unit = UnitBLL.GetUnitByUnitID(decimal.Parse(strUnit)).First();

            VMUnit vmUnit = new VMUnit()
            {
                UnitName = unit.UNITNAME,
                UnitJC=unit.ABBREVIATION,
                UnitTypeID = unit.UNITTYPEID,
                SeqNo = unit.SEQNO,
                ParentUnitID=unit.PARENTID
            };

            //单位类型下拉框
            List<SelectListItem> unitTypeNameList = UnitTypeBLL.
                GetAllUnitType().ToList().Select(c => new SelectListItem
                {
                    Text = c.UNITTYPENAME,
                    Value = c.UNITTYPEID.ToString()
                }).ToList();

            //单位名称下拉框
            List<SelectListItem> unitNameList = UnitBLL.GetAllUnits().ToList()
                .Select(c => new SelectListItem()
            {
                Text = c.UNITNAME,
                Value = c.UNITID.ToString()
            }).ToList();

            //单位类型下拉框
            ViewBag.unitTypeNameList = unitTypeNameList;
            //单位名称下拉框
            ViewBag.unitNameList = unitNameList;
            //父类标识
            ViewBag.ParentUnitID = unit.PARENTID;
            //自身标识
            ViewBag.UnitID = strUnit;

            return PartialView(THIS_VIEW_PATH + "EditUnit.cshtml", vmUnit);
        }

        /// <summary>
        /// 提交修改的行政单位表单
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditUnit(VMUnit wmUnit)
        {
            //获取父类UnitID
            string parentUnitID = this.Request.Form["ParentUnitID"];
            //自身单位标识
            string UnitID = this.Request.Form["UnitID"];

            UNIT newUnit = new UNIT
            {
                UNITID = decimal.Parse(UnitID),
                UNITNAME = wmUnit.UnitName,
                ABBREVIATION=wmUnit.UnitJC,
                UNITTYPEID = wmUnit.UnitTypeID,
                SEQNO = wmUnit.SeqNo,
                DWZC=wmUnit.DWZC
            };

            UnitBLL.ModifyUnit(newUnit);

            return RedirectToAction("Index", new { UnitID = parentUnitID });
        }

        /// <summary>
        /// 验证该组织是否可以删除
        /// </summary>
        /// <returns></returns>
        public string ValidateUnitDelete()
        {
            //自身单位标识
            string unitID = this.Request.QueryString["UnitID"];

            string count = UnitBLL.GetChildUnit(decimal.Parse(unitID)).
                Count().ToString();

            return count;

        }

        /// <summary>
        /// 删除组织
        /// </summary>
        public ActionResult DeleteUnit()
        {
            string unitID = this.Request.QueryString["UnitID"];

            string parentUnitID = this.Request.QueryString["ParentUnitID"];

            UnitBLL.DeleteUnit(decimal.Parse(unitID));

            return RedirectToAction("Index", new { UnitID = parentUnitID });

        }
    }
}
