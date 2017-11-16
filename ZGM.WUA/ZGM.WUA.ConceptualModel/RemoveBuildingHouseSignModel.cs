using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingHouseSignModel
    {
        [Key]
        public decimal SignId { get; set; }
        public decimal? HouseId { get; set; }
        //签协时间
        public DateTime? SignTime { get; set; }
        //可调产房屋面积
        public decimal? HouseArea { get; set; }
        //仓库面积
        public decimal? WareHouseArea { get; set; }
        //划入
        public decimal? WipeIn { get; set; }
        //划出
        public decimal? WipeOut { get; set; }
        //扩户面积
        public decimal? ExpansionArea { get; set; }
        //分户扩户面积
        public decimal? HouseHoldExpansionArea { get; set; }
        //可奖励面积
        public decimal? RewardArea { get; set; }
        //婚领优购面积
        public decimal? MarriageArea { get; set; }
        //原房补偿费
        public decimal? HouseMoney { get; set; }
        //奖励费
        public decimal? RewardMoney { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        //腾空时间
        public DateTime? EmptyTime { get; set; }
    }
}
