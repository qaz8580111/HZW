using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 调查询问笔录(除了案由外，其他字段都需在文书表单页面填写)
    /// </summary>
    public class DCXWBL
    {
        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }


        /// <summary>
        /// 调查(询问)开始日期年月日
        /// </summary>
        public string StartDCXWYMD { get; set; }
        /// <summary>
        /// 调查(询问)开始时间
        /// </summary>
        public string StartDCXWSJ { get; set; }

        /// <summary>
        /// 调查(询问)结束时间
        /// </summary>
        public string EndDCXWSJ { get; set; }

        /// <summary>
        /// 调查(询问)地点
        /// </summary>
        public string DCXWDD { get; set; }

        /// <summary>
        /// 被调查(询问)人
        /// </summary>
        public string BDCXWR { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 名族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string SFZHM { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 职务或职业
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string ZZ { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string YB { get; set; }

        /// <summary>
        /// 与本案关系
        /// </summary>
        public string YBAGX { get; set; }

        /// <summary>
        /// 调查(询问)人1
        /// </summary>
        public string DCXWR1 { get; set; }


        /// <summary>
        /// 调查(询问)人2
        /// </summary>
        public string DCXWR2 { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string JLR { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW2 { get; set; }

        /// <summary>
        /// 笔录内容
        /// </summary>
        public string Content { get; set; }
    }
}
