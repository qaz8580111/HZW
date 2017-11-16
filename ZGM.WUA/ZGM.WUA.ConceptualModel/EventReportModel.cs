using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    //事件上报报表统计
    public class EventReportModel
    {
        [Key]
        public decimal? EventSourceId { get; set; }
        public decimal? EventCounts { get; set; }
        //public string? EventSourceName { get; set; }
        //public string? EventClassId { get; set; }
        //public string? EventClassName { get; set; }
        public DateTime? EventFoundDay { get; set; }
    }

    /// <summary>
    /// 图2 各类事件趋势
    /// </summary>
    public class Trend
    {
        [Key]
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
        [Key]
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
        [Key]
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
    public class EventTrends {
        //时间
        public string DAYS { get; set; }
        //事件来源    1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
        public decimal? sourceid { get; set; }
        //事件来源名称
        public string SourceName { get; set; }
        //数量
        public decimal? counts { get; set; }
    }

}
