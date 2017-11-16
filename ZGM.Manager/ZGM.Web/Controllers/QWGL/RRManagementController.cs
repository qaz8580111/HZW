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
using Common;

namespace ZGM.Web.Controllers.QWGL
{
    public class RRManagementController : Controller
    {
        /// <summary>
        /// 签到区域管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 签到区域数据查询
        /// </summary>
        /// <returns></returns>
        public JsonResult RRManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string areaname = Request["SignInAreaName"].Trim();
            List<QWGL_SIGNINAREAS> list = new List<QWGL_SIGNINAREAS>();
            try
            {
                list = AreaBLL.GetSearchSignInArea(areaname);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的签到区域列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    AREAID = t.AREAID,
                    AREANAME = t.AREANAME.Length > 50 ? t.AREANAME.Substring(0, 50) + "..." : t.AREANAME,
                    STIME = t.SDATE == null ? "" : t.SDATE.Value.Hour.ToString().PadLeft(2, '0') + ":" + t.SDATE.Value.Minute.ToString().PadLeft(2, '0'),
                    ETIME = t.EDATE == null ? "" : t.EDATE.Value.Hour.ToString().PadLeft(2, '0') + ":" + t.EDATE.Value.Minute.ToString().PadLeft(2, '0')
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
        /// 保存添加签到区域
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitAddSignInArea()
        {
            //接收添加数据
            string areaname = Request["AreaName"];
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string geometry = Request["Geometry"];
            string result = "添加失败";

            decimal siid = AreaBLL.GetNewSignInAreaID();
            //签到区域实例
            QWGL_SIGNINAREAS sa_model = new QWGL_SIGNINAREAS();
            if (!string.IsNullOrEmpty(areaname) && !string.IsNullOrEmpty(starttime) &&
                !string.IsNullOrEmpty(endtime) && !string.IsNullOrEmpty(geometry))
            {
                sa_model.AREAID = siid;
                sa_model.AREANAME = areaname;
                sa_model.GEOMETRY = geometry;
                sa_model.AREAOWNERTYPE = 3;
                sa_model.CREATEUSERID = SessionManager.User.UserID;
                sa_model.CREATETIME = DateTime.Now;
                sa_model.STATE = (decimal)StatusEnum.Normal;
                sa_model.SDATE = DateTime.Parse(starttime);
                sa_model.EDATE = DateTime.Parse(endtime);
                //插入数据
                try
                {
                    AreaBLL.AddSignInList(sa_model);
                    result = "添加成功";
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                    result = "添加失败";
                }
            }
            return Content(result);
        }

        /// <summary>
        /// 编辑签到区域展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditSignInAreaShow()
        {
            decimal AreaId = 0;
            decimal.TryParse(Request["AreaID"], out AreaId);
            VMSignInArea model = new VMSignInArea();

            try
            {
                model = AreaBLL.GetSignInAreaView(AreaId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            return Json(new
            {
                list = model
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改签到区域
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitEditSignInArea()
        {
            //接收添加数据
            string areaid = Request["AreaId"];
            string areaname = Request["AreaName"];
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string geometry = Request["Geometry"];
            QWGL_SIGNINAREAS model = new QWGL_SIGNINAREAS();
            string result = "";

            //区域实例数据
            try
            {
                model = AreaBLL.GetSignInAreaByID(decimal.Parse(areaid));
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
                result = "修改失败";
            }
            if (!string.IsNullOrEmpty(areaname) && !string.IsNullOrEmpty(starttime) &&
                !string.IsNullOrEmpty(endtime) && !string.IsNullOrEmpty(geometry) && result != "修改失败")
            {
                model.AREANAME = areaname;
                model.GEOMETRY = geometry;
                model.CREATEUSERID = SessionManager.User.UserID;
                model.SDATE = DateTime.Parse(starttime);
                model.EDATE = DateTime.Parse(endtime);
                ////修改数据
                try
                {
                    AreaBLL.EditSignInAreaList(model);
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
        /// 删除签到区域
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteSignInArea()
        {
            //接收添加数据
            decimal AreaId = 0;
            decimal.TryParse(Request["AreaId"],out AreaId);
            string result = "";
            //删除数据
            try
            {
                AreaBLL.DeleteSignInArea(AreaId);
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
