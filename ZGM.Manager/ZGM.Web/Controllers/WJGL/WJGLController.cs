using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.WJGLBLLs;
using ZGM.BLL.ZonesBLL;
using ZGM.Model.CustomModels;
using ZGM.Model.ViewModels;
using Common;

namespace ZGM.Web.Controllers.WJGL
{
    public class WJGLController : Controller
    {
        /// <summary>
        /// 主页新建违建
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<SYS_ZONES> zones = SYS_ZONESBLL.GetzfsjZone().ToList();
            List<SelectListItem> zonesLlist = zones
                .Select(c => new SelectListItem()
                {
                    Text = c.ZONENAME,
                    Value = c.ZONEID.ToString()
                }).ToList();
            ViewBag.zones = zonesLlist;
            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 违法建筑列表
        /// </summary>
        /// <returns></returns>
        public JsonResult WJGL_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string wjunit = Request["WJUnit"].Trim();
            string wjaddress = Request["WJAddress"].Trim();
            string stime = Request["STime"];
            string etime = Request["ETime"];
            string state = Request["State"];
            List<VMWJGL> list = new List<VMWJGL>();
            try
            {
               list = WJGLBLL.GetSearchData(wjunit, wjaddress, stime, etime, state);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的列表
            list = list.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();

            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 提交新建
        /// </summary>
        /// <returns></returns>
        public void Commit(WJGL_NONBUILDINGS model)
        {
            string wjid = Request["hidden-wjid"];
            string aftercount = Request["hidden-aftercount"];
            model.X2000 = decimal.Parse(model.MAPLOACTION.Split(',')[0]);
            model.Y2000 = decimal.Parse(model.MAPLOACTION.Split(',')[1]);
            int result = 0;
            //添加图片附件
            HttpFileCollectionBase files = Request.Files;

            string OriginPath = System.Configuration.ConfigurationManager.AppSettings["WFJZOriginalPath"];
            string destnationPath = System.Configuration.ConfigurationManager.AppSettings["WFJZFilesPath"];
            string smallPath = System.Configuration.ConfigurationManager.AppSettings["WFJZSmallPath"];

            List<FileClass> list_image = new Process.ImageUpload.ImageUpload().UploadImages(files, OriginPath, destnationPath, smallPath);
            

            //增加
            if (Request["hidden-isedit"] == "0")
            {
                model.WJID = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(10000, 99999);
                model.CREATEUSER = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                try
                {
                    result = WJGLBLL.AddWJGL(model, list_image);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            //编辑
            if (Request["hidden-isedit"] == "1")
            {
                try
                {
                    result = WJGLBLL.EditWJGL(wjid, model, Request["hidden-image"], list_image);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            if(result == 1)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            if(result == 0)
                Response.Write("<script>parent.AddCallBack(0)</script>");
        }

         /// <summary>
        /// 根据路径删除图片
        /// </summary>
        /// <returns></returns>
        public void ClearFileImage()
        {
            string FilePath = Request["ClearFilePath"];
            try
            {
                WJGLBLL.DeleteImageByFilePath(FilePath);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
        }

        /// <summary>
        /// 根据违建标识获取违建信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWJGLByWJID()
        {
            VMWJGL model = new VMWJGL();
            string WJID =Request["WJID"];
            string configstr = "/GetPictureFile.ashx?PicPath=" + System.Configuration.ConfigurationManager.AppSettings["WFJZOriginalPath"].ToString();
            string bstr = "";
            string astr = "";
            if (!string.IsNullOrEmpty(WJID))
            {
                //改造前图片
                model = WJGLBLL.GetWJGLByWJID(WJID);
                if (!string.IsNullOrEmpty(model.BeforePic))
                {
                    string[] barr = model.BeforePic.Split('|');
                    for (int i = 0; i < barr.Length; i++)
                    {
                        barr[i] = configstr + barr[i];
                        if (i < barr.Length - 1)
                            bstr += barr[i] + '|';
                        else
                            bstr += barr[i];
                    }
                    model.BeforePic = bstr;
                }

                if (!string.IsNullOrEmpty(model.AfterPic))
                {
                    //改造后图片
                    string[] aarr = model.AfterPic.Split('|');
                    for (int i = 0; i < aarr.Length; i++)
                    {
                        aarr[i] = configstr + aarr[i];
                        if (i < aarr.Length - 1)
                            astr += aarr[i] + '|';
                        else
                            astr += aarr[i] ;
                    }
                    model.AfterPic = astr;
                }
            }
            return Json(new
            {
                list = model
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
