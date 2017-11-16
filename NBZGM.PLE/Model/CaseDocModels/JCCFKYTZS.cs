using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 解除查封扣押通知书
    /// </summary>
    public class JCCFKYTZS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 查封扣押通知书时间
        /// </summary>
        public DateTime? CFKYTZSSJ { get; set; }

        /// <summary>
        /// 查封扣押通知书编号
        /// </summary>
        public string CFKYTZSBH { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string WPMC { get; set; }

        /// <summary>
        /// 解除查封扣押时间
        /// </summary>
        public DateTime? JCKYCFSJ { get; set; }

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime? LQSJ { get; set; }

        /// <summary>
        /// 领取地点
        /// </summary>
        public string LQDD { get; set; }

        /// <summary>
        /// 落款时间
        /// </summary>
        public DateTime? LKSJ { get; set; }

        /// <summary>
        /// 执法人员1
        /// </summary>
        public string ZFRY1 { get; set; }

        /// <summary>
        /// 执法人员2
        /// </summary>
        public string ZFRY2 { get; set; }
        /// <summary>
        /// 解除物品列表
        /// </summary>
        public List<CFKYWPQD> JCCFKYWPList { get; set; }

        //查封扣押决定书属性
        #region
        ///// <summary>
        ///// 编号
        ///// </summary>
        //public string BH { get; set; }

        ///// <summary>
        ///// 当事人
        ///// </summary>
        //public string DSR { get; set; }

        ///// <summary>
        ///// 查封扣押决定书时间
        ///// </summary>
        //public DateTime? CFKYJDSSJ { get; set; }

        ///// <summary>
        ///// 查封扣押决定书编号
        ///// </summary>
        //public string CFKYJDSBH { get; set; }

        ///// <summary>
        ///// 物品名称
        ///// </summary>
        //public string WPMC { get; set; }

        ///// <summary>
        ///// 解除查封扣押时间
        ///// </summary>
        //public DateTime? JCKYCFSJ { get; set; }

        ///// <summary>
        ///// 领取时间
        ///// </summary>
        //public DateTime? LQSJ { get; set; }

        ///// <summary>
        ///// 领取地点
        ///// </summary>
        //public string LQDD { get; set; }

        ///// <summary>
        ///// 落款时间
        ///// </summary>
        //public DateTime? LKSJ { get; set; }

        ///// <summary>
        ///// 解除物品列表
        ///// </summary>
        //public List<CFKYWPQD> JCCFKYWPList { get; set; }

        /// <summary>
        /// 查封扣押物品清单
        /// </summary>
        /// 
        #endregion
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

            /// <summary>
            /// 备注
            /// </summary>
            public string BZ { get; set; }
        }
    }
}
