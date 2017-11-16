using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class DrawModel
    {
        [Key]
        public decimal ID { get; set; }
        /// <summary>
        /// 绘图类型 Point-点 Polyline-线 Polygon-面 LineArea-线变面（默认扩展十米）
        /// </summary>
        public string Type { get; set; }
        public string Points { get; set; }
        public decimal? UserID { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  Red-1 Blue-2 Green-3 Yellow-4 Borrow(棕色)-5
        /// </summary>
        public int Style { get; set; }
    }
}
