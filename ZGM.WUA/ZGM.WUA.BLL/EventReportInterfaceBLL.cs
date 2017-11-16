using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
   public class EventReportInterfaceBLL
    {
       /// <summary>
       /// 各类事件趋势图
       /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
       /// <returns></returns>
       public static IEnumerable<Trend> GetTrendList(string NYR) {

           return EventReportInterfaceDAL.GetTrendList(NYR);
       }
       /// <summary>
       /// 事件难热点图（图1）
       /// </summary>
       /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
       /// <returns></returns>
       public static IEnumerable<HardHeatMap> GetHardHeatMapList(string NYR)
       {
           return EventReportInterfaceDAL.GetHardHeatMapList(NYR);
       }
       /// <summary>
       /// 事件难热点图（图3）
       /// </summary>
       /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
       /// <returns></returns>
       public static IEnumerable<SourceAnalysis> GetSourceAnalysisList(string NYR)
       {
           return EventReportInterfaceDAL.GetSourceAnalysisList(NYR);
       }
       /// <summary>
       /// 事件趋势图（图5）
       /// </summary>
       /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
       /// <returns></returns>
       public static IEnumerable<EventTrends> GetEventTrendsList(string NYR)
       {
           return EventReportInterfaceDAL.GetEventTrendsList(NYR);
       }
    }
}
