using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Model.ParkingCaseModels;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class ParkingCaseController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/ParkingCase/";

        public ActionResult Index()
        {
            //获取单位标识
            string unitID = SessionManager.User.UnitID.ToString();
            //获取所有采集单位简称
            List<SelectListItem> dlList = ParkingCaseBLL
                .GetGOVs()
                .ToList().Select(c => new SelectListItem()
                {
                    Text = c.JC,
                    Value = c.XZQH
                }).ToList();

            dlList.Insert(0, new SelectListItem()
            {
                Value = "",
                Text = "全部"
            });

            //所属采集的绑定
            ViewBag.SSQY = dlList;
            ViewBag.cjdw = unitID;
            //所属处理单位
            ViewBag.SSCL = dlList;
            ViewBag.cldw = "0";
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示违停案件列表数据
        /// </summary>
        /// <returns>json 格式的数据</returns>
        public JsonResult GetParkingCases(int? iDisplayStart,
            int? iDisplayLength, int? secho)
        {
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];
            ///车牌号
            string strCarNo = this.Request.QueryString["carNo"];
            ///采集单位
            string strCJDW = this.Request.QueryString["cjdw"];
            ///处理单位
            string strCLDW = this.Request.QueryString["cldw"];
            ///处理状态
            string strCLZT = this.Request.QueryString["clzt"];
            ///数据状态
            string strCSBJ = this.Request.QueryString["csbj"];
            ///违停次数
            string strWTCS = this.Request.QueryString["wtcs"];

            IQueryable<ParkingCase> parkingCases = ParkingCaseBLL
                .GetPakringCasesByDateAndWTCS(strStartDate, strEndDate, strWTCS);

            if (!string.IsNullOrWhiteSpace(strCarNo))
            {
                parkingCases = parkingCases.Where(t => t.carNo.Contains(strCarNo.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(strCJDW))
            {
                parkingCases = parkingCases.Where(t => t.CJDW == strCJDW);
            }

            if (!string.IsNullOrWhiteSpace(strCLDW))
            {
                parkingCases = parkingCases.Where(t => t.CLDW == strCLDW);
            }

            if (!string.IsNullOrWhiteSpace(strCLZT))
            {
                //处理状态为 1 时，过滤状态为 4 的数据
                if (strCLZT.Equals("1"))
                {
                    parkingCases = parkingCases.Where(t => t.CLZT != "4");
                }
                //处理状态为 2 时，过滤审核人为空,审核作废原因不为空且校对结果不为 0 的数据
                else if (strCLZT.Equals("2"))
                {
                    parkingCases = parkingCases
                        .Where(t => t.FZSHR != null
                            && t.FZSHYJ == null && t.JDJG == "0");
                }
                //处理状态为 3 时，过滤审核人及审核作废原因为空的数据
                else if (strCLZT.Equals("3"))
                {
                    parkingCases = parkingCases
                        .Where(t => t.FZSHR != null
                            && t.FZSHYJ != null);
                }
                //处理状态为 4 时，过滤状态不为 4 的数据
                else if (strCLZT.Equals("4"))
                {
                    parkingCases = parkingCases.Where(t => t.CLZT == "4");
                }
            }

            if (!string.IsNullOrWhiteSpace(strCSBJ))
            {
                parkingCases = parkingCases.Where(t => t.CSBJ == strCSBJ);
            }

            List<ParkingCase> list = parkingCases
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength).ToList();

            var results =
               from m in list
               select new
               {
                   XLH = m.XLH,
                   carNo = m.carNo,
                   carType = m.carType,
                   caseTime = string.Format("{0:MM-dd HH:mm:ss}", m.caseTime),
                   caseTimeYY = string.Format("{0:yyyy-MM-dd HH:mm:ss}", m.caseTime),
                   caseAddress = m.caseAddress,
                   clztName = ParkingCaseBLL.GetCLZTNameByCLZT(m.CLZT),
                   csbjName = ParkingCaseBLL.GetCSBJNameByCSBJ(m.CSBJ)
               };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = parkingCases.Count(),
                iTotalDisplayRecords = parkingCases.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        //查看违停案件详细
        public ActionResult ShowParkingCase()
        {
            string strXLH = this.Request.QueryString["XLH"];

            ParkingCase parkingCase = ParkingCaseBLL
                .GetPakringCaseByXLH(decimal.Parse(strXLH));

            parkingCase.CJR = ParkingCaseBLL.GetUserNameByUserID(parkingCase.CJR);
            parkingCase.JDR = ParkingCaseBLL.GetUserNameByUserID(parkingCase.JDR);
            parkingCase.CLZT = ParkingCaseBLL.GetCLZTNameByCLZT(parkingCase.CLZT);
            parkingCase.FZSHR = ParkingCaseBLL.GetUserNameByUserID(parkingCase.FZSHR);
            parkingCase.CSBJ = ParkingCaseBLL.GetCSBJNameByCSBJ(parkingCase.CSBJ);
            parkingCase.CLDW = ParkingCaseBLL.GetGOVNameByCLDW(parkingCase.CLDW);

            Picture picture = ParkingCaseBLL
                .GetPicutresByXLH(decimal.Parse(strXLH));

            ViewBag.picture = picture;

            return View(THIS_VIEW_PATH + "ShowParkingCase.cshtml", parkingCase);
        }
    }
}
