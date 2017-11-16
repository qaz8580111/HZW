using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsStreetLampLoadModel
    {
        [Key]
        public decimal SLLId { get; set; }
        public string SLLName { get; set; }
        //起讫点
        public string QQD { get; set; }
        //杆高
        public decimal? GG { get; set; }
        //控制箱编号
        public string KZXBH { get; set; }
        //控制箱坐落地段
        public string KZXZLDD { get; set; }
        //路灯杆编号
        public string LDGBH { get; set; }
        //杆数
        public decimal? GS { get; set; }
        //参考价格
        public string CKJG { get; set; }
        //备注
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
        //是否逻辑删除（默认为0）
        public decimal? SFLJSC { get; set; }

        public decimal? X { get; set; }
        public decimal? Y { get; set; }
    }
}
