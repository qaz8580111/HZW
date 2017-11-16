using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
    public class WebServiceXZSPModel
    {
        /// <summary>
        /// 活动标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 流程标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 活动环节标识
        /// </summary>
        public decimal? ADID { get; set; }
        /// <summary>
        /// 文书编号
        /// </summary>
        public string documentCode { get; set; }
        /// <summary>
        /// 受理时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 审批项目
        /// </summary>
        public string project { get; set; }

        /// <summary>
        /// 申请事项
        /// </summary>
        public string projectItem { get; set; }
        /// <summary>
        /// 申请单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string person { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 申报内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 申请工商执照
        /// </summary>
        public string photoPath1 { get; set; }
        /// <summary>
        /// 施工单位营业执照
        /// </summary>
        public string photoPath2 { get; set; }


        /// <summary>
        /// 队员意见
        /// </summary>
        public string DYYJ { get; set; }
        /// <summary>
        /// 第一张图片
        /// </summary>
        public string photoPath4 { get; set; }
        /// <summary>
        /// 第二张图片
        /// </summary>
        public string photoPath5 { get; set; }
        /// <summary>
        /// 第三张图片
        /// </summary>
        public string photoPath6 { get; set; }
    }
}
