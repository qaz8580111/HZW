using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.GCGLBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.ZDGC;

namespace ZGM.Web.Controllers.GCGL
{
    public class MajorProjectsController : Controller
    {
        //
        // GET: /MajorProjects/

        public ZDGCModels ZDGCmodel = new ZDGCModels();

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            GetPullBox();
            string gcid = Request["GC_ID"];
            decimal id = 0;
            decimal.TryParse(gcid, out id);

            if (id != 0)
            {
                //初始值
                GetInit(id);
                ZDGCmodel.GCGK = 3;
            }

            return View(ZDGCmodel);
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        /// <param name="GC_ID"></param>
        private void GetInit(decimal GC_ID)
        {
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 0;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 0;
            ZDGCmodel.GCGK = 0;

            if (GC_ID != 0)
            {
                ZDGCmodel.GKXXModel = BP_GCGKXXBLL.GetGCGKlist(GC_ID);//概况
                ZDGCmodel.GCJGXXModel = BP_GCJGXXBLL.GetGCJGlist(GC_ID);//竣工
                ZDGCmodel.ZBXXModel = BP_GCZBXXBLL.GetGCZBlist(GC_ID);//招标
                ZDGCmodel.GCSJXXModel = BZQWGHBLL.GetGCSJlist(GC_ID);//工程审计
                ZDGCmodel.GCNRXXModel = BP_GCNRXXBLL.GetGCNRlist(GC_ID);//工程地图
                ZDGCmodel.ListGCFJModel = BP_GCXMFJBLL.GetBP_GCXMFJByGCID(GC_ID).ToList();//工程附件
            }
        }

        /// <summary>
        /// 获取下拉框信息
        /// </summary>
        public void GetPullBox()
        {
            //获取所有的下拉列表
            List<BP_GCZD> list = BP_GCZDBLL.GetGCZDSourceList().ToList();

            //工程类型
            ViewBag.GCLX_TYPE = GetSelectItem(list, "GCLX_TYPE");

            //工程性质
            ViewBag.GCXZ_TYPE = GetSelectItem(list, "GCXZ_TYPE");

            //限额标准
            ViewBag.XEBZ = GetSelectItem(list, "XEBZ_TYPE");

            //招标方式
            ViewBag.ZBFS_ID = GetSelectItem(list, "ZBFS_TYPE");

            //维护类型
            ViewBag.WHLX_TYPE = GetSelectItem(list, "WHLE_TYPE");

            //质量要求
            ViewBag.ZLYQ_TYPE = GetSelectItem(list, "ZLYQ_TYPE");
        }

        /// <summary>
        /// 获取下拉列表赋值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public List<SelectListItem> GetSelectItem(List<BP_GCZD> list, string TypeName)
        {
            List<SelectListItem> listViewBag = list.Where(a => a.TYPEID == TypeName).
                Select(c => new SelectListItem()
                {
                    Text = c.ZDNAME,
                    Value = c.ZDID.ToString()
                }).ToList();
            return listViewBag;
        }

        /// <summary>
        /// 添加重大工程
        /// </summary>
        /// <param name="model"></param>
        public ActionResult CommitGCGK(BP_GCGKXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal num = 0;
            decimal.TryParse(model.GC_ID.ToString(), out num);

            #region 获取页面下拉框中的值

            string GCLX_TYPE = Request["GKXXModel.GCLX_TYPE"];
            string GCXZ_TYPE = Request["GKXXModel.GCXZ_TYPE"];
            string XEBZ = Request["GKXXModel.XEBZ"];
            //string GCNRLX_ID = Request["GKXXModel.GCNRLX_ID"];
            string GEOMETRY = Request["GCNRXXModel.GEOMETRY"];

            //获取文件上传文件
            var file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["GCGLZDGCFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);

            #endregion

            model.GCLX_TYPE = GCLX_TYPE;
            model.GCXZ_TYPE = GCXZ_TYPE;
            model.XEBZ = XEBZ;
            //model.GCNRLX_ID = decimal.Parse(GCNRLX_ID);


            if (model.GC_ID == 0)//增加操作
            {
                model.GC_ID = BP_GCZDBLL.GetNewGCID();
            }

            model.GCGCZT_ID = "1";
            model.SFLJSC = 0;
            model.TBR_ID = SessionManager.User.UserID.ToString();
            model.TBSJ = DateTime.Now;
            BP_GCGKXXBLL.AddGCGKXX(model);

            //添加地图坐标位置
            BP_GCNRXX gcnrxx = new BP_GCNRXX();
            gcnrxx.GC_ID = model.GC_ID;
            gcnrxx.GEOMETRY = GEOMETRY;
            BP_GCNRXXBLL.AddGCNRXX(gcnrxx);
            ZDGCmodel.GCNRXXModel = gcnrxx;

            //循环向数据库添加附件
            foreach (var item in list_file)
            {
                BP_GCXMFJ gcxmfj = new BP_GCXMFJ();
                gcxmfj.RZ_TYPE = "0";
                gcxmfj.GCGCZT_ID = 1;
                gcxmfj.FJ_NAME = item.OriginalName;
                gcxmfj.FJLJ = item.OriginalPath;
                gcxmfj.FJLX_TYPE = item.OriginalType;
                gcxmfj.GC_ID = model.GC_ID;
                gcxmfj.CJRQ = DateTime.Now;
                ZDGCmodel.ListGCFJModel.Add(gcxmfj);
            }

            BP_GCXMFJBLL.AddGCXMFJ(ZDGCmodel.ListGCFJModel);
            //初始值
            GetInit(model.GC_ID);

            if (num == 0)//增加操作
                ZDGCmodel.GCGK = 1;//增加
            else
                ZDGCmodel.GCGK = 2;//修改
            GetPullBox();

            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }
        /// <summary>
        /// 添加工程招标
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitGCZB(BP_GCZBXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID;
            string ZBFS_TYPE = Request["ZBXXModel.ZBFS_TYPE"];
            string ZLYQ = Request["ZBXXModel.ZLYQ"];

            var file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["GCGLZDGCFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
            model.GC_ID = GC_ID;
            model.ZBFS_TYPE = ZBFS_TYPE;
            model.ZLYQ = ZLYQ;
            //循环向数据库添加附件
            foreach (var item in list_file)
            {
                BP_GCXMFJ gcxmfj = new BP_GCXMFJ();
                gcxmfj.RZ_TYPE = "0";
                gcxmfj.GCGCZT_ID = 2;
                gcxmfj.FJ_NAME = item.OriginalName;
                gcxmfj.FJLJ = item.OriginalPath;
                gcxmfj.FJLX_TYPE = item.OriginalType;
                gcxmfj.GC_ID = GC_ID;
                gcxmfj.CJRQ = DateTime.Now;
                ZDGCmodel.ListGCFJModel.Add(gcxmfj);
            }

            BP_GCXMFJBLL.AddGCXMFJ(ZDGCmodel.ListGCFJModel);

            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "2");

            BP_GCZBXXBLL.AddGCZBXX(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "2";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 0;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 1;
            //初始值
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }

        /// <summary>
        /// 添加施工进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitSGJD(BP_GCSGJDXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID.Value;
            model.GCSGJD_ID = GCSGBLL.GetNewSGJDID();
            model.TBSJ = DateTime.Now;
            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "3");
            GCSGBLL.AddSGJD(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "3";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 1;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 0;
            //初始值
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }

        /// <summary>
        /// 添加施工问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitSGWT(BP_GCSGWTXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID.Value;

            model.GC_ID = GC_ID;
            model.GCSGWT_ID = GCSGBLL.GetNewSGWTID();
            model.TBSJ = DateTime.Now;
            model.TBR_ID = UserBLL.GetUserByUserID(SessionManager.User.UserID).USERNAME;

            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "3");

            GCSGBLL.AddSGWT(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "3";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 1;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 0;
            //初始值
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }

        /// <summary>
        /// 添加施工资金拨付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitGCZJBF(BP_GCZJSYQKYB model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID.Value;
            model.GC_ID = GC_ID;
            model.GC_BFID = GCSGBLL.GetNewZJBFID();
            model.TJSJ = DateTime.Now;

            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "3");

            GCSGBLL.AddBFJE(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "3";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 1;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 0;
            //初始值
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }


        /// <summary>
        /// 添加工程竣工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitGCJG(BP_GCJGXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID;
            var file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["GCGLZDGCFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);

            model.TBSJ = DateTime.Now;
            //循环向数据库添加附件
            foreach (var item in list_file)
            {
                BP_GCXMFJ gcxmfj = new BP_GCXMFJ();
                gcxmfj.RZ_TYPE = "0";
                gcxmfj.GCGCZT_ID = 4;
                gcxmfj.FJ_NAME = item.OriginalName;
                gcxmfj.FJLJ = item.OriginalPath;
                gcxmfj.FJLX_TYPE = item.OriginalType;
                gcxmfj.GC_ID = GC_ID;
                gcxmfj.CJRQ = DateTime.Now;

                ZDGCmodel.ListGCFJModel.Add(gcxmfj);
            }
            BP_GCXMFJBLL.AddGCXMFJ(ZDGCmodel.ListGCFJModel);
            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "4");

            BP_GCJGXXBLL.AddGCJGXX(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "4";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 1;
            ZDGCmodel.GCSG = 0;
            ZDGCmodel.BZQWH = 0;
            ZDGCmodel.GCZB = 0;
            //初始值
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }

        /// <summary>
        /// 添加工程审计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitGCSJ(BP_GCSJXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID.Value;
            var file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["GCGLZDGCFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
            model.GC_SJID = BZQWGHBLL.GetNewGCSJID();
            model.TBSJ = DateTime.Now;
            //循环向数据库添加附件
            foreach (var item in list_file)
            {
                BP_GCXMFJ gcxmfj = new BP_GCXMFJ();
                gcxmfj.RZ_TYPE = "0";
                gcxmfj.GCGCZT_ID = 5;
                gcxmfj.FJ_NAME = item.OriginalName;
                gcxmfj.FJLJ = item.OriginalPath;
                gcxmfj.FJLX_TYPE = item.OriginalType;
                gcxmfj.GC_ID = GC_ID;
                gcxmfj.CJRQ = DateTime.Now;
                ZDGCmodel.ListGCFJModel.Add(gcxmfj);
            }
            BP_GCXMFJBLL.AddGCXMFJ(ZDGCmodel.ListGCFJModel);

            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "5");

            BZQWGHBLL.AddGCSJXX(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "5";

            //初始值
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 0;
            ZDGCmodel.BZQWH = 1;
            ZDGCmodel.GCZB = 0;
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }


        /// <summary>
        /// 添加工程维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CommitGCWH(BP_GCWHXX model)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal GC_ID = model.GC_ID.Value;
            string WHLX_TYPE = Request["GCWHXXModel.WHLX_TYPE"];
            model.WHLX_TYPE = WHLX_TYPE;

            model.GC_ID = GC_ID;
            model.GCWH_ID = BZQWGHBLL.GetNewGCWHID();
            model.TBSJ = DateTime.Now;

            BP_GCGKXXBLL.ModifyGCGKXX(GC_ID, "5");

            BZQWGHBLL.AddGCWHXX(model);
            ZDGCmodel.GKXXModel.GCGCZT_ID = "5";
            GetInit(GC_ID);
            ZDGCmodel.GCGK = 3;
            ZDGCmodel.GCJG = 0;
            ZDGCmodel.GCSG = 0;
            ZDGCmodel.BZQWH = 1;
            ZDGCmodel.GCZB = 0;
            GetPullBox();
            return View("~/Views/MajorProjects/Index.cshtml", ZDGCmodel);
        }




        /// <summary>
        /// 获取工程施工进度列表
        /// </summary>
        public JsonResult GCSGJDtablelist(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            decimal GC_ID = 0;
            decimal.TryParse(Request["GCID"], out GC_ID);
            IEnumerable<BP_GCSGJDXX> List = GCSGBLL.GetGCSGJDLists(GC_ID);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    HBRQ = a.HBRQ.Value.ToString("yyyy-MM-dd"),
                    GCJD = a.GCJD + "%",
                    GCJDSM = a.GCJDSM,
                    TBSJ = a.TBSJ.Value.ToString("yyyy-MM-dd")
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取工程施工问题列表
        /// </summary>
        public JsonResult GCSGWTtablelist(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            decimal GC_ID = 0;
            decimal.TryParse(Request["GCID"], out GC_ID);
            IEnumerable<BP_GCSGWTXX> List = GCSGBLL.GetGCSGWTLists(GC_ID);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    FXRQ = a.FXRQ.Value.ToString("yyyy-MM-dd"),
                    SFKK = a.SFKK.ToString() == "1" ? "是" : "否",
                    KKJE = a.KKJE,
                    WTSM = a.WTSM,
                    TBR_ID = a.TBR_ID,
                    TBSJ = a.TBSJ.Value.ToString("yyyy-MM-dd")
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取工程施工资金拨付列表
        /// </summary>
        public JsonResult GCZJBFtablelist(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            decimal GC_ID = 0;
            decimal.TryParse(Request["GCID"], out GC_ID);
            IEnumerable<BP_GCZJSYQKYB> List = GCSGBLL.GetGCZJBFLists(GC_ID);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    BFRQ = a.BFRQ.Value.ToString("yyyy-MM-dd"),
                    BFZE = a.BFZE,
                    KKZE = a.KKZE,
                    TJSJ = a.TJSJ.Value.ToString("yyyy-MM-dd"),
                    BFSM = a.BFSM,
                    TBR = a.TBR
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 获取工程保质期维护列表
        /// </summary>
        public JsonResult GCBZQWHtablelist(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            decimal GC_ID = 0;
            decimal.TryParse(Request["GCID"], out GC_ID);
            IEnumerable<ZGM.BLL.GCGLBLLs.BZQWGHBLL.GCWHXXmodel> List = BZQWGHBLL.GetGCBZQWHLists(GC_ID);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    WHRQ = a.WHRQ == null ? "" : a.WHRQ.Value.ToString("yyyy-MM-dd"),
                    WHLX_TYPE = a.WHLX_TYPE,
                    WHSM = a.WHSM,
                    TBSJ = a.TBSJ == null ? "" : a.TBSJ.Value.ToString("yyyy-MM-dd")
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }



    }
}
