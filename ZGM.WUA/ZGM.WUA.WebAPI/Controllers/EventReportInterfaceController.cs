using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class EventReportInterfaceController : Controller
    {
        //
        // GET: /EventReportInterface/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 各类事件趋势图（图2）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public List<Trend> GetTrendList(string NYR)
        {
            List<Trend> list = EventReportInterfaceBLL.GetTrendList(NYR).ToList();
            return list;
        }

        /// <summary>
        /// 事件难热点图（图1）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public List<HardHeatMap> GetHardHeatMapList(string NYR)
        {
            List<HardHeatMap> list = EventReportInterfaceBLL.GetHardHeatMapList(NYR).ToList();
            return list;
        }
        /// <summary>
        /// 事件难热点图（图3）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public List<SourceAnalysis> GetSourceAnalysisList(string NYR)
        {
            List<SourceAnalysis> list = EventReportInterfaceBLL.GetSourceAnalysisList(NYR).ToList();
            return list;
        }
        /// <summary>
        /// 事件趋势图（图5）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public List<EventTrends> GetEventTrendsList(string NYR)
        {
            List<EventTrends> list = EventReportInterfaceBLL.GetEventTrendsList(NYR).ToList();
            return list;
        }
    }
}
