using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZHCGDisposal
    {
        /// <summary>
        /// 任务号
        /// </summary>
        public string TaskNum { get; set; }
        /// <summary>
        /// 类型(3:结案;4:回退;5:申请延期;6:申请挂账)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 申请内容
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 申请理由
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 处置部门
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
