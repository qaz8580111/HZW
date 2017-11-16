using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.AidDecisionMaking;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.TJGHZFBLLs;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.BLL.XZZFBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;


namespace Web.Controllers.AidDecisionMaking.ReportStatistics
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/
        public const string THIS_VIEW_PATH = @"~/Views/AidDecisionMaking/ReportStatistics/";
        public StringBuilder sbMes = new StringBuilder();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 一般案件数据
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseCount()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetCaseCount();
            return View(THIS_VIEW_PATH + "CaseCount.cshtml", list);
        }
        /// <summary>
        /// 简易案件数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SimpleCaseCount()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetSimpleCase();
            return View(THIS_VIEW_PATH + "SimpleCaseCount.cshtml", list);

        }
        /// <summary>
        /// 执法事件数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ZFSJCount()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetZFSJCount();
            return View(THIS_VIEW_PATH + "ZFSJCount.cshtml", list);

        }
        /// <summary>
        /// 一般案件条形图
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseChart()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetCaseCount();
            //List<ChartDATA> casedata = (from l in list
            //                            select new ChartDATA()
            //                            {
            //                                name = l.UNITNAME,
            //                                data = "[" + l.January.ToString() + "," + l.February.ToString() + "," + l.March.ToString() + "," + l.April.ToString() + "," + l.May.ToString() + "," + l.June.ToString() + "," + l.July.ToString() + "," + l.August.ToString() + "," + l.September.ToString() + "," + l.October.ToString() + "," + l.November.ToString() + "," + l.December.ToString() + "]"
            //                            }).ToList();
            //ViewBag.CaseChartList = JsonHelper.JsonSerializer<List<ChartDATA>>(casedata);
            string str = "[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{\"data\":[" + list[i].January.ToString() + "," + list[i].February.ToString() + "," + list[i].March + "," + list[i].April.ToString() + "," + list[i].May.ToString() + "," + list[i].June.ToString() + "," + list[i].July.ToString() + "," + list[i].August.ToString() + "," + list[i].September.ToString() + "," + list[i].October.ToString() + "," + list[i].November.ToString() + "," + list[i].December.ToString() + "],\"name\":'" + list[i].UNITNAME + "'}";
                if (i < list.Count - 1)
                {
                    str += ",";
                }
            }
            str += "]";
            ViewBag.CaseChartList = str;

            return View(THIS_VIEW_PATH + "CaseChart.cshtml");
        }
        /// <summary>
        /// 简易案件条形图
        /// </summary>
        /// <returns></returns>
        public ActionResult SimpleCaseChart()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetSimpleCase();
            string str = "[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{\"data\":[" + list[i].January.ToString() + "," + list[i].February.ToString() + "," + list[i].March + "," + list[i].April.ToString() + "," + list[i].May.ToString() + "," + list[i].June.ToString() + "," + list[i].July.ToString() + "," + list[i].August.ToString() + "," + list[i].September.ToString() + "," + list[i].October.ToString() + "," + list[i].November.ToString() + "," + list[i].December.ToString() + "],\"name\":'" + list[i].UNITNAME + "'}";
                if (i < list.Count - 1)
                {
                    str += ",";
                }
            }
            str += "]";
            ViewBag.SimpleCaseChartList = str;

            return View(THIS_VIEW_PATH + "SimpleCaseChart.cshtml");
        }
        /// <summary>
        /// 执法事件条形图
        /// </summary>
        /// <returns></returns>
        public ActionResult ZFSJChart()
        {
            List<CaseCount> list = ReportStatisticsBLL.GetZFSJCount();
            string str = "[";
            for (int i = 0; i < list.Count; i++)
            {
                str += "{\"data\":[" + list[i].January.ToString() + "," + list[i].February.ToString() + "," + list[i].March + "," + list[i].April.ToString() + "," + list[i].May.ToString() + "," + list[i].June.ToString() + "," + list[i].July.ToString() + "," + list[i].August.ToString() + "," + list[i].September.ToString() + "," + list[i].October.ToString() + "," + list[i].November.ToString() + "," + list[i].December.ToString() + "],\"name\":'" + list[i].UNITNAME + "'}";
                if (i < list.Count - 1)
                {
                    str += ",";
                }
            }
            str += "]";
            ViewBag.ZFSJChartList = str;
            return View(THIS_VIEW_PATH + "ZFSJChart.cshtml");
        }

        /// <summary>
        /// ZFSJChartByQL表中插入数据
        /// </summary>
        /// <returns></returns>
        public void InsertZFSJChartByQL()
        {
            ReportStatisticsBLL.InsertZFSJChartByQL();
        }


        /// <summary>
        /// 根据问题大类统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ZFSJChartByQL()
        {
            //获取问题大类
            List<SelectListItem> questionDLlist = ZFSJQuestionClassBLL
                .GetZFSJQuestionDL().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString(),
                }).ToList();
            ViewBag.QuestionDL = questionDLlist;
            return View(THIS_VIEW_PATH + "ZFSJChartByQL.cshtml");
        }
        /// <summary>
        /// 获得执法事件跟96310数据
        /// </summary>
        /// <returns></returns>
        public string ZFSJCHARTTBYQL()
        {
            string ddId = Request["DDID"];

            decimal classid = decimal.Parse(Request["DDID"].ToString());
            IList<ZFSJCHARTBYQL> list = ReportStatisticsBLL.GetZFSJBYQL(classid);
            string TS = "[";
            string strzfsj = "[";
            string strsj96310 = "[";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                {
                    TS += ",";
                    strzfsj += ",";
                    strsj96310 += ",";
                }
                TS += "'" + Convert.ToDateTime(list[i].DTTIME).Day + "号'";
                strzfsj += list[i].ZFSJCOUNTS;
                strsj96310 += list[i].SJ96310;
            }
            TS += "]";
            strzfsj += "]";
            strsj96310 += "]";
            return (TS + "_" + strzfsj + "_" + strsj96310);
        }
        /// <summary>
        /// 行政执法统计
        /// </summary>
        /// <returns></returns>
        public ActionResult XZZFChart()
        {
            StringBuilder sbMes = new StringBuilder();
            //获取部门  三个大队以及 执法局
            decimal XZZFUnit;
            //获得传过来部门ID
            if (!decimal.TryParse(Request["UnitNameID"], out XZZFUnit))
            {
                XZZFUnit = 40;
                //Convert.ToDecimal(UnitBLL.GetAllUnits().Where(a => a.UNITID == 40 || a.UNITID == 80 || a.UNITID == 90 || a.UNITTYPEID == 2).OrderBy(a => a.SEQNO).FirstOrDefault().UNITID.ToString());
            }

            DateTime data;
            //获取传过来的时间
            if (!DateTime.TryParse(Request["data"], out data))
            {
                data = DateTime.Now.Date;
            }
            //拼接时间
            string YMDStart = data.ToString("yyyy-MM") + "-1";
            //转换为时间类型
            DateTime dtStart = Convert.ToDateTime(YMDStart);
            //结束时间
            DateTime dtEnt = dtStart.AddMonths(1);
            //获得xzzftablist 里的全部数据
            List<XZZFTABLIST> list = XZZFBLL.GetAllList().Where(t => t.DTTIME >= dtStart && t.DTTIME < dtEnt && t.UNITNAMEID == XZZFUnit).OrderBy(a => a.ID).ToList();
            //说明有值
            if (list.Count != 0)
            {
                List<XZZFQUESTIONCLASS> Qlist = XZZFCLASSBLL.GetXZZFQuestionDL().ToList();
                //定义变量，为累计计算各列的总数
                decimal ANYOTHERALL_t = 0, AYXCFXALL_t = 0, CASEBJALL_t = 0, CASEFAKYALL_t = 0, CASELAALL_t = 0, CASEMSWFSDYALL_t = 0, CASEMSWFCWYALL_t = 0, CASEOTHERALL_t = 0, CASESQFYZXALL_t = 0, CASEQZCSJALL_t = 0, CASEZLTYJALL_t = 0, CASEZZTZALL_t = 0, SIMPLEFKJALL_t = 0, SIMPLEFKYALL_t = 0, SUMBJALL_t = 0, SUMFKYALL_t = 0;
                foreach (var temp in Qlist)
                {
                    //获取此大类下的小类的数据
                    List<XZZFTABLIST> listNew = list.Where(a => a.XZZFQUESTIONCLASS.PARENTID == temp.CLASSID).ToList();

                    //定义变量，为累计计算各列给小类的总数
                    decimal ANYOTHERALL = 0, AYXCFXALL = 0, CASEBJALL = 0, CASEFAKYALL = 0, CASELAALL = 0, CASEMSWFSDYALL = 0, CASEMSWFCWYALL = 0, CASEOTHERALL = 0, CASESQFYZXALL = 0, CASEQZCSJALL = 0, CASEZLTYJALL = 0, CASEZZTZALL = 0, SIMPLEFKJALL = 0, SIMPLEFKYALL = 0, SUMBJ = 0, SUMFKY = 0, SUMBJALL = 0, SUMFKYALL = 0;
                    int i = 0;
                    foreach (var item in listNew)
                    {
                        #region 计算小类每列总算
                        AYXCFXALL += Convert.ToDecimal(item.AYXCFX);
                        ANYOTHERALL += Convert.ToDecimal(item.ANYOTHER);
                        SIMPLEFKJALL += Convert.ToDecimal(item.SIMPLEFKJ);
                        SIMPLEFKYALL += Convert.ToDecimal(item.SIMPLEFKY);
                        CASELAALL += Convert.ToDecimal(item.CASELA);
                        CASEBJALL += Convert.ToDecimal(item.CASEBJ);
                        CASEFAKYALL += Convert.ToDecimal(item.CASEFAKY);
                        CASEMSWFSDYALL += Convert.ToDecimal(item.CASEMSWFSDY);
                        CASEMSWFCWYALL += Convert.ToDecimal(item.CASEMSWFCWY);
                        CASEQZCSJALL += Convert.ToDecimal(item.CASEQZCSJ);
                        CASEZLTYJALL += Convert.ToDecimal(item.CASEZLTYJ);
                        CASEOTHERALL += Convert.ToDecimal(item.CASEOTHER);
                        CASEZZTZALL += Convert.ToDecimal(item.CASEZZTZ);
                        CASESQFYZXALL += Convert.ToDecimal(item.CASESQFYZX);
                        #endregion
                        #region 计算每行总数
                        SUMBJ = Convert.ToDecimal(item.SIMPLEFKJ) + Convert.ToDecimal(item.CASELA);
                        SUMFKY = Convert.ToDecimal(item.SIMPLEFKY) + Convert.ToDecimal(item.CASEFAKY);
                        //把各小类每行的办结总数跟罚款数小计
                        SUMBJALL += SUMBJ;
                        SUMFKYALL += SUMFKY;
                        #endregion
                        #region 拼接表格
                        sbMes.Append("<tr style='text-align:center;vertical-align: middle'>");
                        if (i == 0)
                        {
                            //小类数据大于一条的时候，合并的行+1（合计的那行）
                            if (listNew.Count > 1)
                            {
                                int rowspanCount = listNew.Count() + 1;
                                sbMes.Append("<td rowspan='" + rowspanCount + "'style='width: 20px;'>" + temp.CLASSNAME + "</td>");
                            }
                            else
                            {
                                //只有一条数据则不用合并行
                                sbMes.Append("<td style='width: 20px;'>" + temp.CLASSNAME + "</td>");
                            }
                        }
                        i++;
                        string tdId = "td_" + item.CLASSID.ToString() + "_1";
                        sbMes.Append("<td style='line-height:14px;'>" + item.XZZFQUESTIONCLASS.CLASSNAME + "</td>");
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.AYXCFX + "' style='text-align:center; width:80px;border:none;' disabled='true' id='" + tdId + "' onblur='blur("+item.CLASSID.ToString()+","+1+")' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_2";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.ANYOTHER + "' style='text-align:center; width:10px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_3";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.SIMPLEFKJ + "' style='text-align:center; width:26px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "'  />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_4";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.SIMPLEFKY + "' style='text-align:center; width:26px; border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_5";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASELA + "' style='text-align:center; width:36px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_6";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEBJ + "' style='text-align:center; width:36px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_7";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEFAKY + "' style='text-align:center; width:38px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_8";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEMSWFSDY + "' style='text-align:center; width:80px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_9";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEMSWFCWY + "' style='text-align:center; width:80px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_10";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEQZCSJ + "' style='text-align:center; width:58px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "'/>" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_11";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEZLTYJ + "' style='text-align:center; width:80px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_12";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEOTHER + "' style='text-align:center; width:58px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_13";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASEZZTZ + "' style='text-align:center; width:80px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "' />" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_14";
                        sbMes.Append("<td style='line-height:14px;'><input type='text' value='" + item.CASESQFYZX + "' style='text-align:center; width:80px;border:none;' aria-readonly='true' disabled='true' id='" + tdId + "'/>" + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_15";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + SUMBJ + "</td>");

                        tdId = "td_" + item.CLASSID.ToString() + "_16";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + SUMFKY + "</td>");
                        sbMes.Append("</tr>");
                        #endregion
                    }
                    //当小类数据超过一条时
                    if (listNew.Count > 1)
                    {
                        string tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_1";
                        #region 小计
                        sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                        sbMes.Append("<td style='line-height:14px;'>小计</td>");
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'  >" + AYXCFXALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_2";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + ANYOTHERALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_3";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + SIMPLEFKJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_4";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + SIMPLEFKYALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_5";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + CASELAALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_6";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + CASEBJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_7";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "' >" + CASEFAKYALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_8";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEMSWFSDYALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_9";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEMSWFCWYALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_10";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEQZCSJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_11";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEZLTYJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_12";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEOTHERALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_13";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASEQZCSJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_14";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + CASESQFYZXALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_15";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + SUMBJALL + "</td>");

                        tdId = "td_" + listNew.FirstOrDefault().CLASSID.ToString() + "_16";
                        sbMes.Append("<td style='line-height:14px;' id='" + tdId + "'>" + SUMFKYALL + "</td>");
                        sbMes.Append("</tr>");
                        #endregion
                    }

                    #region 合计
                    //把各小类的小计数累加
                    AYXCFXALL_t += AYXCFXALL;
                    ANYOTHERALL_t += ANYOTHERALL;
                    SIMPLEFKJALL_t += SIMPLEFKJALL;
                    SIMPLEFKYALL_t += SIMPLEFKYALL;
                    CASELAALL_t += CASELAALL;
                    CASEBJALL_t += CASEBJALL;
                    CASEFAKYALL_t += CASEFAKYALL;
                    CASEMSWFSDYALL_t += CASEMSWFSDYALL;
                    CASEMSWFCWYALL_t += CASEMSWFCWYALL;
                    CASEQZCSJALL_t += CASEQZCSJALL;
                    CASEZLTYJALL_t += CASEZLTYJALL;
                    CASEOTHERALL_t += CASEOTHERALL;
                    CASEZZTZALL_t += CASEZZTZALL;
                    CASESQFYZXALL_t += CASESQFYZXALL;
                    //把各小类办结总数跟罚款数小计累加
                    SUMBJALL_t += SUMBJALL;
                    SUMFKYALL_t += +SUMFKYALL;
                    #endregion
                }

                #region 合计
                sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                sbMes.Append("<td colspan='2'>合计</td>");
                sbMes.Append("<td id='" + AYXCFXALL_t + "' >" + AYXCFXALL_t + "</td>");
                sbMes.Append("<td id='" + ANYOTHERALL_t + "' >" + ANYOTHERALL_t + "</td>");
                sbMes.Append("<td id='" + SIMPLEFKJALL_t + "' >" + SIMPLEFKJALL_t + "</td>");
                sbMes.Append("<td id='" + SIMPLEFKYALL_t + "' >" + SIMPLEFKYALL_t + "</td>");
                sbMes.Append("<td id='" + CASELAALL_t + "' >" + CASELAALL_t + "</td>");
                sbMes.Append("<td id='" + CASEBJALL_t + "' >" + CASEBJALL_t + "</td>");
                sbMes.Append("<td id='" + CASEFAKYALL_t + "' >" + CASEFAKYALL_t + "</td>");
                sbMes.Append("<td id='" + CASEMSWFSDYALL_t + "' >" + CASEMSWFSDYALL_t + "</td>");
                sbMes.Append("<td id='" + CASEMSWFCWYALL_t + "' >" + CASEMSWFCWYALL_t + "</td>");
                sbMes.Append("<td id='" + CASEQZCSJALL_t + "' >" + CASEQZCSJALL_t + "</td>");
                sbMes.Append("<td id='" + CASEZLTYJALL_t + "' >" + CASEZLTYJALL_t + "</td>");
                sbMes.Append("<td id='" + CASEOTHERALL_t + "' >" + CASEOTHERALL_t + "</td>");
                sbMes.Append("<td id='" + CASEQZCSJALL_t + "' >" + CASEQZCSJALL_t + "</td>");
                sbMes.Append("<td id='" + CASESQFYZXALL_t + "' >" + CASESQFYZXALL_t + "</td>");
                sbMes.Append("<td id='" + SUMBJALL_t + "' >" + SUMBJALL_t + "</td>");
                sbMes.Append("<td id='" + SUMFKYALL_t + "' >" + SUMFKYALL_t + "</td>");
                sbMes.Append("</tr>");
                #endregion

            }
            else  //表示这个部门这个月没有数据
            {
                XZZFTABLIST model = new XZZFTABLIST();
                List<XZZFQUESTIONCLASS> XZZFlist = XZZFCLASSBLL.GetXZZFQuestion().Where(t => t.TYPEID == 2).ToList();
                for (int i = 0; i < XZZFlist.Count; i++)
                {
                    model = new XZZFTABLIST();
                    model.CLASSID = Convert.ToDecimal(XZZFlist[i].CLASSID);
                    model.ANYOTHER = 0;
                    model.AYXCFX = 0;
                    model.CASEBJ = 0;
                    model.CASEFAKY = 0;
                    model.CASELA = 0;
                    model.CASEMSWFSDY = 0;
                    model.CASEMSWFCWY = 0;
                    model.CASEOTHER = 0;
                    model.CASESQFYZX = 0;
                    model.CASEQZCSJ = 0;
                    model.CASEZLTYJ = 0;
                    model.CASEZZTZ = 0;
                    model.DTTIME = dtStart;
                    model.SHUSER = "";
                    model.SIMPLEFKJ = 0;
                    model.SIMPLEFKY = 0;
                    model.TBUSER = "";
                    model.UNITNAMEID = XZZFUnit;
                    XZZFBLL.InsertXZZF(model);
                }
                Response.Redirect("/Statistics/XZZFChart?data=" + dtStart + "&UnitNameID=" + XZZFUnit);
            }

            ViewBag.XZZFYear = dtStart.Year;
            ViewBag.XZZFMonth = dtStart.Month;
            ViewBag.UnitName = XZZFUnit;
            ViewBag.sbMes = sbMes;
            ViewBag.DTtime = data.ToString("yyyy年MM月");
            return View(THIS_VIEW_PATH + "XZZFChart.cshtml");

        }

        /// <summary>
        /// 行政执法添加数据（页面）
        /// </summary>
        /// <returns></returns>
        public ActionResult XZZFChartADD()
        {
            PLEEntities db = new PLEEntities();
            //获取处罚种类
            List<SelectListItem> CHUFZL = XZZFCLASSBLL
                .GetXZZFQuestion().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString(),
                }).ToList();
            ViewBag.CHUFZL = CHUFZL;
            //获取部门
            List<SelectListItem> Units = UnitBLL.GetAllUnits().Where(a => a.UNITID == 40 || a.UNITID == 80 || a.UNITID == 90 || a.UNITTYPEID == 2).OrderByDescending(a => a.SEQNO).ToList().
       Select(c => new SelectListItem()
       {
           Text = c.UNITNAME,
           Value = c.UNITID.ToString(),
       }).ToList();

            int XZZFYear;
            //页面传过来的年份的值  表示进入页面后点击年份下拉框
            int.TryParse(Request["XZZFYear"], out XZZFYear);
            int XZZFMonth;
            //页面传过来的月份的值  表示进入页面后点击月份下拉框
            int.TryParse(Request["XZZFMonth"], out XZZFMonth);
            int CHUFZLXL;
            //页面传过来的处罚内容ID
            int.TryParse(Request["CHUFZLXL"], out CHUFZLXL);
            decimal UnitNameID;
            //页面传过来的处罚内容ID
            decimal.TryParse(Request["UnitNameID"], out UnitNameID);
            DateTime dt = DateTime.Now;
            //表示第一次进入这个页面  值为0
            if (XZZFYear == 0)
                XZZFYear = dt.Year;
            if (XZZFMonth == 0)
                XZZFMonth = dt.Month;
            //默认数据库的第一条小类  页面为初始的加载的小类时
            if (CHUFZLXL == 0)
                CHUFZLXL = Convert.ToInt32(XZZFCLASSBLL.GetXZZFQuestion().Where(t => t.TYPEID == 2).FirstOrDefault().CLASSID);
            //默认查出的第一个部门ID  页面为初始的加载的部门时
            if (UnitNameID == 0)
                UnitNameID = Convert.ToDecimal(UnitBLL.GetAllUnits().Where(a => a.UNITID == 40 || a.UNITID == 80 || a.UNITID == 90 || a.UNITTYPEID == 2).OrderByDescending(a => a.SEQNO).FirstOrDefault().UNITID);
            ViewBag.XZZFYear = XZZFYear;
            ViewBag.XZZFMonth = XZZFMonth;
            ViewBag.UnitList = Units;
            //拼接时间
            string ymStr = XZZFYear + "-" + XZZFMonth + "-1";
            //转化为时间类型
            DateTime ymDtC = Convert.ToDateTime(ymStr);
            //获取此月的下个月
            DateTime ymDtN = ymDtC.AddMonths(1);
            //获得次月这条数据
            XZZFTABLIST model = XZZFBLL.GetAllList().Where(a => a.DTTIME >= ymDtC && a.DTTIME < ymDtN && a.CLASSID == CHUFZLXL && a.UNITNAMEID == UnitNameID).ToList().FirstOrDefault();
            if (model == null)
            {
                List<XZZFQUESTIONCLASS> list = XZZFCLASSBLL.GetXZZFQuestion().Where(t => t.TYPEID == 2).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    model = new XZZFTABLIST();
                    model.CLASSID = Convert.ToDecimal(list[i].CLASSID);
                    model.ANYOTHER = 0;
                    model.AYXCFX = 0;
                    model.CASEBJ = 0;
                    model.CASEFAKY = 0;
                    model.CASELA = 0;
                    model.CASEMSWFSDY = 0;
                    model.CASEMSWFCWY = 0;
                    model.CASEOTHER = 0;
                    model.CASESQFYZX = 0;
                    model.CASEQZCSJ = 0;
                    model.CASEZLTYJ = 0;
                    model.CASEZZTZ = 0;
                    model.DTTIME = ymDtC;
                    model.SHUSER = "";
                    model.SIMPLEFKJ = 0;
                    model.SIMPLEFKY = 0;
                    model.TBUSER = "";
                    model.UNITNAMEID = UnitNameID;
                    XZZFBLL.InsertXZZF(model);
                }

            }
            return View(THIS_VIEW_PATH + "XZZFADDChart.cshtml", model);
        }

        /// <summary>
        /// 行政执法添加数据（功能）
        /// </summary>
        public void AddXZZFTabList(XZZFTABLIST model)
        {
            int XZZFYear;
            //获得页面年份数据
            int.TryParse(Request["XZZFYear"], out XZZFYear);
            int XZZFMonth;
            //获得页面月份数据
            int.TryParse(Request["XZZFMonth"], out XZZFMonth);
            int CHUFZLXLID;
            //获得页面处罚内容
            int.TryParse(Request["CHUFZL"], out CHUFZLXLID);
            decimal UnitNameID;
            //获取页面部门内容
            decimal.TryParse(Request["Units"], out UnitNameID);
            //拼接时间数据
            string ymStr = XZZFYear + "-" + XZZFMonth + "-1";
            //转换成时间类型
            DateTime ymDtC = Convert.ToDateTime(ymStr);
            //获取此数据的下一个月
            DateTime ymDtN = ymDtC.AddMonths(1);
            XZZFBLL XZZFBLL = new XZZFBLL();
            //查询符合条件的数据
            XZZFTABLIST modelSel = XZZFBLL.GetAllList()
                .Where(a => a.DTTIME >= ymDtC && a.DTTIME < ymDtN && a.CLASSID == CHUFZLXLID && a.UNITNAMEID == UnitNameID).ToList().FirstOrDefault();
            if (modelSel != null)
            {
                model.DTTIME = ymDtC;
                model.ID = modelSel.ID;
                XZZFBLL.UpdateXZZF(model);
            }
            Response.Redirect("/Statistics/XZZFChart?data=" + ymStr + "&UnitNameID=" + UnitNameID);

        }

        /// <summary>
        /// 行政执法统计打印样式
        /// </summary>
        /// <returns></returns>
        public ActionResult XZZFPrintMe()
        {
            StringBuilder sbMes = new StringBuilder();
            DateTime data;
            //获取传过来的时间
            if (!DateTime.TryParse(Request["data"], out data))
            {
                data = DateTime.Now.Date;
            }
            decimal UnitName;
            if (!decimal.TryParse(Request["UnitName"], out UnitName))
            {
                UnitName = 40;
            }

            //拼接时间
            string YMDStart = data.ToString("yyyy-MM") + "-1";
            //转换为时间类型
            DateTime dtStart = Convert.ToDateTime(YMDStart);
            //结束时间
            DateTime dtEnt = dtStart.AddMonths(1);
            //获得xzzftablist 里的全部数据
            List<XZZFTABLIST> list = XZZFBLL.GetAllList().Where(t => t.DTTIME >= dtStart && t.DTTIME < dtEnt && t.UNITNAMEID == UnitName).OrderBy(a => a.ID).ToList();
            //说明有值
            if (list.Count != 0)
            {
                List<XZZFQUESTIONCLASS> Qlist = XZZFCLASSBLL.GetXZZFQuestionDL().ToList();
                //定义变量，为累计计算各列的总数
                decimal ANYOTHERALL_t = 0, AYXCFXALL_t = 0, CASEBJALL_t = 0, CASEFAKYALL_t = 0, CASELAALL_t = 0, CASEMSWFSDYALL_t = 0, CASEMSWFCWYALL_t = 0, CASEOTHERALL_t = 0, CASESQFYZXALL_t = 0, CASEQZCSJALL_t = 0, CASEZLTYJALL_t = 0, CASEZZTZALL_t = 0, SIMPLEFKJALL_t = 0, SIMPLEFKYALL_t = 0, SUMBJALL_t = 0, SUMFKYALL_t = 0;
                foreach (var temp in Qlist)
                {
                    //获取此大类下的小类的数据
                    List<XZZFTABLIST> listNew = list.Where(a => a.XZZFQUESTIONCLASS.PARENTID == temp.CLASSID).ToList();

                    //定义变量，为累计计算各列给小类的总数
                    decimal ANYOTHERALL = 0, AYXCFXALL = 0, CASEBJALL = 0, CASEFAKYALL = 0, CASELAALL = 0, CASEMSWFSDYALL = 0, CASEMSWFCWYALL = 0, CASEOTHERALL = 0, CASESQFYZXALL = 0, CASEQZCSJALL = 0, CASEZLTYJALL = 0, CASEZZTZALL = 0, SIMPLEFKJALL = 0, SIMPLEFKYALL = 0, SUMBJ = 0, SUMFKY = 0, SUMBJALL = 0, SUMFKYALL = 0;
                    int i = 0;
                    foreach (var item in listNew)
                    {
                        #region 计算小类每列总算
                        AYXCFXALL += Convert.ToDecimal(item.AYXCFX);
                        ANYOTHERALL += Convert.ToDecimal(item.ANYOTHER);
                        SIMPLEFKJALL += Convert.ToDecimal(item.SIMPLEFKJ);
                        SIMPLEFKYALL += Convert.ToDecimal(item.SIMPLEFKY);
                        CASELAALL += Convert.ToDecimal(item.CASELA);
                        CASEBJALL += Convert.ToDecimal(item.CASEBJ);
                        CASEFAKYALL += Convert.ToDecimal(item.CASEFAKY);
                        CASEMSWFSDYALL += Convert.ToDecimal(item.CASEMSWFSDY);
                        CASEMSWFCWYALL += Convert.ToDecimal(item.CASEMSWFCWY);
                        CASEQZCSJALL += Convert.ToDecimal(item.CASEQZCSJ);
                        CASEZLTYJALL += Convert.ToDecimal(item.CASEZLTYJ);
                        CASEOTHERALL += Convert.ToDecimal(item.CASEOTHER);
                        CASEZZTZALL += Convert.ToDecimal(item.CASEZZTZ);
                        CASESQFYZXALL += Convert.ToDecimal(item.CASESQFYZX);
                        #endregion
                        #region 计算每行总数
                        SUMBJ = Convert.ToDecimal(item.SIMPLEFKJ) + Convert.ToDecimal(item.CASELA);
                        SUMFKY = Convert.ToDecimal(item.SIMPLEFKY) + Convert.ToDecimal(item.CASEFAKY);
                        //把各小类每行的办结总数跟罚款数小计
                        SUMBJALL += SUMBJ;
                        SUMFKYALL += SUMFKY;
                        #endregion
                        #region 拼接表格
                        string tdId = "td_" + item.CLASSID.ToString() + "_" + data.ToString("yyyyMM") + "_";
                        sbMes.Append("<tr style='text-align:center;vertical-align: middle'>");
                        if (i == 0)
                        {
                            //小类数据大于一条的时候，合并的行+1（合计的那行）
                            if (listNew.Count > 1)
                            {
                                int rowspanCount = listNew.Count() + 1;
                                sbMes.Append("<td rowspan='" + rowspanCount + "'style='width: 20px;'>" + temp.CLASSNAME + "</td>");
                            }
                            else
                            {
                                //只有一条数据则不用合并行
                                sbMes.Append("<td style='width: 20px;'>" + temp.CLASSNAME + "</td>");
                            }
                        }
                        i++;
                        sbMes.Append("<td style='line-height:14px;'>" + item.XZZFQUESTIONCLASS.CLASSNAME + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.AYXCFX + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.ANYOTHER + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.SIMPLEFKJ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.SIMPLEFKY + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASELA + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEBJ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEFAKY + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEMSWFSDY + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEMSWFCWY + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEQZCSJ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEZLTYJ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEOTHER + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASEZZTZ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + item.CASESQFYZX + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SUMBJ + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SUMFKY + "</td>");
                        sbMes.Append("</tr>");
                        #endregion
                    }
                    //当小类数据超过一条时
                    if (listNew.Count > 1)
                    {
                        #region 小计
                        sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                        sbMes.Append("<td style='line-height:14px;'>小计</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + AYXCFXALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + ANYOTHERALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SIMPLEFKJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SIMPLEFKYALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASELAALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEBJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEFAKYALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEMSWFSDYALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEMSWFCWYALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEQZCSJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEZLTYJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEOTHERALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASEQZCSJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + CASESQFYZXALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SUMBJALL + "</td>");
                        sbMes.Append("<td style='line-height:14px;'>" + SUMFKYALL + "</td>");
                        sbMes.Append("</tr>");
                        #endregion
                    }

                    #region 合计
                    //把各小类的小计数累加
                    AYXCFXALL_t += AYXCFXALL;
                    ANYOTHERALL_t += ANYOTHERALL;
                    SIMPLEFKJALL_t += SIMPLEFKJALL;
                    SIMPLEFKYALL_t += SIMPLEFKYALL;
                    CASELAALL_t += CASELAALL;
                    CASEBJALL_t += CASEBJALL;
                    CASEFAKYALL_t += CASEFAKYALL;
                    CASEMSWFSDYALL_t += CASEMSWFSDYALL;
                    CASEMSWFCWYALL_t += CASEMSWFCWYALL;
                    CASEQZCSJALL_t += CASEQZCSJALL;
                    CASEZLTYJALL_t += CASEZLTYJALL;
                    CASEOTHERALL_t += CASEOTHERALL;
                    CASEZZTZALL_t += CASEZZTZALL;
                    CASESQFYZXALL_t += CASESQFYZXALL;
                    //把各小类办结总数跟罚款数小计累加
                    SUMBJALL_t += SUMBJALL;
                    SUMFKYALL_t += +SUMFKYALL;
                    #endregion
                }

                #region 合计
                sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                sbMes.Append("<td colspan='2'>合计</td>");
                sbMes.Append("<td>" + AYXCFXALL_t + "</td>");
                sbMes.Append("<td>" + ANYOTHERALL_t + "</td>");
                sbMes.Append("<td>" + SIMPLEFKJALL_t + "</td>");
                sbMes.Append("<td>" + SIMPLEFKYALL_t + "</td>");
                sbMes.Append("<td>" + CASELAALL_t + "</td>");
                sbMes.Append("<td>" + CASEBJALL_t + "</td>");
                sbMes.Append("<td>" + CASEFAKYALL_t + "</td>");
                sbMes.Append("<td>" + CASEMSWFSDYALL_t + "</td>");
                sbMes.Append("<td>" + CASEMSWFCWYALL_t + "</td>");
                sbMes.Append("<td>" + CASEQZCSJALL_t + "</td>");
                sbMes.Append("<td>" + CASEZLTYJALL_t + "</td>");
                sbMes.Append("<td>" + CASEOTHERALL_t + "</td>");
                sbMes.Append("<td>" + CASEQZCSJALL_t + "</td>");
                sbMes.Append("<td>" + CASESQFYZXALL_t + "</td>");
                sbMes.Append("<td>" + SUMBJALL_t + "</td>");
                sbMes.Append("<td>" + SUMFKYALL_t + "</td>");
                sbMes.Append("</tr>");
                #endregion

            }

            ViewBag.sbMes = sbMes;
            ViewBag.DTtime = data.ToString("yyyy年MM月");
            return View(THIS_VIEW_PATH + "XZZFPrintMe.cshtml");
        }
        /// <summary>
        /// 规划执法统计
        /// </summary>
        /// <returns></returns>
        public ActionResult GHZFChart()
        {
            string CreateUser = "";
            string CheckUser = "";
            decimal unitid = 0;
            #region 获取登录用户的大队或者所在区局编号
            List<UNIT> unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == SessionManager.User.UnitID).ToList();
            if (unitList != null && unitList.Count > 0)
            {
                if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                {
                    unitid = unitList[0].UNITID;
                }
                else
                {
                    //获取父级是否为大队类型，如果不是大队类型，则不需要修改功能
                    unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == unitList[0].PARENTID).ToList();
                    if (unitList != null && unitList.Count > 0)
                    {
                        if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                        {
                            unitid = unitList[0].UNITID;
                        }
                    }
                }
            }
            #endregion

            StringBuilder sbMes = new StringBuilder();
            DateTime data;
            if (!DateTime.TryParse(Request["data"], out data))
            {
                data = DateTime.Now.Date;
            }
            string YMDStart = data.ToString("yyyy-MM") + "-1";
            DateTime dtStart = Convert.ToDateTime(YMDStart);
            DateTime dtEnt = dtStart.AddMonths(1);

            ViewBag.GHZFYear = dtStart.Year;
            ViewBag.GHZFMonth = dtStart.Month;

            IQueryable<TJGHZF> list = new TJGHZFBLL().List().Where(a => a.TJTIME >= dtStart && a.TJTIME < dtEnt).OrderBy(a => a.ID);

            if (list != null)
            {
                decimal AYXCFXAll = 0, AYOTHERAll = 0, LAAll = 0, BJAll = 0, FKAll = 0, WFMJAll = 0, MSSWAll = 0, MSWFSRAll = 0, OZLTZJSAll = 0, ODDFYAll = 0, OYSAll = 0, OZZTZAll = 0, OSQFYZXSQAll = 0, OSQFYZXZJAll = 0, OBQSZFZCALLAll = 0, OBQSZFZCZJAll = 0, OXZFYSSAll = 0;
                //获取市局 三个大队的统计数据
                IList<TJGHZF> listSJ = list.Where(a => a.UNITID == 40 || a.UNITID == 80 || a.UNITID == 90).ToList();
                string SJMES = TJSJMes(listSJ);
                int i = 0;
                foreach (var item in list)
                {
                    if (item.UNITID == unitid)
                    {
                        CreateUser = item.CREATEUSER;
                        CheckUser = item.CHECKUSSER;
                    }
                    if (i == 3)//追加3个大队的数据
                    {
                        sbMes.Append(SJMES);
                    }
                    i++;

                    #region 计算统计结果
                    AYXCFXAll += Convert.ToDecimal(item.AYXCFX);
                    AYOTHERAll += Convert.ToDecimal(item.AYOTHER);
                    LAAll += Convert.ToDecimal(item.LA);
                    BJAll += Convert.ToDecimal(item.BJ);
                    FKAll += Convert.ToDecimal(item.FK);
                    WFMJAll += Convert.ToDecimal(item.WFMJ);
                    MSSWAll += Convert.ToDecimal(item.MSSW);
                    MSWFSRAll += Convert.ToDecimal(item.MSWFSR);
                    OZLTZJSAll += Convert.ToDecimal(item.OZLTZJS);
                    ODDFYAll += Convert.ToDecimal(item.ODDFY);
                    OYSAll += Convert.ToDecimal(item.OYS);
                    OZZTZAll += Convert.ToDecimal(item.OZZTZ);
                    OSQFYZXSQAll += Convert.ToDecimal(item.OSQFYZXSQ);
                    OSQFYZXZJAll += Convert.ToDecimal(item.OSQFYZXZJ);
                    OBQSZFZCALLAll += Convert.ToDecimal(item.OBQSZFZCALL);
                    OBQSZFZCZJAll += Convert.ToDecimal(item.OBQSZFZCZJ);
                    OXZFYSSAll += Convert.ToDecimal(item.OXZFYSS);
                    #endregion
                    #region 拼接表格
                    string tdId = "td_" + item.UNITID.ToString() + "_" + data.ToString("yyyyMM") + "_";
                    sbMes.Append("<tr style='text-align:center;vertical-align: middle'>");
                    sbMes.Append("<td>" + UnitBLL.GetUnitNameByUnitID(Convert.ToDecimal(item.UNITID)) + "</td>");
                    sbMes.Append("<td id='" + tdId + "AYXCFX'>" + item.AYXCFX + "</td>");
                    sbMes.Append("<td id='" + tdId + "AYOTHER'>" + item.AYOTHER + "</td>");
                    sbMes.Append("<td id='" + tdId + "LA'>" + item.LA + "</td>");
                    sbMes.Append("<td id='" + tdId + "BJ'>" + item.BJ + "</td>");
                    sbMes.Append("<td id='" + tdId + "FK'>" + item.FK + "</td>");
                    sbMes.Append("<td id='" + tdId + "WFMJ'>" + item.WFMJ + "</td>");
                    sbMes.Append("<td id='" + tdId + "MSSW'>" + item.MSSW + "</td>");
                    sbMes.Append("<td id='" + tdId + "MSWFSR'>" + item.MSWFSR + "</td>");
                    sbMes.Append("<td id='" + tdId + "OZLTZJS'>" + item.OZLTZJS + "</td>");
                    sbMes.Append("<td id='" + tdId + "ODDFY'>" + item.ODDFY + "</td>");
                    sbMes.Append("<td id='" + tdId + "OYS'>" + item.OYS + "</td>");
                    sbMes.Append("<td id='" + tdId + "OZZTZ'>" + item.OZZTZ + "</td>");
                    sbMes.Append("<td id='" + tdId + "OSQFYZXSQ'>" + item.OSQFYZXSQ + "</td>");
                    sbMes.Append("<td id='" + tdId + "OSQFYZXZJ'>" + item.OSQFYZXZJ + "</td>");
                    sbMes.Append("<td id='" + tdId + "OBQSZFZCALL'>" + item.OBQSZFZCALL + "</td>");
                    sbMes.Append("<td id='" + tdId + "OBQSZFZCZJ'>" + item.OBQSZFZCZJ + "</td>");
                    sbMes.Append("<td id='" + tdId + "OXZFYSS'>" + item.OXZFYSS + "</td>");
                    sbMes.Append("</tr>");
                    #endregion
                }

                #region 总结

                sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                sbMes.Append("<td>合计</td>");
                sbMes.Append("<td>" + AYXCFXAll + "</td>");
                sbMes.Append("<td>" + AYOTHERAll + "</td>");
                sbMes.Append("<td>" + LAAll + "</td>");
                sbMes.Append("<td>" + BJAll + "</td>");
                sbMes.Append("<td>" + FKAll + "</td>");
                sbMes.Append("<td>" + WFMJAll + "</td>");
                sbMes.Append("<td>" + MSSWAll + "</td>");
                sbMes.Append("<td>" + MSWFSRAll + "</td>");
                sbMes.Append("<td>" + OZLTZJSAll + "</td>");
                sbMes.Append("<td>" + ODDFYAll + "</td>");
                sbMes.Append("<td>" + OYSAll + "</td>");
                sbMes.Append("<td>" + OZZTZAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXSQAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXZJAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCALLAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCZJAll + "</td>");
                sbMes.Append("<td>" + OXZFYSSAll + "</td>");
                sbMes.Append("</tr>");

                #endregion

            }
            ViewBag.UNITID = unitid;
            ViewBag.CreateUser = CreateUser;
            ViewBag.CheckUser = CheckUser;
            ViewBag.TJTIME = data.ToString("yyyy年MM月");
            ViewBag.CXGHGLZFTJ = sbMes.ToString();
            return View(THIS_VIEW_PATH + "GHZFChart.cshtml");
        }

        /// <summary>
        /// 统计市局数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TJSJMes(IList<TJGHZF> list)
        {
            StringBuilder sbMes = new StringBuilder();
            if (list != null)
            {
                decimal AYXCFXAll = 0, AYOTHERAll = 0, LAAll = 0, BJAll = 0, FKAll = 0, WFMJAll = 0, MSSWAll = 0, MSWFSRAll = 0, OZLTZJSAll = 0, ODDFYAll = 0, OYSAll = 0, OZZTZAll = 0, OSQFYZXSQAll = 0, OSQFYZXZJAll = 0, OBQSZFZCALLAll = 0, OBQSZFZCZJAll = 0, OXZFYSSAll = 0;
                foreach (var item in list)
                {
                    #region 计算统计结果
                    AYXCFXAll += Convert.ToDecimal(item.AYXCFX);
                    AYOTHERAll += Convert.ToDecimal(item.AYOTHER);
                    LAAll += Convert.ToDecimal(item.LA);
                    BJAll += Convert.ToDecimal(item.BJ);
                    FKAll += Convert.ToDecimal(item.FK);
                    WFMJAll += Convert.ToDecimal(item.WFMJ);
                    MSSWAll += Convert.ToDecimal(item.MSSW);
                    MSWFSRAll += Convert.ToDecimal(item.MSWFSR);
                    OZLTZJSAll += Convert.ToDecimal(item.OZLTZJS);
                    ODDFYAll += Convert.ToDecimal(item.ODDFY);
                    OYSAll += Convert.ToDecimal(item.OYS);
                    OZZTZAll += Convert.ToDecimal(item.OZZTZ);
                    OSQFYZXSQAll += Convert.ToDecimal(item.OSQFYZXSQ);
                    OSQFYZXZJAll += Convert.ToDecimal(item.OSQFYZXZJ);
                    OBQSZFZCALLAll += Convert.ToDecimal(item.OBQSZFZCALL);
                    OBQSZFZCZJAll += Convert.ToDecimal(item.OBQSZFZCZJ);
                    OXZFYSSAll += Convert.ToDecimal(item.OXZFYSS);
                    #endregion
                }
                #region 总结

                sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                sbMes.Append("<td>合计</td>");
                sbMes.Append("<td>" + AYXCFXAll + "</td>");
                sbMes.Append("<td>" + AYOTHERAll + "</td>");
                sbMes.Append("<td>" + LAAll + "</td>");
                sbMes.Append("<td>" + BJAll + "</td>");
                sbMes.Append("<td>" + FKAll + "</td>");
                sbMes.Append("<td>" + WFMJAll + "</td>");
                sbMes.Append("<td>" + MSSWAll + "</td>");
                sbMes.Append("<td>" + MSWFSRAll + "</td>");
                sbMes.Append("<td>" + OZLTZJSAll + "</td>");
                sbMes.Append("<td>" + ODDFYAll + "</td>");
                sbMes.Append("<td>" + OYSAll + "</td>");
                sbMes.Append("<td>" + OZZTZAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXSQAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXZJAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCALLAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCZJAll + "</td>");
                sbMes.Append("<td>" + OXZFYSSAll + "</td>");
                sbMes.Append("</tr>");

                #endregion
            }
            return sbMes.ToString();
        }

        /// <summary>
        /// 提交修改规划执法统计
        /// </summary>
        /// <returns></returns>
        public int SubmitUpdateGHZF()
        {
            bool result = false;

            int UpdateUnitId, AYXCFX, AYOTHER, LA, BJ, FK, WFMJ, MSSW, MSWFSR, OZLTZJS, ODDFY, OYS, OZZTZ, OSQFYZXSQ, OSQFYZXZJ, OBQSZFZCALL, OBQSZFZCZJ, OXZFYSS;
            int.TryParse(Request["UpdateUnitId"], out UpdateUnitId);
            int.TryParse(Request["AYXCFX"], out AYXCFX);
            int.TryParse(Request["AYOTHER"], out AYOTHER);
            int.TryParse(Request["LA"], out LA);
            int.TryParse(Request["BJ"], out BJ);
            int.TryParse(Request["FK"], out FK);
            int.TryParse(Request["WFMJ"], out WFMJ);
            int.TryParse(Request["MSSW"], out MSSW);
            int.TryParse(Request["MSWFSR"], out MSWFSR);
            int.TryParse(Request["OZLTZJS"], out OZLTZJS);
            int.TryParse(Request["ODDFY"], out ODDFY);
            int.TryParse(Request["OYS"], out OYS);
            int.TryParse(Request["OZZTZ"], out OZZTZ);
            int.TryParse(Request["OSQFYZXSQ"], out OSQFYZXSQ);
            int.TryParse(Request["OSQFYZXZJ"], out OSQFYZXZJ);
            int.TryParse(Request["OBQSZFZCALL"], out OBQSZFZCALL);
            int.TryParse(Request["OBQSZFZCZJ"], out OBQSZFZCZJ);
            int.TryParse(Request["OXZFYSS"], out OXZFYSS);

            string UpdateTJTime = Request["UpdateTJTime"];
            string CreateUser = Request["CreateUser"];
            string CheckUser = Request["CheckUser"];

            DateTime dtTimeS = Convert.ToDateTime(UpdateTJTime.Replace("年", "-").Replace("月", "-") + "1");
            DateTime dtTimeE = Convert.ToDateTime(UpdateTJTime.Replace("年", "-").Replace("月", "-") + "28");

            IList<TJGHZF> list = new TJGHZFBLL().List().Where(a => a.UNITID == UpdateUnitId && a.TJTIME >= dtTimeS && a.TJTIME <= dtTimeE).ToList();
            TJGHZF model = list[0];
            model.AYXCFX = AYXCFX;
            model.AYOTHER = AYOTHER;
            model.LA = LA;
            model.BJ = BJ;
            model.FK = FK;
            model.WFMJ = WFMJ;
            model.MSSW = MSSW;
            model.MSWFSR = MSWFSR;
            model.OZLTZJS = OZLTZJS;
            model.ODDFY = ODDFY;
            model.OYS = OYS;
            model.OZZTZ = OZZTZ;
            model.OSQFYZXSQ = OSQFYZXSQ;
            model.OSQFYZXZJ = OSQFYZXZJ;
            model.OBQSZFZCALL = OBQSZFZCALL;
            model.OBQSZFZCZJ = OBQSZFZCZJ;
            model.OXZFYSS = OXZFYSS;
            model.CREATETIME = DateTime.Now;
            model.CREATEUSER = CreateUser;
            model.CHECKUSSER = CheckUser;

            result = new TJGHZFBLL().UpdateTJGHZF(model);
            if (result)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// 规划执法统计 （打印）
        /// </summary>
        /// <returns></returns>
        public ActionResult GHZFPrintMe()
        {
            string CreateUser = "";
            string CheckUser = "";
            decimal unitid = 0;
            #region 获取登录用户的大队或者所在区局编号
            List<UNIT> unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == SessionManager.User.UnitID).ToList();
            if (unitList != null && unitList.Count > 0)
            {
                if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                {
                    unitid = unitList[0].UNITID;
                }
                else
                {
                    //获取父级是否为大队类型，如果不是大队类型，则不需要修改功能
                    unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == unitList[0].PARENTID).ToList();
                    if (unitList != null && unitList.Count > 0)
                    {
                        if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                        {
                            unitid = unitList[0].UNITID;
                        }
                    }
                }
            }
            #endregion

            StringBuilder sbMes = new StringBuilder();
            DateTime data;
            if (!DateTime.TryParse(Request["data"], out data))
            {
                data = DateTime.Now.Date;
            }
            string YMDStart = data.ToString("yyyy-MM") + "-1";
            DateTime dtStart = Convert.ToDateTime(YMDStart);
            DateTime dtEnt = dtStart.AddMonths(1);

            ViewBag.GHZFYear = dtStart.Year;
            ViewBag.GHZFMonth = dtStart.Month;

            IQueryable<TJGHZF> list = new TJGHZFBLL().List().Where(a => a.TJTIME >= dtStart && a.TJTIME < dtEnt).OrderBy(a => a.ID);

            if (list != null)
            {
                decimal AYXCFXAll = 0, AYOTHERAll = 0, LAAll = 0, BJAll = 0, FKAll = 0, WFMJAll = 0, MSSWAll = 0, MSWFSRAll = 0, OZLTZJSAll = 0, ODDFYAll = 0, OYSAll = 0, OZZTZAll = 0, OSQFYZXSQAll = 0, OSQFYZXZJAll = 0, OBQSZFZCALLAll = 0, OBQSZFZCZJAll = 0, OXZFYSSAll = 0;
                //获取市局 三个大队的统计数据
                IList<TJGHZF> listSJ = list.Where(a => a.UNITID == 40 || a.UNITID == 80 || a.UNITID == 90).ToList();
                string SJMES = TJSJMes(listSJ);
                int i = 0;
                foreach (var item in list)
                {
                    if (item.UNITID == unitid)
                    {
                        CreateUser = item.CREATEUSER;
                        CheckUser = item.CHECKUSSER;
                    }
                    if (i == 3)//追加3个大队的数据
                    {
                        sbMes.Append(SJMES);
                    }
                    i++;

                    #region 计算统计结果
                    AYXCFXAll += Convert.ToDecimal(item.AYXCFX);
                    AYOTHERAll += Convert.ToDecimal(item.AYOTHER);
                    LAAll += Convert.ToDecimal(item.LA);
                    BJAll += Convert.ToDecimal(item.BJ);
                    FKAll += Convert.ToDecimal(item.FK);
                    WFMJAll += Convert.ToDecimal(item.WFMJ);
                    MSSWAll += Convert.ToDecimal(item.MSSW);
                    MSWFSRAll += Convert.ToDecimal(item.MSWFSR);
                    OZLTZJSAll += Convert.ToDecimal(item.OZLTZJS);
                    ODDFYAll += Convert.ToDecimal(item.ODDFY);
                    OYSAll += Convert.ToDecimal(item.OYS);
                    OZZTZAll += Convert.ToDecimal(item.OZZTZ);
                    OSQFYZXSQAll += Convert.ToDecimal(item.OSQFYZXSQ);
                    OSQFYZXZJAll += Convert.ToDecimal(item.OSQFYZXZJ);
                    OBQSZFZCALLAll += Convert.ToDecimal(item.OBQSZFZCALL);
                    OBQSZFZCZJAll += Convert.ToDecimal(item.OBQSZFZCZJ);
                    OXZFYSSAll += Convert.ToDecimal(item.OXZFYSS);
                    #endregion
                    #region 拼接表格
                    string tdId = "td_" + item.UNITID.ToString() + "_" + data.ToString("yyyyMM") + "_";
                    sbMes.Append("<tr style='text-align:center;vertical-align: middle'>");
                    sbMes.Append("<td style='line-height:16px;'>" + UnitBLL.GetUnitNameByUnitID(Convert.ToDecimal(item.UNITID)) + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.AYXCFX + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.AYOTHER + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.LA + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.BJ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.FK + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.WFMJ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.MSSW + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.MSWFSR + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OZLTZJS + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.ODDFY + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OYS + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OZZTZ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OSQFYZXSQ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OSQFYZXZJ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OBQSZFZCALL + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OBQSZFZCZJ + "</td>");
                    sbMes.Append("<td style='line-height:16px;'>" + item.OXZFYSS + "</td>");
                    sbMes.Append("</tr>");
                    #endregion
                }

                #region 总结

                sbMes.Append("<tr style='text-align:center;vertical-align: middle;background:#999'>");
                sbMes.Append("<td>合计</td>");
                sbMes.Append("<td>" + AYXCFXAll + "</td>");
                sbMes.Append("<td>" + AYOTHERAll + "</td>");
                sbMes.Append("<td>" + LAAll + "</td>");
                sbMes.Append("<td>" + BJAll + "</td>");
                sbMes.Append("<td>" + FKAll + "</td>");
                sbMes.Append("<td>" + WFMJAll + "</td>");
                sbMes.Append("<td>" + MSSWAll + "</td>");
                sbMes.Append("<td>" + MSWFSRAll + "</td>");
                sbMes.Append("<td>" + OZLTZJSAll + "</td>");
                sbMes.Append("<td>" + ODDFYAll + "</td>");
                sbMes.Append("<td>" + OYSAll + "</td>");
                sbMes.Append("<td>" + OZZTZAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXSQAll + "</td>");
                sbMes.Append("<td>" + OSQFYZXZJAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCALLAll + "</td>");
                sbMes.Append("<td>" + OBQSZFZCZJAll + "</td>");
                sbMes.Append("<td>" + OXZFYSSAll + "</td>");
                sbMes.Append("</tr>");

                #endregion

            }
            ViewBag.UNITID = unitid;
            ViewBag.CreateUser = CreateUser;
            ViewBag.CheckUser = CheckUser;
            ViewBag.TJTIME = data.ToString("yyyy年MM月");
            ViewBag.CXGHGLZFTJ = sbMes.ToString();
            return View(THIS_VIEW_PATH + "GHZFPrintMe.cshtml");
        }

        /// <summary>
        /// 信访事项月报
        /// </summary>
        /// <returns></returns>
        public ActionResult GGFWChart()
        {
            StringBuilder sbMes = new StringBuilder();

            sbMes.Append(" <select id=\"year\" >");

            for (int i = 0; i < 2015; i++)
            {
                sbMes.Append(" <option value='" + i + "'>" + i + "</option>");
            }
            sbMes.Append("  </select>");
            ViewBag.getYear = sbMes;
            return View(THIS_VIEW_PATH + "GGFWChart.cshtml");
        }

        /// <summary>
        /// 导入信访事项月报
        /// 显示月报列表
        /// </summary>
        public string GetGGFWReport()
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            DateTime firstdate = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            DateTime lastdate = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).AddDays(-1);
            IList<GGFWMONTHLYREPORT> list = new GGFWMONTHLYREPORTSBLL().getListByTime(firstdate).ToList();
            if (list.Count == 0)
            {
                ReportStatisticsBLL.GetGGFWChart(firstdate, lastdate);

            }
            string GGFWMes = GetGGFWChart(firstdate);
            return GGFWMes;

        }

        /// <summary>
        /// 公共服务报表
        /// </summary>
        /// <param name="firstdate"></param>
        /// <returns></returns>
        public string GetGGFWChart(DateTime firstdate)
        {
            IList<GGFWReport> list = ReportStatisticsBLL.GetGGFWRChart(firstdate);
            StringBuilder sbMes = new StringBuilder();
            #region 拼接表头
            sbMes.Append("<div style=\"text-align:center\"><span style=\"font-weight:bold;font-size:xx-large;\">台州市城市管理行政执法信访事项办理月报表</span></div>");
            sbMes.Append("<div><span> 填表单位（盖章）：</span><span style=\"float:right\">" + firstdate.ToString("yyyy年MM月") + "</span></div>");
            sbMes.Append("<table class=\"table table-bordered table-striped table-hover fill-head\" id=\"GeographicalSpace\">");
            sbMes.Append("<thead>");
            sbMes.Append("<tr> <th style=\"text-align: center;\" rowspan=\"2\">信访来源</th><th style=\"text-align: center;\" colspan=\"7\">办理情况</th><th style=\"text-align: center;\" colspan=\"10\">案件分类</th></tr>");
            sbMes.Append(" <tr>");
            sbMes.Append(" <th style=\"text-align: center;\">上月转结</th>");
            sbMes.Append(" <th style=\"text-align: center;\">本月受理</th>");
            sbMes.Append(" <th style=\"text-align: center;\">本月办结</th>");
            sbMes.Append(" <th style=\"text-align: center;\">转结下月</th>");
            sbMes.Append(" <th style=\"text-align: center;\">未按时办理</th>");
            sbMes.Append(" <th style=\"text-align: center;\">催办数量</th>");
            sbMes.Append(" <th style=\"text-align: center;\">督办数量</th>");
            sbMes.Append(" <th style=\"text-align: center;\">市容环卫</th>");
            sbMes.Append(" <th style=\"text-align: center;\">城乡规划</th>");
            sbMes.Append(" <th style=\"text-align: center;\">城市绿化</th>");
            sbMes.Append(" <th style=\"text-align: center;\">市政公用</th>");
            sbMes.Append(" <th style=\"text-align: center;\">环境保护</th>");
            sbMes.Append(" <th style=\"text-align: center;\">工商行政</th>");
            sbMes.Append(" <th style=\"text-align: center;\">公安交通</th>");
            sbMes.Append(" <th style=\"text-align: center;\">城市河道</th>");
            sbMes.Append(" <th style=\"text-align: center;\">违纪举报</th>");
            sbMes.Append(" <th style=\"text-align: center;\">其他</th>");
            sbMes.Append(" </tr> </thead>");
            #endregion
            #region 所需参数
            int SYZJnum = 0;
            int Byslnum = 0;
            int Bybjnum = 0;
            int Jzxynum = 0;
            int Wasblnum = 0;
            int Cbslnum = 0;
            int Dbslnum = 0;
            int Srhwnum = 0;
            int Cxghnum = 0;
            int Cslhnum = 0;
            int Szgynum = 0;
            int Hjbhnum = 0;
            int Gsxznum = 0;
            int GAJTnum = 0;
            int CSHDnum = 0;
            int WJJBnum = 0;
            int QTnum = 0;
            #endregion

            if (list.Count > 0)
            {
                foreach (var item in list)
                {

                    #region 数据同步
                    string Syzj = string.IsNullOrEmpty(item.Syzj) ? "0" : item.Syzj;
                    string Bysl = string.IsNullOrEmpty(item.Bysl) ? "0" : item.Bysl;
                    string Bybj = string.IsNullOrEmpty(item.Bybj) ? "0" : item.Bybj;
                    string Jzxy = string.IsNullOrEmpty(item.Jzxy) ? "0" : item.Jzxy;
                    string Wasbl = string.IsNullOrEmpty(item.Wasbl) ? "0" : item.Wasbl;
                    string Cbsl = string.IsNullOrEmpty(item.Cbsl) ? "0" : item.Cbsl;
                    string Dbsl = string.IsNullOrEmpty(item.Dbsl) ? "0" : item.Dbsl;
                    string Srhw = string.IsNullOrEmpty(item.Srhw) ? "0" : item.Srhw;
                    string Cxgh = string.IsNullOrEmpty(item.Cxgh) ? "0" : item.Cxgh;
                    string Cslh = string.IsNullOrEmpty(item.Cslh) ? "0" : item.Cslh;
                    string Szgy = string.IsNullOrEmpty(item.Szgy) ? "0" : item.Szgy;
                    string Hjbh = string.IsNullOrEmpty(item.Hjbh) ? "0" : item.Hjbh;
                    string Gsxz = string.IsNullOrEmpty(item.Gsxz) ? "0" : item.Gsxz;
                    string GAJT = string.IsNullOrEmpty(item.GAJT) ? "0" : item.GAJT;
                    string CSHD = string.IsNullOrEmpty(item.CSHD) ? "0" : item.CSHD;
                    string WJJB = string.IsNullOrEmpty(item.WJJB) ? "0" : item.WJJB;
                    string QT = string.IsNullOrEmpty(item.QT) ? "0" : item.QT;
                    #endregion

                    #region 合计计算
                    SYZJnum += int.Parse(Syzj);
                    Byslnum += int.Parse(Bysl);
                    Bybjnum += int.Parse(Bybj);
                    Jzxynum += int.Parse(Jzxy);
                    Wasblnum += int.Parse(Wasbl);
                    Cbslnum += int.Parse(Cbsl);
                    Dbslnum += int.Parse(Dbsl);
                    Srhwnum += int.Parse(Srhw);
                    Cxghnum += int.Parse(Cxgh);
                    Cslhnum += int.Parse(Cslh);
                    Szgynum += int.Parse(Szgy);
                    Hjbhnum += int.Parse(Hjbh);
                    Gsxznum += int.Parse(Gsxz);
                    GAJTnum += int.Parse(GAJT);
                    CSHDnum += int.Parse(CSHD);
                    WJJBnum += int.Parse(WJJB);
                    QTnum += int.Parse(QT);

                    #endregion

                    #region 拼接字符串
                    sbMes.Append("<tr>");
                    sbMes.Append("<th style=\"text-align: center;\">" + item.Sname + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Syzj + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Bysl + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Bybj + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Jzxy + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Wasbl + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Cbsl + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Dbsl + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Srhw + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Cxgh + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Cslh + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Szgy + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Hjbh + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + Gsxz + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + GAJT + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + CSHD + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + WJJB + "</th>");
                    sbMes.Append("<th style=\"text-align: center;\">" + QT + "</th>");
                    sbMes.Append("</tr>");
                    #endregion


                }
                #region 合计数据
                sbMes.Append("<tr>");
                sbMes.Append("<th style=\"text-align: center;\">合计</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + SYZJnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Byslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Bybjnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Jzxynum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Wasblnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Cbslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Dbslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Srhwnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Cxghnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Cslhnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Szgynum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Hjbhnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + Gsxznum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + GAJTnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + CSHDnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + WJJBnum + "</th>");
                sbMes.Append("<th style=\"text-align: center;\">" + QTnum + "</th>");
                sbMes.Append("</tr>");
                #endregion

            }
            else
            {
                sbMes.Append("<tr>");
                sbMes.Append("<td colspan='18'  align='center' style='color:red'>暂无数据</td>");
                sbMes.Append("</tr>");
            }
            sbMes.Append("</table>");
            return sbMes.ToString();
        }
        /// <summary>
        ///公共服务 获取月份时间
        /// </summary>
        /// <returns></returns>
        public string GetMouth()
        {
            return GetMouthMes();
        }
        /// <summary>
        ///公共服务 获取年份时间
        /// </summary>
        /// <returns></returns>
        public string GetYear()
        {
            return GetYearMes();
        }

        /// <summary>
        /// 公共服务 查询报表结果
        /// </summary>
        /// <returns></returns>
        public string GetTableMes()
        {
            string year = Request["year"];
            string mouth = Request["mouth"];
            int Year = 0;
            int Mouth = 0;
            int.TryParse(year, out Year);
            int.TryParse(mouth, out Mouth);
            DateTime date = new DateTime(Year, Mouth, 1);
            return GetGGFWChart(date);
        }
        /// <summary>
        ///公共服务 获取开始时间（月份）
        /// </summary>
        /// <param name="hours">选中的时间</param>
        private string GetMouthMes()
        {
            StringBuilder sbMes = new StringBuilder();
            for (int i = 1; i < 13; i++)
            {
                string value = i.ToString();

                sbMes.Append("<option value=\"" + i + "\">" + value + "</option>");


            }
            return sbMes.ToString();
        }
        /// <summary>
        ///公共服务 获取年份时间
        /// </summary>
        /// <returns></returns>
        private string GetYearMes()
        {
            StringBuilder sbMes = new StringBuilder();
            for (int i = 2014; i < 2017; i++)
            {
                string value = i.ToString();

                sbMes.Append("<option value=\"" + i + "\">" + value + "</option>");


            }
            return sbMes.ToString();
        }

        /// <summary>
        /// 公共服务 查询报表结果打印
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTableMesPrintMe()
        {
            DateTime data;
            //获取传过来的时间
            if (!DateTime.TryParse(Request["data"], out data))
            {
                data = DateTime.Now.Date;
            }
            ViewBag.SbMes = GetGGFWChartPrintMe(data);
            return View(THIS_VIEW_PATH + "GGFWPrintMe.cshtml");
        }

        /// <summary>
        /// 公共服务 数据同步结果打印
        /// </summary>
        public ActionResult GetGGFWReportPrintMe()
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            DateTime firstdate = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            DateTime lastdate = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).AddDays(-1);
            IList<GGFWMONTHLYREPORT> list = new GGFWMONTHLYREPORTSBLL().getListByTime(firstdate).ToList();
            if (list.Count == 0)
            {
                ReportStatisticsBLL.GetGGFWChart(firstdate, lastdate);

            }
            ViewBag.SbMes = GetGGFWChartPrintMe(firstdate);
            return View(THIS_VIEW_PATH + "GGFWPrintMe.cshtml");
        }

        /// <summary>
        /// 公共服务 打印报表格式
        /// </summary>
        /// <param name="firstdate"></param>
        /// <returns></returns>
        public string GetGGFWChartPrintMe(DateTime firstdate)
        {
            IList<GGFWReport> list = ReportStatisticsBLL.GetGGFWRChart(firstdate);
            StringBuilder sbMes = new StringBuilder();
            #region 拼接表头
            sbMes.Append("<div style=\"text-align:center;width:1200px;\"><span style=\"font-weight:bold;font-size:xx-large;\">台州市城市管理行政执法信访事项办理月报表</span></div>");
            sbMes.Append("<div style=\" margin-top:20px; padding-left: 10px; width:1190px;\"><span> 填表单位（盖章）：</span><span style=\"float:right\">" + firstdate.ToString("yyyy年MM月") + "</span></div>");
            sbMes.Append("<table class=\"table table-bordered table-striped table-hover fill-head\" id=\"GeographicalSpace\"style=\"width:1200px;\" >");
            sbMes.Append("<thead>");
            sbMes.Append("<tr> <th style=\"text-align: center; width: 90px; line-height:60px;\" rowspan=\"2\" >信访来源</th><th style=\"text-align: center;\" colspan=\"7\">办理情况</th><th style=\"text-align: center;\" colspan=\"10\">案件分类</th></tr>");
            sbMes.Append(" <tr>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">上月<br/>转结</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">本月<br/>受理</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">本月<br/>办结</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">转结<br/>下月</th>");
            sbMes.Append(" <th style=\"text-align: center; width:60px;\">未按时<br/>办理</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">催办<br/>数量</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">督办<br/>数量</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">市容<br/>环卫</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">城乡<br/>规划</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">城市<br/>绿化</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">环境<br/>保护</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">市政<br/>公用</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">工商<br/>行政</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">公安<br/>交通</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">城市<br/>河道</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px;\">违纪<br/>举报</th>");
            sbMes.Append(" <th style=\"text-align: center; width:38px; line-height:30px;\">其他</th>");
            sbMes.Append(" </tr> </thead>");
            #endregion

            #region 所需参数
            int SYZJnum = 0;
            int Byslnum = 0;
            int Bybjnum = 0;
            int Jzxynum = 0;
            int Wasblnum = 0;
            int Cbslnum = 0;
            int Dbslnum = 0;
            int Srhwnum = 0;
            int Cxghnum = 0;
            int Cslhnum = 0;
            int Szgynum = 0;
            int Hjbhnum = 0;
            int Gsxznum = 0;
            int GAJTnum = 0;
            int CSHDnum = 0;
            int WJJBnum = 0;
            int QTnum = 0;
            #endregion

            if (list.Count > 0)
            {
                foreach (var item in list)
                {

                    #region 数据同步
                    string Syzj = string.IsNullOrEmpty(item.Syzj) ? "0" : item.Syzj;
                    string Bysl = string.IsNullOrEmpty(item.Bysl) ? "0" : item.Bysl;
                    string Bybj = string.IsNullOrEmpty(item.Bybj) ? "0" : item.Bybj;
                    string Jzxy = string.IsNullOrEmpty(item.Jzxy) ? "0" : item.Jzxy;
                    string Wasbl = string.IsNullOrEmpty(item.Wasbl) ? "0" : item.Wasbl;
                    string Cbsl = string.IsNullOrEmpty(item.Cbsl) ? "0" : item.Cbsl;
                    string Dbsl = string.IsNullOrEmpty(item.Dbsl) ? "0" : item.Dbsl;
                    string Srhw = string.IsNullOrEmpty(item.Srhw) ? "0" : item.Srhw;
                    string Cxgh = string.IsNullOrEmpty(item.Cxgh) ? "0" : item.Cxgh;
                    string Cslh = string.IsNullOrEmpty(item.Cslh) ? "0" : item.Cslh;
                    string Szgy = string.IsNullOrEmpty(item.Szgy) ? "0" : item.Szgy;
                    string Hjbh = string.IsNullOrEmpty(item.Hjbh) ? "0" : item.Hjbh;
                    string Gsxz = string.IsNullOrEmpty(item.Gsxz) ? "0" : item.Gsxz;
                    string GAJT = string.IsNullOrEmpty(item.GAJT) ? "0" : item.GAJT;
                    string CSHD = string.IsNullOrEmpty(item.CSHD) ? "0" : item.CSHD;
                    string WJJB = string.IsNullOrEmpty(item.WJJB) ? "0" : item.WJJB;
                    string QT = string.IsNullOrEmpty(item.QT) ? "0" : item.QT;
                    #endregion

                    #region 合计计算
                    SYZJnum += int.Parse(Syzj);
                    Byslnum += int.Parse(Bysl);
                    Bybjnum += int.Parse(Bybj);
                    Jzxynum += int.Parse(Jzxy);
                    Wasblnum += int.Parse(Wasbl);
                    Cbslnum += int.Parse(Cbsl);
                    Dbslnum += int.Parse(Dbsl);
                    Srhwnum += int.Parse(Srhw);
                    Cxghnum += int.Parse(Cxgh);
                    Cslhnum += int.Parse(Cslh);
                    Szgynum += int.Parse(Szgy);
                    Hjbhnum += int.Parse(Hjbh);
                    Gsxznum += int.Parse(Gsxz);
                    GAJTnum += int.Parse(GAJT);
                    CSHDnum += int.Parse(CSHD);
                    WJJBnum += int.Parse(WJJB);
                    QTnum += int.Parse(QT);

                    #endregion


                    #region 拼接字符串
                    sbMes.Append("<tr>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + item.Sname + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Syzj + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Bysl + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Bybj + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Jzxy + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Wasbl + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Cbsl + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Dbsl + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Srhw + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Cxgh + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Cslh + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Szgy + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Hjbh + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + Gsxz + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + GAJT + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + CSHD + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + WJJB + "</th>");
                    sbMes.Append("<th style=\"text-align: center; line-height: 14px;\">" + QT + "</th>");
                    sbMes.Append("</tr>");
                    #endregion

                }

                #region 合计数据
                sbMes.Append("<tr>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">合计</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + SYZJnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Byslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Bybjnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Jzxynum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Wasblnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Cbslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Dbslnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Srhwnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Cxghnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Cslhnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Szgynum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Hjbhnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + Gsxznum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + GAJTnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + CSHDnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + WJJBnum + "</th>");
                sbMes.Append("<th style=\"text-align: center; line-height: 16px;\">" + QTnum + "</th>");
                sbMes.Append("</tr>");
                #endregion

            }
            else
            {
                sbMes.Append("<tr>");
                sbMes.Append("<td colspan='18'  align='center' style='color:red'>暂无数据</td>");
                sbMes.Append("</tr>");
            }
            sbMes.Append("</table>");
            return sbMes.ToString();
        }

        /// <summary>
        /// 规划执法添加数据  页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GHZFChartAdd()
        {
            decimal unitid = 0;
            #region 获取登录用户的大队或者所在区局编号
            List<UNIT> unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == SessionManager.User.UnitID).ToList();
            if (unitList != null && unitList.Count > 0)
            {
                if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                {
                    unitid = unitList[0].UNITID;
                }
                else
                {
                    //获取父级是否为大队类型，如果不是大队类型，则不需要修改功能
                    unitList = UnitBLL.GetAllUnits().Where(a => a.UNITID == unitList[0].PARENTID).ToList();
                    if (unitList != null && unitList.Count > 0)
                    {
                        if (unitList[0].UNITTYPEID == 2 || unitList[0].UNITTYPEID == 4)//说明是区局或者是大队
                        {
                            unitid = unitList[0].UNITID;
                        }
                    }
                }
            }
            #endregion

            int GHZFYear;
            int.TryParse(Request["GHZFYear"], out GHZFYear);
            int GHZFMonth;
            int.TryParse(Request["GHZFMonth"], out GHZFMonth);
            DateTime dt = DateTime.Now;
            if (GHZFYear == 0)
                GHZFYear = dt.Year;
            if (GHZFMonth == 0)
                GHZFMonth = dt.Month;

            ViewBag.GHZFYear = GHZFYear;
            ViewBag.GHZFMonth = GHZFMonth;
            ViewBag.UNITID = unitid;

            TJGHZFBLL tjghzfBll = new TJGHZFBLL();
            string ymStr = GHZFYear + "-" + GHZFMonth + "-1";
            DateTime ymDtC = Convert.ToDateTime(ymStr);
            DateTime ymDtN = ymDtC.AddMonths(1);
            TJGHZF model = tjghzfBll.List()
                .Where(a => a.TJTIME >= ymDtC && a.TJTIME < ymDtN && a.UNITID == unitid).ToList().FirstOrDefault();
            if (model == null)
            {
                if (unitid != 0)
                {
                    DateTime dtOne = Convert.ToDateTime("2015-1-01");
                    DateTime dtTwo = dtOne.AddMonths(1);
                    IList<TJGHZF> list = tjghzfBll.List()
                        .Where(a => a.TJTIME >= dtOne && a.TJTIME < dtTwo).ToList();
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i].TJTIME = ymDtC;
                            tjghzfBll.AddTJGHZF(list[i]);
                        }
                    }
                }
                model = new TJGHZF();
            }

            return View(THIS_VIEW_PATH + "GHZFChartAdd.cshtml", model);
        }
        /// <summary>
        /// 规划执法添加数据 功能
        /// </summary>
        /// <param name="model"></param>
        public void AddGHZF(TJGHZF model)
        {
            int GHZFYear;
            int.TryParse(Request["GHZFYear"], out GHZFYear);
            int GHZFMonth;
            int.TryParse(Request["GHZFMonth"], out GHZFMonth);
            string ymStr = GHZFYear + "-" + GHZFMonth + "-1";
            DateTime ymDtC = Convert.ToDateTime(ymStr);
            DateTime ymDtN = ymDtC.AddMonths(1);
            TJGHZFBLL tjghzfBll = new TJGHZFBLL();
            TJGHZF modelSel = tjghzfBll.List()
                .Where(a => a.TJTIME >= ymDtC && a.TJTIME < ymDtN && a.UNITID == model.UNITID).ToList().FirstOrDefault();
            if (modelSel != null)
            {
                model.TJTIME = ymDtC;
                model.ID = modelSel.ID;
                model.CREATETIME = DateTime.Now;
                tjghzfBll.UpdateTJGHZF(model);
            }
            Response.Redirect("/Statistics/GHZFChart?data=" + ymStr);
        }

    }
}
