using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsBridgeModel
    {
        [Key]
        public decimal BridgeId { get; set; }
        public string BridgeName { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public string DLTX { get; set; }
        //三维模型标识
        public decimal? SWMX_ID { get; set; }
        //业务属性
        //荷载等级
        public string HZDJ_TYPE { get; set; }
        //桥梁长度（米）
        public decimal? QLCD { get; set; }
        //孔数-跨径
        public string KSKJ { get; set; }
        //车行道宽（米）
        public decimal? CXDK { get; set; }
        //车行道面积（平方米）
        public decimal? CXDMJ { get; set; }
        //桥面铺装材料
        public string QMPZCL_TYPE { get; set; }
        //人行道宽（米）
        public decimal? RXDK { get; set; }
        //人行道面积（平方米）
        public decimal? RXDMJ { get; set; }
        //人行道铺装材料
        public string RXDPZCL_TYPE { get; set; }
        //养护类型
        public string YHLX_TYPE { get; set; }
        //养护等级
        public string YHDJ_TYPE { get; set; }
        //桥面标高（米）
        public decimal? QMBG { get; set; }
        //梁底标高（米）
        public decimal? LDBG { get; set; }
        //常水位（米）
        public decimal? CSW { get; set; }
        //参考价格
        public string CKJG { get; set; }
        //备注
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //改造日期
        public DateTime? GZRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
    }
}
