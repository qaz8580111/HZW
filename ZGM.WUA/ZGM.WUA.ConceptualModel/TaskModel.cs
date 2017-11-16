using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class TaskModel
    {
        [Key]
        public string ZFSJId { get; set; }
        //流程ID
        public string WFid { get; set; }
        public string EventTitle { get; set; }
        //案件来源ID
        public decimal? SourceId { get; set; }
        public string SourceName { get; set; }
        //相关联系人
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public string EventAddress { get; set; }
        public string EventContent { get; set; }
        //大类
        public decimal? BClassId { get; set; }
        public string BClassName { get; set; }
        //小类
        public decimal? SClassId { get; set; }
        public string SClassName { get; set; }
        //发现时间
        public DateTime? FoundTime { get; set; }
        //紧急级别
        public decimal? LevelNum { get; set; }
        //地图位置
        public string Geometry { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }
        public DateTime? CreatetTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        //手机标识
        public string IMEICode { get; set; }
        //是否超期,0：否，1：是
        public decimal? IsOverdue { get; set; }
        public string IsOverdueName { get; set; }
        //超期时长
        public decimal? OverdueLong { get; set; }
        //处理时限
        public decimal? DisposeLimit { get; set; }
        //过期时间
        public DateTime? OverTime { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        //案件编号
        public string EventCode { get; set; }

        //状态
        public string Status { get; set; }

        //当前环节名称
        public string WFName { get; set; }
    }
}
