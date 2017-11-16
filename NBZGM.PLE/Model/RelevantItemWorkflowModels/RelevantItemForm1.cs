using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Taizhou.PLE.Model.RelevantItemWorkflowModels
{
    /// <summary>
    /// 主办队员提出相关事项审批表
    /// </summary>
    public class RelevantItemForm1 : BaseForm
    {
        /// <summary>
        /// 申请事项
        /// </summary>
        public string SQSX { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 立案日期
        /// </summary>
        public DateTime LARQ { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string ZY { get; set; }

        /// <summary>
        /// 名族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 法定代表人
        /// </summary>
        public string FDDBR { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string ZZ { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string YZBM { get; set; }

        /// <summary>
        /// 简要案情及申请理由依据和内容
        /// </summary>
        public string JYAQ { get; set; }

        /// <summary>
        /// 承办人意见
        /// </summary>
        public string CBRYJ { get; set; }

        /// <summary>
        /// 承办单位标识
        /// </summary>
        public decimal CBDWID { get; set; }

        /// <summary>
        /// 承办单位名称
        /// </summary>
        public string CBDWName { get; set; }

        /// <summary>
        /// 承办领导标识
        /// </summary>
        public decimal CBLDID { get; set; }

        /// <summary>
        /// 承办领导名称
        /// </summary>
        public string CBLDName { get; set; }
    }
}
