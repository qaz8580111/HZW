using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZHCGDealModel
    {
        /// <summary>
        /// 案件编号
        /// </summary>
        public string TaskNum { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime DealTime { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 处理部门编号
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 处理部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 图片集合
        /// </summary>
        public List<ZHCGMedia> MediaList { get; set; }
        //整个JSON字符串
        public string RequestData { get; set; }

    }
}
