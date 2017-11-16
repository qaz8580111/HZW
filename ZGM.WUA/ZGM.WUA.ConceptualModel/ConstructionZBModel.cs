using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 工程招标
    /// </summary>
    public class ConstructionZBModel
    {
        [Key]
        public decimal? ConstrId { get; set; }
        //招标日期
        public DateTime? ZBRQ { get; set; }
        //招标方式
        public string ZBFS_TYPE { get; set; }
        //招标负责人
        public string ZBFZR { get; set; }
        //中标金额
        public decimal? ZBJE { get; set; }
        //中标公司
        public string ZBGS { get; set; }
        //中标负责人
        public string ZBGSFZR { get; set; }
        //中标公司联系电话
        public string ZBGSLXDH { get; set; }
        //监理公司
        public string JLGS { get; set; }
        //监理公司负责人
        public string JLGSFZR { get; set; }
        //监理公司联系电话
        public string JLGSLXDH { get; set; }
        //设计公司
        public string SJGS { get; set; }
        //设计公司负责人
        public string SJGSFZR { get; set; }
        //设计公司联系电话
        public string SJGSLXDH { get; set; }
        //合同签订日期
        public DateTime? HTQDRQ { get; set; }
        //合同金额
        public decimal? HTJE { get; set; }
        //质量要求
        public string ZLYQ { get; set; }
        //保修期限
        public decimal? BXQX { get; set; }
        //工程特殊要求
        public string GCTSYQ { get; set; }
        //填报日期
        public DateTime? TBSJ { get; set; }
        //招标代理人
        public string ZBDLR { get; set; }
        //招标联系人
        public string ZBLXR { get; set; }
        //招标联系电话
        public string ZBLXDH { get; set; }
    }
}
