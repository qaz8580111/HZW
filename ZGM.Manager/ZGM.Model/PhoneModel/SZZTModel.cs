using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public class SZZTModel
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
    }

    public class ZHCGMedia
    {
        //案件编号
        public string TASKNUM { get; set; }
        //类型 默认 3
        public string MEDIATYPE { get; set; }
        //附件数量
        public Nullable<decimal> MEDIANUM { get; set; }
        //附件排序
        public Nullable<decimal> MEDIAORDER { get; set; }
        //附件名称
        public string NAME { get; set; }
        //附件地址
        public string URL { get; set; }
        //附件时间
        public DateTime CREATETIME { get; set; }
        //附件流
        public string IMGCODE { get; set; }
        //是否可用 默认1
        public string ISUSED { get; set; }
    }
}
