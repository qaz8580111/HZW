using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsPumpModel
    {
        [Key]
        public decimal PumpId { get; set; }
        public string PumpName { get; set; }
        public decimal? SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //
        //泵站类型
        public string BZLX_TYPE { get; set; }
        //水泵功率
        public string SBGL { get; set; }
        //发电机功率
        public string FDJGL { get; set; }
        //水泵数量
        public decimal? SBSL { get; set; }
        //出水口管径
        public string CSKGJ { get; set; }
        //出水口位置
        public string CSKWZ { get; set; }
        //单位时间流量
        public string DWSJLL { get; set; }
        //地址详细信息
        public string DZXXXX { get; set; }
        //参考价格
        public string CKJG { get; set; }
        //备注
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
    }
}
