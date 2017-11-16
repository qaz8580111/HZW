using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.CQGLBLLs;
using ZGM.Model.CustomModels;
using ZGM.Model.ViewModels;
using Common;

namespace ZGM.Web.Controllers.CQGL
{
    public class CQGLManagementController : Controller
    {
        /// <summary>
        /// 项目列表主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal userid= SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 查看项目详情
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexLook()
        {
            decimal userid = SessionManager.User.UserID;
            decimal ProjectId = 0;
            decimal.TryParse(Request["ProjectId"], out ProjectId);
            if (ProjectId > 0)
            {
                VMProject model = CQGLBLL.EditShow(ProjectId);
                ViewBag.PROJECTNAME = model.PROJECTNAME;
                ViewBag.PROJECTUSER = model.PROJECTUSER;
                ViewBag.PROJECTAREA = model.PROJECTAREA;
                ViewBag.HOUSEHOLDS = model.HOUSEHOLDS;
                ViewBag.STARTTIME = model.StartTimeStr;
                ViewBag.ENDTIME = model.EndTimeStr;
                ViewBag.REMARKS = model.REMARKS;
                ViewBag.GEOMETRY = model.GEOMETRY;
                ViewBag.FILENAME = model.FILENAME;
                ViewBag.FILEPATH = model.FILEPATH;
            }
            return View();
        }

        /// <summary>
        /// 新建住宅拆迁
        /// </summary>
        /// <returns></returns>
        public ActionResult AddHouse()
        {
            decimal userid = SessionManager.User.UserID;
            List<CQGL_PROJECTS> list = CQGLBLL.GetSearchData(null, null, null, null,null,null);
            List<SelectListItem> ProLlist = list.OrderByDescending(t=>t.CREATETIME)
                .Select(t => new SelectListItem()
                {
                    Text = t.PROJECTNAME,
                    Value = t.PROJECTID.ToString()
                }).ToList();
            ViewBag.project = ProLlist;
            string houseid = Request["EditHouseId"];
            if (houseid != "" && houseid != null)
            {
                List<VMCQGL_TRANSITIONS> tlist = new List<VMCQGL_TRANSITIONS>();
                try
                {
                    tlist = CQGLBLL.GetTransitionsByHouseId(decimal.Parse(houseid));
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                ViewBag.TList = tlist;
            }
            return View();
        }

        /// <summary>
        /// 住宅拆迁列表
        /// </summary>
        /// <returns></returns>
        public ActionResult HouseList()
        {
            decimal userid = SessionManager.User.UserID;
            List<CQGL_PROJECTS> list = CQGLBLL.GetSearchData(null, null, null, null, null, null);
            List<SelectListItem> ProLlist = list.Select(t => new SelectListItem()
            {
                Text = t.PROJECTNAME,
                Value = t.PROJECTID.ToString()
            }).ToList();
            ViewBag.project = ProLlist;
            return View();
        }

        /// <summary>
        /// 查看企业拆迁详情
        /// </summary>
        /// <returns></returns>
        public ActionResult EnterpriseLook()
        {
            decimal userid = SessionManager.User.UserID;
            decimal EnterpriseId = 0;
            decimal.TryParse(Request["EnterpriseId"], out EnterpriseId);
            if (EnterpriseId > 0)
            {
                CQGL_ENTERPRISES model = CQGLBLL.EditEPShow(EnterpriseId);
                CQGL_FILES str = CQGLBLL.GetFilesNames(model.ENTERPRISEID, 7);
                List<VMCQGL_EPMoney> list = CQGLBLL.GetEPList(EnterpriseId);
                ViewBag.PROJECTID = model.PROJECTID;
                ViewBag.LEGALNAME = model.LEGALNAME;
                ViewBag.LEGALPHONE = model.LEGALPHONE;
                ViewBag.LANDAREA = model.LANDAREA;
                ViewBag.WARRANTAREA = model.WARRANTAREA;
                ViewBag.MEASUREMENTAREA = model.MEASUREMENTAREA;
                ViewBag.WITHOUTAREA = model.WITHOUTAREA;
                ViewBag.SIGNTIME = model.SIGNTIME == null ? "" : model.SIGNTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.EMPTYTIME = model.EMPTYTIME == null ? "" : model.EMPTYTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.SUMMONEY = model.SUMMONEY;
                ViewBag.TAX = model.TAX;
                ViewBag.FILENAME = str.FILENAME;
                ViewBag.FILEPATH = str.FILEPATH;
                ViewBag.EPMList = list;
            }

            return View();
        }

        /// <summary>
        /// 新建企业拆迁
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCompany()
        {
            decimal userid = SessionManager.User.UserID;
            List<CQGL_PROJECTS> list = CQGLBLL.GetSearchData(null, null, null, null,null,null);
            List<SelectListItem> ProLlist = list
                .Select(t => new SelectListItem()
                {
                    Text = t.PROJECTNAME,
                    Value = t.PROJECTID.ToString()
                }).ToList();
            ViewBag.project = ProLlist;

            return View();
        }

        /// <summary>
        /// 企业拆迁列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyList()
        {
            decimal userid = SessionManager.User.UserID;
            List<CQGL_PROJECTS> list = CQGLBLL.GetSearchData(null, null, null, null, null, null);
            List<SelectListItem> ProLlist = list.OrderByDescending(t=>t.CREATETIME)
                .Select(t => new SelectListItem()
                {
                    Text = t.PROJECTNAME,
                    Value = t.PROJECTID.ToString()
                }).ToList();
            ViewBag.project = ProLlist;
            return View();
        }

        /// <summary>
        /// 拆迁项目查询
        /// </summary>
        /// <returns></returns>
        public JsonResult CQGLManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string ProjectName = Request["ProjectName"].Trim();
            string ProjectUser = Request["ProjectUser"].Trim();
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string EStartTime = Request["EStartTime"];
            string EEndTime = Request["EEndTime"];
            List<CQGL_PROJECTS> list = new List<CQGL_PROJECTS>();

            try
            {
                list = CQGLBLL.GetSearchData(ProjectName, ProjectUser, StartTime, EndTime, EStartTime, EEndTime);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    ProjectID = t.PROJECTID,
                    ProjectName = t.PROJECTNAME,
                    ProjectUser = t.PROJECTUSER,
                    HouseHolds = t.HOUSEHOLDS,
                    ProjectArea = t.PROJECTAREA,
                    StartTime = t.STARTTIME == null ? "":t.STARTTIME.Value.ToString("yyyy-MM-dd"),
                    EndTime = t.ENDTIME == null ?"":t.ENDTIME.Value.ToString("yyyy-MM-dd")
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
        /// 住宅拆迁列表查询
        /// </summary>
        /// <returns></returns>
        public JsonResult CQGLHouse_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string ProjectName = Request["ProjectName"].Trim();
            string ProjectUser = Request["ProjectUser"].Trim();
            string HouseHolder = Request["HouseHolder"].Trim();
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            decimal HouseStatus = 0;
            decimal.TryParse(Request["HouseStatus"], out HouseStatus);
            IQueryable<VMCQGL> list = null;

            try
            {
                list = CQGLBLL.GetSearchHouseData(ProjectName, ProjectUser, HouseHolder, STime, ETime, HouseStatus);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            var data = list.OrderByDescending(t => t.HouseId).Skip((int)iDisplayStart).Take((int)iDisplayLength)
                        .Select(t => new
                        {
                            HouseID = t.HouseId,
                            ProjectName = t.PROJECTNAME,
                            ProjectUser = t.PROJECTUSER,
                            HouseHolder = t.HouseHolder,
                            StartTime = t.STARTTIME,
                            EndTime = t.ENDTIME,    // t.ENDTIME, 
                            SignTime = t.SignTime,
                            StatusId = t.StatusId,
                            HouseStatus = (t.StatusId == 1 ? "丈量摸底阶段" : (t.StatusId == 2 ? "签协阶段" : (t.StatusId == 3 ? "过渡阶段" : (t.StatusId == 4 ? "抽签安置阶段" : (t.StatusId==5?"结账阶段":"")))))
                        }).ToList();

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
        /// 企业项目查询
        /// </summary>
        /// <returns></returns>
        public JsonResult CQGLEnterprise_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string ProjectName = Request["ProjectName"].Trim();
            string ProjectUser = Request["ProjectUser"].Trim();
            string LegalName = Request["LegalName"].Trim();
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            List<VMCQGLEP> list = new List<VMCQGLEP>();

            try
            {
                list = CQGLBLL.GetSearchEnterpriseData(ProjectName, ProjectUser, LegalName, STime, ETime);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的列表
            var data = list.OrderByDescending(t=>t.EnterpriseId).Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    EnterpriseID = t.EnterpriseId,
                    ProjectName = t.PROJECTNAME,
                    ProjectUser = t.PROJECTUSER,
                    LegalName = t.LegalName,
                    SignTime = t.SignTime == null ? "" : t.SignTime.Value.ToString("yyyy-MM-dd"),
                    StartTime = t.STARTTIME == null ? "" : t.STARTTIME.Value.ToString("yyyy-MM-dd"),
                    EndTime = t.ENDTIME == null ? "" : t.ENDTIME.Value.ToString("yyyy-MM-dd")
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
        /// 提交或修改项目
        /// </summary>
        /// <returns></returns>
        public void Commit(CQGL_PROJECTS model)
        {
            decimal ProjectId = 0;
            decimal.TryParse(Request["hidden-projectid"], out ProjectId);
            int result = 0;
            int result_file = 0;

            //增加
            if (Request["hidden-isedit"] == "0")
            {
                model.CREATEUSER = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATE = 1;

                try
                {
                    result = CQGLBLL.AddProject(model);
                    decimal newprojectid = CQGLBLL.GetNewProjectID() - 1;
                    if (result > 0)
                        FileUpload(newprojectid, 1);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = 1;
            }
            //编辑
            if (Request["hidden-isedit"] == "1" && ProjectId > 0)
            {
                try
                {
                    result = CQGLBLL.EditProject(ProjectId, model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = FileUpload(ProjectId, 1);
                if (result_file > 0)
                    result = 1;
                else
                    result_file = 1;
            }


            if (result > 0 && result_file > 0)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            else
                Response.Write("<script>parent.AddCallBack(2)</script>");
        }

        /// <summary>
        /// 项目编辑展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditShow()
        {
            decimal ProjectId = 0;
            decimal.TryParse(Request["ProjectId"], out ProjectId);
            VMProject model = new VMProject();
            if (ProjectId > 0)
            {
                try
                {
                    model = CQGLBLL.EditShow(ProjectId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            return Json(new
            {
                list = model
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <returns></returns>
        public ContentResult Delete()
        {
            decimal ProjectId = 0;
            decimal.TryParse(Request["ProjectId"], out ProjectId);
            int result = 0;
            if (ProjectId > 0)
            {
                try
                {
                    result = CQGLBLL.Delete(ProjectId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            if (result > 0)
                return Content("删除成功");
            else
                return Content("删除失败");
        }

        /// <summary>
        /// 新建住宅拆迁
        /// </summary>
        /// <returns></returns>
        public void NewHouseCommit(CQGL_HOUSES hmodel,CQGL_SIGNS smodel,CQGL_TRANSITIONS tmodel,CQGL_DRAWS dmodel,CQGL_CHECKOUT cmodel)
        {
            decimal whichbtnid = 0, projectid = 0, houseid = 0,hidenprojectid = 0;
            decimal.TryParse(Request["hidden-whichbtn"],out whichbtnid);
            decimal.TryParse(Request["SelectProject"], out projectid);
            decimal.TryParse(Request["hidden-houseid"],out houseid);
            decimal.TryParse(Request["hidden-projectid"], out hidenprojectid);
            string hiddenedit = Request["hidden-isedit"];
            int result = 0;
            int result_file = 0;

            if (CQGLBLL.CheckStage(houseid) >= (whichbtnid-1))
            {
                //丈量摸底阶段
                if (whichbtnid == 1)
                {
                    //增加
                    if (hiddenedit == "0")
                    {
                        hmodel.PROJECTID = projectid;
                        hmodel.CREATEUSER = SessionManager.User.UserID;
                        hmodel.CREATETIME = DateTime.Now;
                        try
                        {
                            result = CQGLBLL.AddHouse(hmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        decimal newhouseid = CQGLBLL.GetNewCQHouseID() - 1;
                        if (result > 0)
                            FileUpload(newhouseid, 2);
                        result_file = 1;
                        houseid = newhouseid;
                    }
                    //编辑
                    if (hiddenedit == "1" && hidenprojectid > 0)
                    {
                        hmodel.PROJECTID = hidenprojectid;
                        try
                        {
                            result = CQGLBLL.EditHouse(houseid, hmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        result_file = FileUpload(houseid,2);
                        if (result_file > 0)
                            result = 1;
                        else
                            result_file = 1;
                    }
                }

                //签协阶段
                if (whichbtnid == 2)
                {
                    decimal result_stage = CQGLBLL.CheckStage(houseid);
                    //增加
                    if (hiddenedit == "0" && result_stage == 1)
                    {
                        smodel.HOUSEID = houseid;
                        smodel.CREATEUSER = SessionManager.User.UserID;
                        smodel.CREATETIME = DateTime.Now;
                        try
                        {
                            result = CQGLBLL.AddSign(smodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        if (result > 0)
                            FileUpload(houseid, 3);
                        result_file = 1;
                    }
                    //编辑
                    if (hiddenedit == "1" && hidenprojectid > 0)
                    {
                        hmodel.PROJECTID = hidenprojectid;
                        try
                        {
                            result = CQGLBLL.EditSign(houseid, smodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        result_file = FileUpload(houseid,3);
                        if (result_file > 0)
                            result = 1;
                        else
                            result_file = 1;
                    }
                }

                //过渡阶段
                if (whichbtnid == 3)
                {                    
                    decimal result_stage = CQGLBLL.CheckStage(houseid);
                    //增加
                    if (hiddenedit == "0" && result_stage == 2)
                    {                        
                        tmodel.HOUSEID = houseid;
                        tmodel.CREATTIME = DateTime.Now;
                        try
                        {
                            result = CQGLBLL.AddTransition(tmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        decimal newtsid = CQGLBLL.GetNewCQTransitionID() - 1;
                        if (result > 0)
                            FileUpload(newtsid, 4);
                        result_file = 1;
                    }
                    //编辑
                    if (hiddenedit == "1" && hidenprojectid > 0)
                    {
                        hmodel.PROJECTID = hidenprojectid;
                        try
                        {
                            result = CQGLBLL.EditTransition(houseid, tmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        result_file = FileUpload(CQGLBLL.GetTransitionInfoByHouseId(houseid).TRANSITIONID, 4);
                        if (result_file > 0)
                            result = 1;
                        else
                            result_file = 1;
                    }
                }

                //抽签安置阶段
                if (whichbtnid == 4)
                {
                    decimal result_stage = CQGLBLL.CheckStage(houseid);
                    //增加
                    if (hiddenedit == "0" && result_stage == 3)
                    {
                        dmodel.HOUSEID = houseid;
                        dmodel.CREATETIME = DateTime.Now;
                        dmodel.CREATEUSER = SessionManager.User.UserID;
                        try
                        {
                            result = CQGLBLL.AddDraw(dmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        decimal newdrawid = CQGLBLL.GetNewCQDrawID() - 1;
                        if (result > 0)
                        {
                            FileUpload(houseid, 5);
                            result_file = 1;
                            string[] places = Request["RESIDENTIAL"].Split(',');
                            string[] numbers = Request["HOUSENUMBER"].Split(',');
                            string[] areas = Request["AREA"].Split(',');

                            CQGL_DRAWHOUSE dhmodel = new CQGL_DRAWHOUSE();
                            try
                            {
                                for (int i = 0; i < int.Parse(Request["HOUSECOUNT"]); i++)
                                {
                                    dhmodel.DRAWID = newdrawid;
                                    dhmodel.RESIDENTIAL = places[i];
                                    dhmodel.HOUSENUMBER = numbers[i];
                                    dhmodel.AREA = decimal.Parse(areas[i]);
                                    dhmodel.CREATETIME = DateTime.Now;
                                    CQGLBLL.AddDrawHouse(dhmodel);
                                }
                            }
                            catch (Exception e)
                            {
                                Log4NetTools.WriteLog(e);
                            }
                        }
                    }
                    //编辑
                    if (hiddenedit == "1" && hidenprojectid > 0)
                    {
                        try
                        {
                            result = CQGLBLL.EditDraw(houseid, dmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        result_file = FileUpload(houseid,5);
                        string[] places = Request["RESIDENTIAL"].Split(',');
                        string[] numbers = Request["HOUSENUMBER"].Split(',');
                        string[] areas = Request["AREA"].Split(',');

                        CQGL_DRAWHOUSE dhmodel = new CQGL_DRAWHOUSE();
                        try
                        {
                            decimal drawid = CQGLBLL.EditDrawHouse(houseid);
                            for (int i = 0; i < int.Parse(Request["HOUSECOUNT"]); i++)
                            {
                                dhmodel.DRAWID = drawid;
                                dhmodel.RESIDENTIAL = places[i];
                                dhmodel.HOUSENUMBER = numbers[i];
                                dhmodel.AREA = decimal.Parse(areas[i]);
                                dhmodel.CREATETIME = DateTime.Now;
                                CQGLBLL.AddDrawHouse(dhmodel);
                            }
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        if (result_file > 0)
                            result = 1;
                        else
                            result_file = 1;
                    }
                }

                //结账阶段
                if (whichbtnid == 5)
                {
                    decimal result_stage = CQGLBLL.CheckStage(houseid);
                    //增加
                    if (hiddenedit == "0" && result_stage == 4)
                    {
                        cmodel.SIGNID = CQGLBLL.GetSignIdByHouseId(houseid);
                        cmodel.HOUSEID = houseid;
                        cmodel.CREATETIME = DateTime.Now;
                        cmodel.CREATEUSER = SessionManager.User.UserID;
                        try
                        {
                            result = CQGLBLL.AddCheckOut(cmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        if (result > 0)
                            FileUpload(houseid, 6);
                        result_file = 1;
                    }
                    //编辑
                    if (hiddenedit == "1" && hidenprojectid > 0)
                    {
                        try
                        {
                            result = CQGLBLL.EditCheckOut(houseid, cmodel);
                        }
                        catch (Exception e)
                        {
                            Log4NetTools.WriteLog(e);
                        }
                        result_file = FileUpload(houseid,6);
                        if (result_file > 0)
                            result = 1;
                        else
                            result_file = 1;
                    }
                }
                //是否保存成功
                if (result_file > 0)
                    Response.Write("<script>parent.AddCallBack(1," + (whichbtnid + 1) + "," + houseid + "," + hiddenedit + ",99)</script>");
                else
                    Response.Write("<script>parent.AddCallBack(2," + (whichbtnid + 1) + "," + houseid + "," + 1 + ",99)</script>");
            }
            else
                Response.Write("<script>parent.AddCallBack(3," + (whichbtnid + 1) + "," + houseid + "," + 1 + "," + CQGLBLL.CheckStage(houseid) + ")</script>");
        }

        /// <summary>
        /// 新增过渡费
        /// </summary>
        /// <returns></returns>
        public void MultiTransCommit(CQGL_TRANSITIONS model)
        {
            decimal HouseId = 0;
            decimal.TryParse(Request["hidden-houseid"], out HouseId);
            decimal result_stage = 0;
            int result = 0;
            if (HouseId > 0)
            {
                result_stage = CQGLBLL.CheckStage(HouseId);
            }
            //增加
            if (result_stage >= 2)
            {
                model.HOUSEID = HouseId;
                model.CREATTIME = DateTime.Now;
                try
                {
                    result = CQGLBLL.AddTransition(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                decimal newtsid = CQGLBLL.GetNewCQTransitionID() - 1;
                if (result > 0)
                    FileUpload(newtsid, 4);
            }

            //是否保存成功
            if (result > 0)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            else
                Response.Write("<script>parent.AddCallBack(2)</script>");
        }

        /// <summary>
        /// 拆迁住宅展示
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowCQHouse()
        {
            decimal HouseId = 0;
            decimal.TryParse(Request["HouseId"], out HouseId);
            decimal stageid = 0;
            CQGL_HOUSES hmodel = new CQGL_HOUSES();
            CQGL_FILES hstr = new CQGL_FILES();
            CQGL_SIGNS smodel = new CQGL_SIGNS();
            CQGL_FILES sstr = new CQGL_FILES();
            CQGL_TRANSITIONS tmodel = new CQGL_TRANSITIONS();
            CQGL_FILES tstr = new CQGL_FILES();
            CQGL_DRAWS dmodel = new CQGL_DRAWS();
            CQGL_FILES dstr = new CQGL_FILES();
            List<CQGL_DRAWHOUSE> dhlist = new List<CQGL_DRAWHOUSE>();
            CQGL_CHECKOUT cmodel = new CQGL_CHECKOUT();
            CQGL_FILES cstr = new CQGL_FILES();
            if (HouseId > 0)
            {
                stageid = CQGLBLL.CheckStage(HouseId);
                try
                {
                    if (stageid > 0)
                    {
                        hmodel = CQGLBLL.GeHouseInfoByHouseId(HouseId);
                        hstr = CQGLBLL.GetFilesNames(HouseId, 2);
                    }

                    if (stageid > 1)
                    {
                        smodel = CQGLBLL.GetSignInfoByHouseId(HouseId);
                        sstr = CQGLBLL.GetFilesNames(HouseId, 3);
                    }
                    if (stageid > 2)
                    {
                        tmodel = CQGLBLL.GetTransitionInfoByHouseId(HouseId);
                        tstr = CQGLBLL.GetFilesNames(tmodel.TRANSITIONID, 4);
                    }
                    if (stageid > 3)
                    {
                        dmodel = CQGLBLL.GetDrawInfoByHouseId(HouseId);
                        dstr = CQGLBLL.GetFilesNames(HouseId, 5);
                        dhlist = CQGLBLL.GetDWHouseInfoByDrawId(dmodel.DRAWID);
                    }
                    if (stageid > 4)
                    {
                        cmodel = CQGLBLL.GetCheckOutInfoByHouseId(HouseId);
                        cstr = CQGLBLL.GetFilesNames(HouseId, 6);
                    }
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return Json(new
            {
                hmodel = hmodel,
                hstr = hstr,
                smodel = smodel,
                sstr = sstr,
                tmodel = tmodel,
                tstr = tstr,
                dmodel = dmodel,
                dstr = dstr,
                dhmodel = dhlist,
                cmodel = cmodel,
                cstr = cstr,
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新建企业拆迁
        /// </summary>
        /// <returns></returns>
        public void NewCompanyCommit(CQGL_ENTERPRISES model)
        {
            decimal EnterpriseId = 0;
            decimal.TryParse(Request["hidden-enterpriseid"], out EnterpriseId);
            int result = 0;
            int result_file = 0;

            //增加
            if (Request["hidden-isedit"] == "0")
            {
                model.PROJECTID = Request["SelectProject"]==""?0:decimal.Parse(Request["SelectProject"]);
                model.CREATEUSER = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                try
                {
                    result = CQGLBLL.AddEnterprise(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                decimal newepid = CQGLBLL.GetNewEnterpriseID() - 1;
                if (result > 0)
                    FileUpload(newepid, 7);
                result_file = 1;
            }
            //编辑
            if (Request["hidden-isedit"] == "1" && EnterpriseId > 0)
            {
                try
                {
                    result = CQGLBLL.EditEnterprise(EnterpriseId, model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = FileUpload(EnterpriseId, 7);
                if (result_file > 0)
                    result = 1;
                else
                    result_file = 1;
            }

            if (result > 0 && result_file > 0)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            else
                Response.Write("<script>parent.AddCallBack(2)</script>");
        }

        /// <summary>
        /// 新增企业拆迁支付
        /// </summary>
        /// <returns></returns>
        public void MultiEMoneyCommit(CQGL_ENTERPRISEMONEYS model)
        {
            decimal EnterpriseId = 0;
            decimal.TryParse(Request["hidden-enterpriseid"], out EnterpriseId);
            int result = 0;
            if (EnterpriseId > 0)
            {
                model.ENTERPRISEID = EnterpriseId;
                model.CREATETIME = DateTime.Now;
                model.CREATEUSER = SessionManager.User.UserID;
                try
                {
                    result = CQGLBLL.AddEnterpriseMoney(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                decimal newepmid = CQGLBLL.GetNewEPMoneyID() - 1;
                if (result > 0)
                    FileUpload(newepmid, 8);
            }
            //是否保存成功
            if (result > 0)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            else
                Response.Write("<script>parent.AddCallBack(2)</script>");
        }

        /// <summary>
        /// 企业拆迁编辑展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditEPShow()
        {
            decimal EnterpriseId = 0;
            decimal.TryParse(Request["EnterpriseId"], out EnterpriseId);
            CQGL_ENTERPRISES model = new CQGL_ENTERPRISES();
            CQGL_FILES str = new CQGL_FILES();
            if (EnterpriseId > 0)
            {
                try
                {
                    model = CQGLBLL.EditEPShow(EnterpriseId);
                    str = CQGLBLL.GetFilesNames(model.ENTERPRISEID, 7);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            return Json(new
            {
                list = model,
                filename = str
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取拆迁住宅首次过渡费
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFirstTransitionInfo()
        {
            decimal HouseId = 0;
            decimal.TryParse(Request["HouseId"], out HouseId);
            CQGL_TRANSITIONS model = new CQGL_TRANSITIONS();
            if (HouseId > 0)
            {
                model = CQGLBLL.GetTransitionInfoByHouseId(HouseId);
            }
            return Json(new
            {
                list = model
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除数据库文件
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteDBFile()
        {
            string AttrachId = Request["AttrachId"];
            int result = 0;
            try
            {
                result = CQGLBLL.DeleteDBFile(AttrachId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (result > 0)
                return Content("删除成功");
            else
                return Content("删除失败");
        }

        /// <summary>
        /// 上传项目附件
        /// </summary>
        /// <returns></returns>
        public int FileUpload(decimal Id,decimal GCId)
        {
            int result_file = 0;
            HttpFileCollectionBase files = Request.Files;
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["ProjectPath"];
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, FilePath);

            if (list_file.Count != 0)
            {
                CQGL_FILES fmodel = new CQGL_FILES();
                try
                {
                    foreach (var item in list_file)
                    {
                        fmodel.SOURCEID = Id;
                        fmodel.GCID = GCId;
                        fmodel.FILENAME = item.OriginalName;
                        fmodel.FILEPATH = item.OriginalPath;
                        fmodel.CREATETIME = DateTime.Now;
                        result_file = CQGLBLL.AddProjectFile(fmodel);
                        if (result_file == 0)
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return result_file;
        }

    }


}
