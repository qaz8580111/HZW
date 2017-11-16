using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 立案审批表
    /// </summary>
    public class LASPB
    {
        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 案件来源
        /// </summary>
        public string AJLY { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ZZJGDMZBH { get; set; }

        /// <summary>
        /// 法定代表人姓名
        /// </summary>
        public string FDDBRXM { get; set; }

        /// <s  ummary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public string CSNY { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 住所地
        /// </summary>
        public string ZSD { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 案情摘要
        /// </summary>
        public string AQZY { get; set; }

        /// <summary>
        /// 立案理由
        /// </summary>
        public string LALY { get; set; }

        /// <summary>
        /// 拟办意见
        /// </summary>
        public string NBYJ { get; set; }

        /// <summary>
        /// 拟办意见签名
        /// </summary>
        public string NBYJQM { get; set; }

        /// <summary>
        /// 拟办意见签名日期
        /// </summary>
        public string NBYJQMRQ { get; set; }

        /// <summary>
        /// 领导审批意见
        /// </summary>
        public string LDSPYJ { get; set; }

        /// <summary>
        /// 领导是否同意
        /// </summary>
        public bool? Approve { get; set; }

        /// <summary>
        /// 承办人员(如有多个用","分隔)
        /// </summary>
        public string CBRY { get; set; }

        /// <summary>
        /// 领导审批意见签名
        /// </summary>
        public string LDSPYJQM { get; set; }

        /// <summary>
        /// 领导审批意见签名日期
        /// </summary>
        public string LDSPYJQMRQ { get; set; }

        /// <summary>
        /// 法制处审批意见
        /// </summary>
        public string FZCSPYJ { get; set; }

        /// <summary>
        /// 法制处签名日期
        /// </summary>
        public string FZCSPYJQMRQ { get; set; }

        /// <summary>
        /// 法制处办事人编号
        /// </summary>
        public string FZCQM { get; set; }

        /// <summary>
        /// 法制处是否同意
        /// </summary>
        public bool? FZCSFTY { get; set; }

        /// <summary>
        /// 主办队员
        /// </summary>
        public string ZBDY { get; set; }

        /// <summary>
        /// 协办队员
        /// </summary>
        public string XBDY { get; set; }

        /// <summary>
        /// 分管副局长审批意见
        /// </summary>
        public string FGFJZSPYJ { get; set; }

        /// <summary>
        /// 分管副局长是否同意
        /// </summary>
        public bool? FGFJZSFTY { get; set; }

        /// <summary>
        /// 分管副局长审批日期
        /// </summary>
        public string FGFJZSPRQ { get; set; }
        /// <summary>
        /// 分管副局长编号
        /// </summary>
        public string FGLD { get; set; }

        /// <summary>
        /// 拟办队员名称1
        /// </summary>
        public string NBDYNAME1 { get; set; }

        /// <summary>
        /// 拟办队员名称2
        /// </summary>
        public string NBDYNAME2 { get; set; }
    }
}
