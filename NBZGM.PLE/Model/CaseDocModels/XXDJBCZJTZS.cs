using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 先行登记保存证据通知书
    /// </summary>
    public class XXDJBCZJTZS:XZCFSXGZS
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
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 违反的规定
        /// </summary>
        public string WFGD { get; set; }

        /// <summary>
        /// 保存开始时间
        /// </summary>
        public DateTime? BCKSSJ { get; set; }

        /// <summary>
        /// 保存结束时间
        /// </summary>
        public DateTime? BCJSSJ { get; set; }

        /// <summary>
        /// 存放方式
        /// </summary>
        public string CFFS { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        public string CFDD { get; set; }

        /// <summary>
        /// 文书落款时间
        /// </summary>
        public DateTime? WSLKSJ { get; set; }

        /// <summary>
        /// 先行登记保存证据清单集合
        /// </summary>
        public List<XXDJBCZJQD> XXDJBCZJQDList { get; set; }
    }

    /// <summary>
    /// 先行登记保存证据清单
    /// </summary>
    public class XXDJBCZJQD
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 品级
        /// </summary>
        public string PJ { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GG { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string XH { get; set; }

        /// <summary>
        /// 形态
        /// </summary>
        public string XT { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
