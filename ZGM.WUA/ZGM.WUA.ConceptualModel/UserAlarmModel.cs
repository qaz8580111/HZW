using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class UserAlarmModel
    {
        [Key]
        public decimal Id { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        //定位时间
        public DateTime? GPSTime { get; set; }
        //速度
        public decimal? Speed { get; set; }
        //报警开始时间
        public DateTime? StartTime { get; set; }
        //报警结束时间
        public DateTime? EndTime { get; set; }
        //报警类型（1.停留 2.越界 3.离线）
        public decimal? TypeId { get; set; }
        //报警类型名称
        public string TypeName { get; set; }
        //用户ID
        public decimal? UserId { get; set; }
        //处理状态0未处理 1生效 2作废
        public decimal? State { get; set; }
        //处理状态名称
        public string StateName { get; set; }
        //处理内容
        public string Content { get; set; }
        //处理时间
        public DateTime? DealTime { get; set; }
        //处理人标识
        public decimal? DealUserId { get; set; }
        //是否申述(0否1是)
        public decimal? IsAllege { get; set; }
        //申述原因
        public string AllegeReason { get; set; }
        //申述时间
        public DateTime? AllegeTime { get; set; }
    }
}
