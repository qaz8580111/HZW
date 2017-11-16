using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.SimpleCaseModels;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class SimpleCaseController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/SimpleCase/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        //新增简易案件表单页面
        public ActionResult Add()
        {
            string ID = this.Request.Form["ID"];
            string type = this.Request.Form["Type"];
            ViewBag.ID = ID;
            ViewBag.type = type;

            //获取所有的大类
            List<SelectListItem> dlList = IllegalItemBLL
                .GetIllegalClassesByParentID(null)
                .ToList().Select(c => new SelectListItem()
                {
                    Text = c.ILLEGALCODE + " " + c.ILLEGALCLASSNAME,
                    Value = c.ILLEGALCLASSID.ToString()
                }).ToList();

            dlList.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "请选择大类"
            });

            //所属大类的绑定
            ViewBag.dlList = dlList;

            return View(THIS_VIEW_PATH + "Add.cshtml");
        }

        //提交新增简易案件
        [HttpPost]
        public ActionResult CommitSimpleCase(SimpleCase simpleCase)
        {
            HttpFileCollectionBase files = Request.Files;

            string strPictureTypes = Request.Form["picturetype"];

            string[] pictureTypes = strPictureTypes.Split(',');

            SimpleCaseBLL.AddSimpleCase(simpleCase, files, pictureTypes);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 分页显示简易案件列表数据
        /// </summary>
        /// <returns>json 格式的数据</returns>
        public JsonResult GetSimpleCases(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];

            //起始日期 & 结束日期
            DateTime startDate;
            DateTime endDate;

            IQueryable<SimpleCase> simpleCases = SimpleCaseBLL.GetSimpleCases();

            if (DateTime.TryParse(strStartDate, out startDate))
            {
                simpleCases = simpleCases.Where(t => t.CaseTime >= startDate);
            }

            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                simpleCases = simpleCases.Where(t => t.CaseTime <= endDate);
            }

            List<SimpleCase> list = simpleCases
                .OrderByDescending(t => t.CaseTime)
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength).ToList();

            var results =
               from m in list
               select new
               {
                   SimpleCaseID = m.SimpleCaseID,
                   JDSBH = m.JDSBH,
                   WFXWName = m.WFXWName,
                   CaseTime = string.Format("{0:MM-dd HH:mm}", m.CaseTime),
                   CaseTimeYY = string.Format("{0:yyyy-MM-dd HH:mm}", m.CaseTime),
                   DSRNAME = m.DSRLX == "gr" ? m.DSRName : m.FZRName
               };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = results.Count(),
                iTotalDisplayRecords = simpleCases.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        //查看简易案件详细
        public ActionResult ShowSimpleCase()
        {
            string strSimpleCaseID = this.Request.QueryString["simpleCaseID"];

            SimpleCase simpleCase = SimpleCaseBLL
                .GetSimpleCaseBySimpleCaseID(decimal.Parse(strSimpleCaseID));

            IQueryable<SIMPLECASEPICTURE> simpleCasePictures = SimpleCasePictureBLL.
            GetPicturesBySimpleCaseID(decimal.Parse(strSimpleCaseID));

            ViewBag.simpleCasePictures = simpleCasePictures.ToList();

            return View(THIS_VIEW_PATH + "ShowSimpleCase.cshtml", simpleCase);
        }

        //根据图片标识获取图片
        public void GetPictureByPictureID(decimal pictureID)
        {
            SIMPLECASEPICTURE simpleCasePicture = SimpleCasePictureBLL
                .GetPictureByPictureID(pictureID);

            if (simpleCasePicture != null)
            {
                this.Response.Clear();
                this.Response.BinaryWrite(simpleCasePicture.PICTURE);
            }
        }

    }
}
