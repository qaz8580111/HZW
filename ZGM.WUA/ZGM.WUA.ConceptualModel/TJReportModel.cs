using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class TJReportModel { }

    //部门队员上报事件模型
    public class UnitReportEventModel
    {
        [Key]
        public decimal? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? ReportEventCount { get; set; }
        public decimal? FinishEventCount { get; set; }

    }

    //片区上报事件模型
    public class ZoneReportEventModel
    {
        [Key]
        public decimal? ZoneId { get; set; }
        public string ZoneName { get; set; }
        public decimal? Count { get; set; }
    }

    //部门队员签到模型
    public class UnitReportSignModel
    {
        [Key]
        public decimal? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? SignCount { get; set; }
        public decimal? UnSignCount { get; set; }
    }

    //部门报警类型模型
    public class UnitReportAlarmModel
    {
        [Key]
        public decimal? OverBorderCount { get; set; }
        public string OverBorderPercent { get; set; }
        public decimal? OverLineCount { get; set; }
        public string OverLinePercent { get; set; }
        public decimal? OverTimeCount { get; set; }
        public string OverTimePercent { get; set; }
    }

    //部门队员巡查模型
    public class UnitReportProwledModel
    {
        [Key]
        public decimal? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? OverBorderCount { get; set; }
        public decimal? OverLineCount { get; set; }
        public decimal? OverTimeCount { get; set; }
    }

    //队员路程模型
    public class ReportWalkModel
    {
        [Key]
        public decimal? UserId { get; set; }
        public string UserName { get; set; }
        public decimal? WalkCount { get; set; }
    }
}
