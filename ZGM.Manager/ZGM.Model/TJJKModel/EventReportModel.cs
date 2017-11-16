using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.TJJKModel
{
   public class EventReportModel
    {
    }

   /// <summary>
   /// 图2 各类事件趋势
   /// </summary>
   public class Trend
   {
       //上报数
       public decimal? NumberOfReported { get; set; }
       //结案数
       public decimal? CasesSettled { get; set; }
       //事件来源    1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
       public decimal? Source { get; set; }
       //事件来源名称
       public string sourcename { get; set; }
   }


   /// <summary>
   /// 图1 事件难热点问题统计 
   /// </summary>
   public class HardHeatMap
   {
       //数量
       public decimal? zfsj_Count { get; set; }
       //大类名称
       public string BClassName { get; set; }
       //事件来源    1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
       public decimal? Source { get; set; }
       //事件来源名称
       public string SourceName { get; set; }
   }

   /// <summary>
   /// 图3 事件来源分析
   /// </summary>
   public class SourceAnalysis
   {
       //数量
       public decimal? zfsj_Count { get; set; }
       // 事件来源   1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
       public decimal? Source { get; set; }
       //事件来源名称
       public string SourceName { get; set; }
   }
   /// <summary>
   /// 图5 事件趋势图
   /// </summary>
   public class EventTrends
   {
       //时间
       public DateTime? DAYS { get; set; }
       //事件来源    1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
       public decimal? Source { get; set; }
       //事件来源名称
       public string SourceName { get; set; }
       //数量
       public decimal? zfsj_Count { get; set; }
   }
}
