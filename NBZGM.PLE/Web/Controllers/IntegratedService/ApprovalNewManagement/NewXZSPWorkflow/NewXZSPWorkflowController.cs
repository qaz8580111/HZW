using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflowController : Controller
    {
        //
        // GET: /XZSPWorkflow/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        public ActionResult Index()
        {
            return View();
        }
        //行政审批处理action
        public ActionResult NewXZSPWorkflowProcess()
        {
            string ADID = this.Request["ADID"];//当前流程状态编号
            string AIID = this.Request["AIID"];//主表中的编号
            StringBuilder sbMes = new StringBuilder();
            //获取行政审批表单
            IList<XZSPNEWTAB> list = ActivityInstanceBLL.GetList().Where(a => a.AIID == AIID).OrderBy(a => a.PQSJ).ToList();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].PQR))
                    {
                        DateTime dt = Convert.ToDateTime(list[i].PQSJ);
                        string PQR = list[i].PQR;
                        string PQYJ = list[i].PQYJ;
                        decimal PQRDecimal;
                        decimal.TryParse(PQR, out PQRDecimal);
                        string unitDZname = "";
                        USER userModel = UserBLL.GetUserSingleByUserId(PQRDecimal);
                        if (userModel != null)
                        {
                            //所在大队名称
                            unitDZname = UnitBLL.GetUnitByUnitID(UnitBLL.GetUnitIDByUserID
                                (userModel.USERID)).FirstOrDefault().UNITNAME;
                        }
                        if (list[i].ADID == 1)
                        {
                            sbMes.Append("<table class='table table-bordered'>");
                            sbMes.Append("<tr>");
                            sbMes.Append("<th colspan='4' style='font-weight: bold; height: 25px; font-size: 16px'>执法大队派遣" + "</th>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>指派大队" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + unitDZname + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣意见" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + PQYJ + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣人" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + userModel.USERNAME + "</td>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣时间" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + dt + "</td>");
                            sbMes.Append("</tr>");
                            sbMes.Append("</table>");
                        }
                        if (list[i].ADID == 2)
                        {
                            sbMes.Append("<table class='table table-bordered'>");
                            sbMes.Append("<tr>");
                            sbMes.Append("<th colspan='4' style='font-weight: bold; height: 25px; font-size: 16px'>执法中队派遣" + "</th>");  
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>指派中队" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + unitDZname + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣意见" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + PQYJ + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣人" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + userModel.USERNAME + "</td>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣时间" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + dt + "</td>");
                            sbMes.Append("</tr>");
                            sbMes.Append("</table>");
                        }
                        if (list[i].ADID == 3)
                        {
                            sbMes.Append("<table class='table table-bordered'>");
                            sbMes.Append("<tr>");
                            sbMes.Append("<th colspan='4' style='font-weight: bold; height: 25px; font-size: 16px'>执法队员派遣" + "</th>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>指派中队队员" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + unitDZname + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣意见" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + PQYJ + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣人" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + userModel.USERNAME + "</td>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>派遣时间" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + dt + "</td>");
                            sbMes.Append("</tr>");
                            sbMes.Append("</table>");

                        }
                        if (list[i].ADID == 4)
                        {
                            sbMes.Append("<table class='table table-bordered'>");
                            sbMes.Append("<tr>");
                            sbMes.Append("<th colspan='4' style='font-weight: bold; height: 25px; font-size: 16px'>执法队员处理" + "</th>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>中队处理队员" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + unitDZname + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>处理意见" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + PQYJ + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>处理人" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + userModel.USERNAME + "</td>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>处理时间" + "</th>");
                            sbMes.Append("<td style='line-height: 30px; width: 40%;'>" + dt + "</td>");
                            sbMes.Append("</tr>");
                            sbMes.Append("</table>");
                        }
                        if (list[i].ADID == 5)
                        {
                            sbMes.Append("<table class='table table-bordered'>");
                            sbMes.Append("<tr>");
                            sbMes.Append("<th colspan='4' style='font-weight: bold; height: 25px; font-size: 16px'>中队长审核" + "</th>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>中队长" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + unitDZname + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>审核意见" + "</th>");
                            sbMes.Append("<td colspan='3' style='height: 40px;'>" + PQYJ + "</td>");
                            sbMes.Append("</tr>");

                            sbMes.Append("<tr>");
                            sbMes.Append("<th style='text-align: center; line-height: 30px;'>处理时间" + "</th>");
                            sbMes.Append("<td colspan='3' style='line-height: 30px;'>" + dt + "</td>");
                            sbMes.Append("</tr>");
                            sbMes.Append("</table>");
                        }

                    }
                }
            }
            ViewBag.resultMes = sbMes;

            XZSPNEWTAB XZSPNEWTABModel = ActivityInstanceBLL.GetList()
                .Where(a => a.AIID == AIID && a.ADID == 4).OrderByDescending(a => a.PQSJ).FirstOrDefault();
            if (XZSPNEWTABModel == null)
                XZSPNEWTABModel = new XZSPNEWTAB();
            ViewBag.Pic1Path = XZSPNEWTABModel.ATTACHMENT1;
            ViewBag.Pic2Path = XZSPNEWTABModel.ATTACHMENT2;
            ViewBag.Pic3Path = XZSPNEWTABModel.ATTACHMENT3;


            //流程活动控制器
            ViewBag.ControllerName = string.Format("NewXZSPWorkflow{0}",
                ADID.Trim());
            string ss = ViewBag.ControllerName;
            //流程活动附件控制器
            //ViewBag.ControllerAttachName = string.Format("NewXZSPAttachment{0}"
            //    , ADID.Trim());
            ViewBag.ADID = ADID;
            ViewBag.AIID = AIID;
            return View(THIS_VIEW_PATH + "NewXZSPWorkflowProcess.cshtml");
        }


        /// <summary>
        /// 展现图片
        /// </summary>
        /// <param name="AIID"></param>
        /// <returns></returns>
        public ActionResult XZSPAttachment(string AIID)
        {
            return View(@"~/Views/IntegratedService/ApprovalNewManagement/NEWXZSPAttachment/NEWXZSPAttachment.cshtml");
        }
    }
}
