using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class AlarmModel
    {
        public decimal Id { get; set; }
        /// <summary>
        /// 报警类型（1.停留 2.越界 3.离线）
        /// </summary>
        public decimal? AlarmTypeId { get; set; }
        public string AlarmTypeName { get; set; }
        /// <summary>
        /// 报警对象
        /// </summary>
        public decimal AlarmMemberId { get; set; }
        public DateTime? AlarmStart { get; set; }
        public DateTime? AlarmEnd { get; set; }
        public DateTime? CrateTime { get; set; }
    }
}
