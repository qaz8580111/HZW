using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class IllegalBuildingModel
    {
        [Key]
        public string IBId { get; set; }
        public string IBUnitName { get; set; }
        //状态（1已拆2未拆）
        public string State { get; set; }
        // 个人或法人代表身份  身份表外键
        public decimal? IdentityId { get; set; }
        public string IdentityName { get; set; }
        //片区标识
        public decimal? ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public DateTime? WJTime { get; set; }
        //违建类别（1新建2扩建）
        public string WJType { get; set; }
        //违建结构（1框架2简易）
        public string WJFame { get; set; }
        public decimal? WJFloor { get; set; }
        //土地类别（1建设用地2耕地）
        public string LandType { get; set; }
        //违建用途（1住宅2商用3厂房）
        public string WJUse { get; set; }
        //占地面积
        public decimal? LandArea { get; set; }
        //建筑面积
        public decimal? BuildingArea { get; set; }
        //拆除时间
        public DateTime? RemoveTime { get; set; }
        //拆除面积
        public decimal? RemoveArea { get; set; }
        //拆后清理复垦绿化建设项目(1清理2复垦3绿化)
        public string ConstructionProject { get; set; }
        //六类先拆情况（1顶风的2“四边”违法的 3妨碍重点工程的 4 存在安全隐患的 5严重影响城乡规划实施的 5侵占耕地及卫片发现的 ）
        public string SixCase { get; set; }
        //整改通知发放情况（1是2否）
        public string RectificationCase { get; set; }
        //整改通知发放时间
        public DateTime? RectificationTime { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }

        public DateTime? CreateTime { get; set; }
        public decimal? CreateUser { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
    }
}
