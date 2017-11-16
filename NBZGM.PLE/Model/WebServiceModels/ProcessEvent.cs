using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 处理事件
    /// </summary>
    public class ProcessEvent
    {
        /// <summary>
        /// 流程实例标识
        /// </summary>
        public string wiid { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        public int processWayID { get; set; }

        /// <summary>
        /// 查处方式
        /// </summary>
        public int investigateWayID { get; set; }

        /// <summary>
        /// 案卷编号
        /// </summary>
        public string caseCode { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string opinion { get; set; }

        /// <summary>
        /// 处理后照片一
        /// </summary>
        public byte[] processedPhoto1 { get; set; }

        /// <summary>
        /// 处理后照片二
        /// </summary>
        public byte[] processedPhoto2 { get; set; }

        /// <summary>
        /// 处理后照片三
        /// </summary>
        public byte[] processedPhoto3 { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string processTime { get; set; }

        /// <summary>
        /// 执法队员标识
        /// </summary>
        public int userID { get; set; }
    }
}
