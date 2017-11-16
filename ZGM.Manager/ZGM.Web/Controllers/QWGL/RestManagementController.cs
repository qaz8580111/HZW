using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.QWGLBLLs;
using ZGM.Common.Enums;
using ZGM.Model;
using Newtonsoft.Json;
using ZGM.Model.ViewModels;
using Common;


namespace ZGM.Web.Controllers.QWGL
{
    public class RestManagementController : Controller
    {
        //
        // GET: /RestManagement/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 区域管理数据查询
        /// </summary>
        /// <returns></returns>
        public JsonResult RestManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string areaname = Request["SerrchRESTNAME"];
            List<QWGL_RESTPOINTS> list = new List<QWGL_RESTPOINTS>();
            try
            {
                list = RestAreaBLL.GetSearchRestPoint(areaname);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的评价列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new QWGL_RESTPOINTS
                {
                    RESTID = t.RESTID,
                    RESTNAME = t.RESTNAME,
                    RESTDESCRIPTION = t.RESTDESCRIPTION
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
            string RESTNAME = Request["RESTNAME"];
            string RESTDESCRIPTION = Request["RESTDESCRIPTION"];
            string geometry = Request["Geometry"];
            string areatypeid = Request["AreaTypeID"];
            string result = "";

            //区域实例数据
            QWGL_RESTPOINTS model = new QWGL_RESTPOINTS();
            if (!string.IsNullOrEmpty(areatypeid) && !string.IsNullOrEmpty(RESTNAME) &&
                !string.IsNullOrEmpty(RESTDESCRIPTION) && !string.IsNullOrEmpty(geometry))
            {

                model.RESTNAME = RESTNAME;
                model.RESTDESCRIPTION = RESTDESCRIPTION;
                model.GEOMETRY = geometry;
                model.RESTOWNERTYPE = decimal.Parse(areatypeid);
                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATE = (decimal)StatusEnum.Normal;
                //插入数据
                try
                {
                    RestAreaBLL.AddRestPoint(model);
                    result = "添加成功";
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                    result = "添加失败";
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
            decimal RESTID = 0;
            decimal.TryParse(Request["RESTID"], out RESTID);
            QWGL_RESTPOINTS model = new QWGL_RESTPOINTS();
            try
            {
                model = RestAreaBLL.GetRESTByID(RESTID);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (model != null)
            {
                VMRestModel VMmodel = new VMRestModel();
                VMmodel.RESTNAME = model.RESTNAME;
                VMmodel.RESTDESCRIPTION = model.RESTDESCRIPTION;
                VMmodel.RESTOWNERTYPE = model.RESTOWNERTYPE == null ? "0" : model.RESTOWNERTYPE.Value.ToString();
                VMmodel.GEOMETRY = model.GEOMETRY;

                return Json(new
                  {
                      arealist = VMmodel
                  }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存修改区域
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitEditArea()
        {
            //接收添加数据
            string RESTNAME = Request["RESTNAME"];
            string RESTDESCRIPTION = Request["RESTDESCRIPTION"];
            string geometry = Request["Geometry"];
            string areatypeid = Request["AreaTypeID"];
            string RESTID = Request["RESTID"];
            string result = "";
            if (string.IsNullOrEmpty(RESTID))
            {
                return Content("修改失败");
            }

            //区域实例数据
            QWGL_RESTPOINTS model = RestAreaBLL.GetRESTByID(decimal.Parse(RESTID));
            if (!string.IsNullOrEmpty(areatypeid) && !string.IsNullOrEmpty(RESTNAME) &&
                 !string.IsNullOrEmpty(RESTDESCRIPTION) && !string.IsNullOrEmpty(geometry))
            {
                model.RESTNAME = RESTNAME;
                model.RESTDESCRIPTION = RESTDESCRIPTION;
                model.GEOMETRY = geometry;
                model.RESTOWNERTYPE = decimal.Parse(areatypeid);
                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATE = (decimal)StatusEnum.Normal;
                //修改数据
                try
                {
                    RestAreaBLL.EditREST(model);
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
        public ContentResult DeleteREST()
        {
            //接收添加数据
            decimal RESTId = 0;
            decimal.TryParse(Request["RESTId"], out RESTId);
            string result = "";
            //删除数据
            try
            {
                RestAreaBLL.DeleteREST(RESTId);
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
