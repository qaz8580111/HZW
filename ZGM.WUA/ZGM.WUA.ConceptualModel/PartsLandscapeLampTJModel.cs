using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsLandscapeLampTJModel
    {
        [Key]
        public decimal LLId { get; set; }
        //灯泡类型    钠灯，投光灯…
        [Key]
        public string DPLX_TYPE { get; set; }
        //功率   250W，150W…
        [Key]
        public string GL_TYPE { get; set; }
        //盏数
        public decimal? ZS { get; set; }
    }
}
