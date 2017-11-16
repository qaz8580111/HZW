using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 抽样取证通知书
    /// </summary>
    public class CYQZTZS
    {
        /// <summary>
        /// 抽样取证编号(后台自动生成,允许修改)
        /// </summary>
        public string CYQZBH { get; set; }

        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 违法行为
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 违法的规定
        /// </summary>
        public string WFDGD { get; set; }

        /// <summary>
        /// 抽样物品地址
        /// </summary>
        public string CYWPDZ { get; set; }

        /// <summary>
        /// 抽样取证物品清单集合
        /// </summary>
        public List<CYQZWPQD> CYQZWPQDList { get; set; }
        /// <summary>
        /// 通知时间
        /// </summary>
        public string TZSJ { get; set; }
    }

    /// <summary>
    /// 抽样取证物品清单
    /// </summary>
    public class CYQZWPQD
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
