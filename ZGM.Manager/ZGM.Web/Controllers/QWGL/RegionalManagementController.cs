using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.ViewModels;
using ZGM.BLL.UserBLLs;
using ZGM.Common.Enums;
using Newtonsoft.Json;
using ZGM.Model.CustomModels;
using Common;

namespace ZGM.Web.Controllers.QWGL
{
    public class RegionalManagementController : Controller
    {
        /// <summary>
        /// 区域管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<QWGL_RESTPOINTS> list = RestAreaBLL.GetSearchArea(null);
            ViewBag.restlist = list;
            return View();
        }

        /// <summary>
        /// 区域管理数据查询
        /// </summary>
        /// <returns></returns>
        public JsonResult RegionalManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string areaname = Request["AreaName"].Trim();
            List<QWGL_AREAS> list = new List<QWGL_AREAS>();

            try
            {
                list = AreaBLL.GetSearchArea(areaname);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的评价列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new QWGL_AREAS
                {
                    AREAID = t.AREAID,
                    AREANAME = t.AREANAME,
                    AREADESCRIPTION = t.AREADESCRIPTION,
                });

            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存添加区域
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitAddArea()
        {
            //接收添加数据
            string areaname = Request["AreaName"];
            string areadescription = Request["AreaDescription"];
            string areatypeid = Request["AreaTypeID"];
            string geometry = Request["Geometry"];
            string color = Request["Color"];
            string restlist = Request["restlist"];
            string result = ""; ;

            //区域实例数据
            QWGL_AREAS model = new QWGL_AREAS();
            if (!string.IsNullOrEmpty(areatypeid) && !string.IsNullOrEmpty(areaname) &&
                !string.IsNullOrEmpty(areadescription) && !string.IsNullOrEmpty(geometry))
            {
                model.AREAID = AreaBLL.GetNewAREAID();
                model.AREANAME = areaname;
                model.AREADESCRIPTION = areadescription;
                model.GEOMETRY = geometry;
                model.COLOR = color;
                model.AREAOWNERTYPE = decimal.Parse(areatypeid);
                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATE = (decimal)StatusEnum.Normal;
                //插入数据
                try
                {
                    AreaBLL.AddAreaList(model);
                    result = "添加成功";
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                    result = "添加失败";
                }
                if (!string.IsNullOrEmpty(restlist) && result != "添加失败")
                {
                    string[] list = restlist.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        QWGL_RESTAREARS modelrs = new QWGL_RESTAREARS();
                        modelrs.AREAID = model.AREAID;
                        modelrs.RESTID = decimal.Parse(list[i]);
                        try
                        {
                            AreaBLL.AddRESTAREARS(modelrs);
                            result = "添加成功";
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                            result = "添加失败";
                        }
                    }
                }
            }
            else
            {
                result = "添加失败";
            }

            return Content(result);

        }

        /// <summary>
        /// 编辑区域展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditAreaShow()
        {
            decimal AreaId = 0;
            decimal.TryParse(Request["AreaID"],out AreaId);
            QWGL_AREAS model = AreaBLL.GetAreaByID(AreaId);
            List<ZGM.Model.CustomModels.AreaRest> list = new List<AreaRest>();
            try
            {
                list = AreaBLL.GetRTSByAreaID(AreaId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            AreaClassModel modelc = new AreaClassModel();
            modelc.AREADESCRIPTION = model.AREADESCRIPTION;
            modelc.AREAID = model.AREAID;
            modelc.AREANAME = model.AREANAME;
            modelc.AREAOWNERTYPE = model.AREAOWNERTYPE;
            modelc.CREATETIME = model.CREATETIME;
            modelc.CREATEUSERID = model.CREATEUSERID;
            modelc.GEOMETRY = model.GEOMETRY;
            modelc.COLOR = model.COLOR;
            modelc.SQENUM = model.SQENUM;
            modelc.list = list;
            //返回json
            return Json(new
            {
                modelc = modelc,

            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改区域
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitEditArea()
        {
            //接收添加数据
            decimal AreaId = 0;
            decimal.TryParse(Request["AreaId"], out AreaId);
            string areaname = Request["AreaName"];
            string areadescription = Request["AreaDescription"];
            string areatypeid = Request["AreaTypeID"];
            string geometry = Request["Geometry"];
            string color = Request["Color"];
            string restlist = Request["restlist"];
            string result = "";

            //区域实例数据
            QWGL_AREAS model = AreaBLL.GetAreaByID(AreaId);
            if (!string.IsNullOrEmpty(areatypeid) && !string.IsNullOrEmpty(areaname) &&
                !string.IsNullOrEmpty(areadescription) && !string.IsNullOrEmpty(geometry))
            {
                model.AREANAME = areaname;
                model.AREADESCRIPTION = areadescription;
                model.GEOMETRY = geometry;
                model.COLOR = color;
                model.AREAOWNERTYPE = decimal.Parse(areatypeid);
                model.CREATEUSERID = SessionManager.User.UserID;
                //修改数据
                try
                {
                    AreaBLL.EditAreaList(model);
                    //删除区域休息点关系中所有这个区域的数据。
                    AreaBLL.DeleteRestAreaByAreaid(AreaId);
                    if (!string.IsNullOrEmpty(restlist))
                    {
                        string[] list = restlist.Split(',');
                        for (int i = 0; i < list.Length; i++)
                        {
                            QWGL_RESTAREARS modelrs = new QWGL_RESTAREARS();
                            modelrs.AREAID = model.AREAID;
                            modelrs.RESTID = decimal.Parse(list[i]);
                            AreaBLL.AddRESTAREARS(modelrs);
                        }
                    }
                    result = "修改成功";
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                    result = "修改失败";
                }
            }
            return Content(result);

        }

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteArea()
        {
            //接收添加数据
            decimal AreaId = 0;
            decimal.TryParse(Request["AreaId"], out AreaId);
            string result;
            //删除数据
            try
            {
                AreaBLL.DeleteArea(AreaId);
                result = "1";
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
                result = "2";
            }
            return Content(result);
        }

    }
}
