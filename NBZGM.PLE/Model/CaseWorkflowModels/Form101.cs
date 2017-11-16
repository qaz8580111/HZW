using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 执法队员提出立案建议
    /// </summary>
    public class Form101 : BaseForm
    {
        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 案件来源标识
        /// </summary>
        public decimal AJLYID { get; set; }

        /// <summary>
        /// 案件来源名称
        /// </summary>
        public string AJLYName { get; set; }

        /// <summary>
        /// 是否立案
        /// </summary>
        public string SFLA { get; set; }

        /// <summary>
        /// 是否为重大案件
        /// </summary>
        public string SFWZDAN { get; set; }

        /// <summary>
        /// 当事人类型
        /// </summary>
        public string DSRLX { get; set; }

        /// <summary>
        /// 当事人基本情况(单位)
        /// </summary>
        public OrgForm OrgForm { get; set; }

        /// <summary>
        /// 当事人基本情况(个人)
        /// </summary>
        public PersonForm PersonForm { get; set; }

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
        /// 违法行为
        /// </summary>
        public IllegalForm IllegalForm { get; set; }

        /// <summary>
        /// 立案理由
        /// </summary>
        public string LALY { get; set; }

        /// <summary>
        /// 拟办意见
        /// </summary>
        public string NBYJ { get; set; }

        /// <summary>
        /// 承办单位标识
        /// </summary>
        public decimal? CBDWID { get; set; }

        /// <summary>
        /// 承办单位名称
        /// </summary>
        public string CBDWName { get; set; }

        /// <summary>
        /// 承办领导标识
        /// </summary>
        public decimal? CBLDID { get; set; }

        /// <summary>
        /// 承办领导名称
        /// </summary>
        public string CBLDName { get; set; }
        
        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }

        /// <summary>
        /// 拟办队员编号1
        /// </summary>
        public int NBDYID1 { get; set; }

        /// <summary>
        /// 拟办队员名称1
        /// </summary>
        public string NBDYNAME1 { get; set; }
       
        /// <summary>
        /// 拟办队员编号2
        /// </summary>
        public int NBDYID2 { get; set; }

        /// <summary>
        /// 拟办队员名称2
        /// </summary>
        public string NBDYNAME2 { get; set; }
    }
}