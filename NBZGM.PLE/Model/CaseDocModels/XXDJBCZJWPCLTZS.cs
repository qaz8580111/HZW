using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 先行登记保存证据物品处理通知书
    /// </summary>
    public class XXDJBCZJWPCLTZS
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
        /// 先行登记保存证据通知时间
        /// </summary>
        public DateTime? XXDJBCZJTZSJ { get; set; }

        /// <summary>
        /// 现行登记保存证据编号
        /// </summary>
        public string XXDJBCZJBH { get; set; }


        /// <summary>
        /// 物品名称
        /// </summary>
        public string WPMC { get; set; }

        /// <summary>
        /// 开始保存期限
        /// </summary>
        public DateTime? KSBCSJ { get; set; }

        /// <summary>
        /// 结束保存期限
        /// </summary>
        public DateTime? JSBCSJ { get; set; }

        /// <summary>
        /// 违反的规定
        /// </summary>
        public string WFGD { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string CLJG { get; set; }

        /// <summary>
        /// 先行登记保存证据物品处理时间
        /// </summary>
        public DateTime? XXDJBCZJWPCLSJ { get; set; }

        /// <summary>
        /// 先行登记保存证据物品处理清单集合
        /// </summary>
        public List<XXDJBCZJWPCLQD> XXDJBCZJWPCLQDList { get; set; }


    }

    /// <summary>
    /// 先行登记保存证据物品处理清单
    /// </summary>
    public class XXDJBCZJWPCLQD
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
