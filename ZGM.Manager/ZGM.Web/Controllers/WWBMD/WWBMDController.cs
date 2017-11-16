using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.WWBMDBLLs;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Web.Process.ImageUpload;
using ZGM.Model.WWBMDModels;

namespace ZGM.Web.Controllers.WWBMD
{
    public class WWBMDController : Controller
    {
        //
        // GET: /WWBMD/

        public ActionResult Add()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取类型
            ViewBag.BMDType = WWBMDBLL.GetAllBMDType().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.TYPENAME,
                    Value = c.TYPEID.ToString()
                }).ToList();

            return View();
        }

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="Model"></param>
        public void Commit(WWBMDModel Model)
        {
            Model.BMDID = WWBMDBLL.GetNewWWBMDID();
            Model.CREATETIME = DateTime.Now;
            Model.CREATEUSER = SessionManager.User.UserID;
            #region 头像图片
            HttpFileCollectionBase files = Request.Files;
            List<FileClass> fileClass = new List<FileClass>();
            if (files.Count > 0 && files != null && files[0].ContentLength > 0)
            {
                string Ofilepath = ConfigurationManager.AppSettings["BMDOriginalPath"];
                string ffilepath = ConfigurationManager.AppSettings["BMDFilesPath"];
                string sfilepath = ConfigurationManager.AppSettings["BMDSmallPath"];
                fileClass = new ImageUpload().UploadImages(files, Ofilepath, ffilepath, sfilepath);
                //for (int i = 0; i < files.Count; i++)
                //{
                Model.FILEPATH = fileClass[0].OriginalPath;
                Model.FILENAME = fileClass[0].OriginalName;
                // }
            }
            #endregion
            WWBMDBLL.AddWWBMD(Model);
            // string Num = Request["Num"];
            if (Model.MapAddress != null)
            {
                foreach (var item in Model.MapAddress)
                {
                    BMD_USERAREA UserArea = new BMD_USERAREA();
                    UserArea.BMDID = Model.BMDID;
                    UserArea.ADDRESSNAME = item.MapAddressName;
                    UserArea.GEOMETRY = item.MapAddressID;
                    BMDUserAreaBLL.AddBMDUserArea(UserArea);
                }
            }
            //return RedirectToAction("Index");
            Response.Write("<script>window.location.href='/WWBMD/Index/?flag=1'</script>");
        }


        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 白名单列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public ActionResult BMDList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //接收查询条件
            string userbh = Request["UserBH"];
            string username = Request["UserName"];
            //string starttime = Request["StartTime"];
            //string endtime = Request["EndTime"];
            //DateTime tempDate = DateTime.Now;
            //DateTime sempDate = DateTime.Now;
            string sex = Request["Sex"];
            string charge = Request["Charge"];
            IQueryable<WWBMDModel> BMDs = WWBMDBLL.GetAllWWBMD();
            if (!string.IsNullOrWhiteSpace(userbh))
            {
                BMDs = BMDs.Where(a => a.NUMBER.Contains(userbh));
            }
            if (!string.IsNullOrWhiteSpace(username))
            {
                BMDs = BMDs.Where(a => a.NAME.Contains(username));
            }
            //else if (DateTime.TryParse(endtime, out tempDate) && DateTime.TryParse(starttime, out sempDate))
            //{
            //    tempDate = tempDate.AddDays(1);
            //    BMDs = BMDs.Where(t => t.BIRTHDAY <= tempDate && t.BIRTHDAY >= sempDate);
            //}
            if (sex != "请选择性别")
            {
                decimal NumSex = decimal.Parse(sex);
                BMDs = BMDs.Where(a => a.SEX == NumSex);
            }
            if (!string.IsNullOrWhiteSpace(charge))
            {
                BMDs = BMDs.Where(a => a.CHARGE.Contains(charge));
            }
            int count = BMDs != null ? BMDs.Count() : 0;
            List<WWBMDModel> list = BMDs.OrderByDescending(a => a.CREATETIME.Value)
              .Skip((int)iDisplayStart)
              .Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;
            var result = (from r in list
                          select new
                          {
                              iDisplayStart = iDisplayStart,
                              iDisplayLength = iDisplayLength,
                              BMDID = r.BMDID,
                              CORRECTUNIT = r.CORRECTUNIT,
                              NUMBER = r.NUMBER,
                              CORRECTDATE = r.CORRECTDATE == null ? "" : string.Format("{0:yyyy-MM-dd}", r.CORRECTDATE),
                              NAME = r.NAME,
                              SEX = r.SEX == 0 ? "男" : "女",
                              CHARGE = r.CHARGE,
                              //TypeName = r.BMD_BMDTYPE.TYPENAME,
                              SEQNO = seqno++
                          }).ToList();
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 白名单详情数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowPage()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string BMDID = Request["BMDID"];
            if (string.IsNullOrEmpty(BMDID))
            {
                return View("Index.cshtml");
            }
            else
            {
                decimal NumBMDID = decimal.Parse(BMDID);
                WWBMDModel WWBMD = WWBMDBLL.GetWWBMDDelByID(NumBMDID);
                IQueryable<BMDUserAreaModel> modelList = BMDUserAreaBLL.GetBMDUserAreaListByID(NumBMDID);
                ViewBag.MapList = modelList.ToList();
                ViewBag.TypeName = WWBMD.TypeName;
                ViewBag.CORRECTDATE = WWBMD.CORRECTDATE == null ? "" : WWBMD.CORRECTDATE.Value.ToString("yyyy-MM-dd");
                ViewBag.BIRTHDAY = WWBMD.BIRTHDAY == null ? "" : WWBMD.BIRTHDAY.Value.ToString("yyyy-MM-dd");
                ViewBag.SENTENCEDATE = WWBMD.SENTENCEDATE == null ? "" : WWBMD.SENTENCEDATE.Value.ToString("yyyy-MM-dd");
                ViewBag.SENTENCESTATRTIME = WWBMD.SENTENCESTATRTIME == null ? "" : WWBMD.SENTENCESTATRTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.SENTENCEENDTIME = WWBMD.SENTENCEENDTIME == null ? "" : WWBMD.SENTENCEENDTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.CORRECTSTARTTIME = WWBMD.CORRECTSTARTTIME == null ? "" : WWBMD.CORRECTSTARTTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.CORRECTENDTIME = WWBMD.CORRECTENDTIME == null ? "" : WWBMD.CORRECTENDTIME.Value.ToString("yyyy-MM-dd");
                ViewBag.Path = ConfigurationManager.AppSettings["BMDOriginalPath"];
                return View("ShowPage", WWBMD);
            }
        }


        /// <summary>
        /// 编辑白名单页面数据
        /// </summary>
        /// <param name="ComIfmnID"></param>
        /// <returns></returns>
        public ActionResult EditPage()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string BMDID = Request["BMDID"];
            if (string.IsNullOrEmpty(BMDID))
            {
                return View("Index.cshtml");
            }
            decimal NumBMDID = decimal.Parse(BMDID);
            WWBMDModel WWBMD = WWBMDBLL.GetAllWWBMD().Where(t => t.BMDID == NumBMDID).FirstOrDefault();
            ViewBag.CORRECTDATE = WWBMD.CORRECTDATE == null ? "" : WWBMD.CORRECTDATE.Value.ToString("yyyy-MM-dd");
            ViewBag.BIRTHDAY = WWBMD.BIRTHDAY == null ? "" : WWBMD.BIRTHDAY.Value.ToString("yyyy-MM-dd");
            ViewBag.SENTENCEDATE = WWBMD.SENTENCEDATE == null ? "" : WWBMD.SENTENCEDATE.Value.ToString("yyyy-MM-dd");
            ViewBag.SENTENCESTATRTIME = WWBMD.SENTENCESTATRTIME == null ? "" : WWBMD.SENTENCESTATRTIME.Value.ToString("yyyy-MM-dd");
            ViewBag.SENTENCEENDTIME = WWBMD.SENTENCEENDTIME == null ? "" : WWBMD.SENTENCEENDTIME.Value.ToString("yyyy-MM-dd");
            ViewBag.CORRECTSTARTTIME = WWBMD.CORRECTSTARTTIME == null ? "" : WWBMD.CORRECTSTARTTIME.Value.ToString("yyyy-MM-dd");
            ViewBag.CORRECTENDTIME = WWBMD.CORRECTENDTIME == null ? "" : WWBMD.CORRECTENDTIME.Value.ToString("yyyy-MM-dd");
            if (WWBMD != null)
            {
                //获取类型
                ViewBag.BMDType = WWBMDBLL.GetAllBMDType().ToList().
                    Select(c => new SelectListItem()
                    {
                        Text = c.TYPENAME,
                        Value = c.TYPEID.ToString()
                    }).ToList();
                //获取婚姻状态
                string[] names = new string[] { "已婚", "未婚" };
                string[] values = new string[] { "1", "2" };
                List<SelectListItem> select = new List<SelectListItem>();
                for (int i = 0; i < names.Length; i++)
                {
                    select.Add(new SelectListItem
                    {
                        Text = names[i],
                        Value = values[i]
                    });
                }
                IQueryable<BMDUserAreaModel> modelList = BMDUserAreaBLL.GetBMDUserAreaListByID(NumBMDID);
                ViewBag.MapList = modelList.ToList();
                ViewBag.MapListCount = modelList.Count();
                ViewBag.Marriage = select;
                ViewBag.Path = ConfigurationManager.AppSettings["BMDOriginalPath"];
            }
            return View("EditPage", WWBMD);
        }


        /// <summary>
        /// 编辑白名单数据
        /// </summary>
        /// <returns></returns>
        public void EditSave(WWBMDModel WWBMD)
        {
            #region 头像图片
            HttpFileCollectionBase files = Request.Files;
            List<FileClass> fileClass = new List<FileClass>();
            if (files.Count > 0 && files != null && files[0].ContentLength > 0)
            {
                string Ofilepath = ConfigurationManager.AppSettings["BMDOriginalPath"];
                string ffilepath = ConfigurationManager.AppSettings["BMDFilesPath"];
                string sfilepath = ConfigurationManager.AppSettings["BMDSmallPath"];
                fileClass = new ImageUpload().UploadImages(files, Ofilepath, ffilepath, sfilepath);
                for (int i = 0; i < files.Count; i++)
                {
                    WWBMD.FILEPATH = fileClass[i].OriginalPath;
                    WWBMD.FILENAME = fileClass[i].OriginalName;
                }
            }
            #endregion
            WWBMDBLL.EditWWBMD(WWBMD);// 编辑白名单基础数据
            WWBMDBLL.DeleteBMD_USERAREA(WWBMD.BMDID);

            if (WWBMD.MapAddress != null)
            {
                foreach (var item in WWBMD.MapAddress)
                {
                    BMD_USERAREA UserArea = new BMD_USERAREA();
                    UserArea.BMDID = WWBMD.BMDID;
                    UserArea.ADDRESSNAME = item.MapAddressName;
                    UserArea.GEOMETRY = item.MapAddressID;
                    BMDUserAreaBLL.AddBMDUserArea(UserArea);
                }
            }



            //string Num = Request["Num"];
            //if (!string.IsNullOrWhiteSpace(Num) && Num != "0")
            //{
            //    foreach (var item in WWBMD.MapAddress)
            //    {
            //        BMD_USERAREA UserArea = new BMD_USERAREA();
            //        UserArea.BMDID = WWBMD.BMDID;
            //        UserArea.GEOMETRY = item.MapAddressID;
            //        UserArea.ADDRESSNAME = item.MapAddressName;
            //        // BMDUserAreaBLL.EditBMDUserArea(UserArea);
            //    }
            //}
            Response.Write("<script>window.location.href='/WWBMD/Index/?flag=2'</script>");
            // return RedirectToAction("Index");
        }

        /// <summary>
        /// 删除白名单
        /// </summary>
        /// <returns></returns>
        public ContentResult Delete()
        {
            //接收添加数据
            decimal BMDID = decimal.Parse(Request["BMDID"]);
            //删除数据
            BMD_WWBMD model = WWBMDBLL.GetWWBMDByID(BMDID);
            if (model != null)
            {
                WWBMDBLL.DeleteWWBMD(model);
            }
            return Content("删除成功");
        }

        /// <summary>
        /// 白名单编号唯一校验
        /// </summary>
        /// <returns></returns>
        public ContentResult CheckNumber()
        {
            string number = Request["NUMBER"];
            string result = WWBMDBLL.CheckNumber(number);

            return Content(result);
        }
    }
}
