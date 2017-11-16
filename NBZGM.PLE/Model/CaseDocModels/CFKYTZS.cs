using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 查封扣押通知书
    /// </summary>
    public class CFKYTZS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人的名称或姓名
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 违法行为
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 法律法规
        /// </summary>
        public string FVFG { get; set; }

        /// <summary>
        /// 违法地点
        /// </summary>
        public string DD { get; set; }

        /// <summary>
        /// 执法人员1
        /// </summary>
        public string ZFRY1 { get; set; }

        /// <summary>
        /// 执法人员2
        /// </summary>
        public string ZFRY2 { get; set; }

        /// <summary>
        /// 查封扣押物品清单集合
        /// </summary>
        public List<CFKYWPQD> CFKYWPQDList { get; set; }

        /// <summary>
        /// 查封扣押通知时间
        /// </summary>
        public DateTime? CFKYTZSJ { get; set; }
    }

    /// <summary>
    /// 查封扣押物品清单
    /// </summary>
    public class CFKYWPQD
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string XH { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string WPMC { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GG { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 生产日期(批号)
        /// </summary>
        public string SCRQ { get; set; }

        /// <summary>
        /// 生产单位
        /// </summary>
        public string SCDW { get; set; }
    }
}
