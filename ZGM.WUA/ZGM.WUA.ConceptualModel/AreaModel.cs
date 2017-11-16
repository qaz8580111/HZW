using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 巡查区域
    /// </summary>
    public class AreaModel
    {
        [Key]
        public decimal AreaId { get; set; }
        public string AreaName { get; set; }
        //区域描述
        public string AreaDescription { get; set; }
        //点集
        public string Geometry { get; set; }
        //所有者类型 1：队员，2：车辆
        public decimal? AreaTypeId { get; set; }
        //颜色,Red,Green
        public string Color { get; set; }
    }
}
