using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsStreetLampModel
    {
        [Key]
        public decimal SLId { get; set; }
        //基础路灯编号
        public string JCLDBH { get; set; }
        //高度
        public decimal? GD { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public string DLTX { get; set; }
        //三维模型标识
        public decimal? SWMX_ID { get; set; }
        //是否有效
        public string SFYX { get; set; }
        //创建时间
        public DateTime? CJSJ { get; set; }
    }
}
