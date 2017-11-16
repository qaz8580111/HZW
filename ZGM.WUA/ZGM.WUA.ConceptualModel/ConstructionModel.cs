using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class ConstructionModel
    {
        [Key]
        public decimal? ConstrId { get; set; }
        public string ConstrName { get; set; }
        //工程过程状态标识 1.工程概况 2.工程招标3.工程施工4.工程竣工5.保质期维护
        public string GCGCZT_ID { get; set; }
        //工程过程状态名称
        public string GCGCZT_NAME { get; set; }
        //工程地址
        public string Address { get; set; }
        //工程类型
        public string GCLX_TYPE { get; set; }
        //工程类型名称
        public string GCLX_NAME { get; set; }
        //工程性质
        public string GCXZ_TYPE { get; set; }
        //工程性质名称
        public string GCXZ_NAME { get; set; }
        //预算资金
        public decimal? YSZJ { get; set; }
        //建设内容
        public string JSNR { get; set; }
        //计划开工日期
        public DateTime? JHKGRQ { get; set; }
        //计划竣工日期
        public DateTime? JHJGRQ { get; set; }
        //施工工期
        public decimal? SGGQ { get; set; }
        //工程内容类型标识
        public decimal? GCNRLX_ID { get; set; }
        //填报时间
        public DateTime? TBSJ { get; set; }
        //工程编号
        public string GCBH { get; set; }
        //立项依据
        public string LXYJ { get; set; }
        //立项批准机关
        public string LXPZJG { get; set; }
        //限额标准
        public string XEBZ { get; set; }
    }
}
