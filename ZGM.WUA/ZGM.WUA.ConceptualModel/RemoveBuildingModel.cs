using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingModel
    {
        [Key]
        public decimal ProjectId { get; set; }
        public string ProjectName { get; set; }
        //项目负责人
        public string ProjectUser { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Remarks { get; set; }
        /// <summary>
        /// 项目状态（1正在进行中2已完结3已删除）
        /// </summary>
        public decimal? State { get; set; }
        public string StateName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string Geometry { get; set; }

        //共用
        //权证记载面积
        public decimal? WarrantArea { get; set; }
        //丈量面积
        public decimal? MeasurementArea { get; set; }
        //无证建筑面积
        public decimal? WithoutArea { get; set; }

        //住宅基础
        //标识
        public decimal HouseId { get; set; }
        //户主
        public string HouseHolder { get; set; }
        //户主电话
        public string HolderPhone { get; set; }

        //企业
        //标识
        public decimal EnterpriseId { get; set; }
        //法人代表姓名
        public string LegalName { get; set; }
        //法人代表联系方式
        public string LegalPhone { get; set; }
        //土地面积
        public decimal? LandArea { get; set; }
        //签约时间
        public DateTime? SignTime { get; set; }
        //腾空时间
        public DateTime? EmptyTime { get; set; }
        //总补偿金额
        public decimal? SumMoney { get; set; }
        //所得税补偿金额
        public decimal? Tax { get; set; }
        //支付详情标识
        public decimal OMId { get; set; } 
    }
}
