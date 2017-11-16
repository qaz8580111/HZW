using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJModels
{
    /// <summary>
    /// 事件上报
    /// </summary>
    public class EventReport
    {
        /// <summary>
        /// 事件编号(自动生成)
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// 事件标题
        /// </summary>
        public string EventTitle { get; set; }

        /// <summary>
        /// 事件地址
        /// </summary>
        public string EventAddress { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public decimal EventSourceID { get; set; }

        /// <summary>
        /// 问题大类标识
        /// </summary>
        public decimal QuestionDLID { get; set; }

        /// <summary>
        /// 问题小类标识
        /// </summary>
        public decimal QuestionXLID { get; set; }

        /// <summary>
        /// 所属区局标识
        /// </summary>
        public decimal SSQJID { get; set; }

        /// <summary>
        /// 所属中队标识
        /// </summary>
        public decimal SSZDID { get; set; }

        /// <summary>
        /// 发现时间，默认为当前时间
        /// </summary>
        public string FXSJ { get; set; }

        /// <summary>
        /// 地图位置
        /// </summary>
        public string DTWZ { get; set; }

        /// <summary>
        /// 上报时间(自动生成)
        /// </summary>
        public string SBSJ { get; set; }

        /// <summary>
        /// 上报队员(自动生成)
        /// </summary>
        public decimal SBDYID { get; set; }

        /// <summary>
        /// 相关联系人
        /// </summary>
        public string XGLXR { get; set; }

        /// <summary>
        /// 相关联系人电话
        /// </summary>
        public string XGLXRDH { get; set; }
    }
}
