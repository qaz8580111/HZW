using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZGM.Model.CoordinationManager
{
    /// <summary>
    /// 流程待办事件实体
    /// </summary>
   public class WFEventsModel
    {
        /// <summary>
        /// 流程活动实例编号
        /// </summary>
        public string wfsid { get; set; }
        /// <summary>
        /// 流程活动实例名称
        /// </summary>
        public string wfsname { get; set; }
        /// <summary>
        /// 流程活动实例创建时间
        /// </summary>
        public Nullable<System.DateTime> createtime { get; set; }
        /// <summary>
        /// 程活动实例状态
        /// </summary>
        public Nullable<decimal> status { get; set; }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string wfid { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string wfname { get; set; }
        /// <summary>
        /// 流程活动实例创建用户编号
        /// </summary>
        public Nullable<decimal> userid { get; set; }
        /// <summary>
        /// 流程活动实例创建用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 流程活动环节编号
        /// </summary>
        public string wfdid { get; set; }
        /// <summary>
        /// 流程活动环节名称
        /// </summary>
        public string wfdname { get; set; }
        /// <summary>
        /// 环节具体实例编号
        /// </summary>
        public string wfsaid { get; set; }
        /// <summary>
        /// 是否主流程，1：是 2：不是
        /// </summary>
        public Nullable<decimal> ISMAINWF { get; set; }
        /// <summary>
        /// 当前处理人对应表的主键编号
        /// </summary>
        public string wfsuid { get; set; }
        /// <summary>
        /// 是否归档，1：未归档 2：已归档
        /// </summary>
        public int filestatus { get; set; }
    }
}
