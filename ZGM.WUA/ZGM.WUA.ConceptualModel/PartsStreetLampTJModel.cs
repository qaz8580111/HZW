using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 路灯统计
    /// </summary>
    public class PartsStreetLampTJModel
    {
        [Key]
        public decimal SLLId { get; set; }
        //灯杆类型   单侧单叉，双侧双叉，单侧双叉
        [Key]
        public string DGLX_TYPE { get; set; }
        //灯泡类型  钠灯，LED灯…
        [Key]
        public string DPLX_TYPE { get; set; }
        //功率 110W，150W…
        [Key]
        public string GL_TYPE { get; set; }
        //杆数
        public decimal? GS { get; set; }
    }
}
