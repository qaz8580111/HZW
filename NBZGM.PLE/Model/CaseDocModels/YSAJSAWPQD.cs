using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 移送案件涉案物品清单
    /// </summary>
    public class YSAJSAWPQD
    {
        /// <summary>
        /// 案件编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string JSR {get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime JSSJ {get;set;}

        /// <summary>
        /// 移送人
        /// </summary>
        public string YSR{get;set;}
        
        /// <summary>
        /// 移送时间
        /// </summary>
        public DateTime YSSJ { get; set;}

        /// <summary>
        /// 移送案件涉案物品清单集合
        /// </summary>
        public List<YSAJSAWPQDLIST> YSAJSAWPQDList { get; set; }
    }

    /// <summary>
    ///  移送案件涉案物品清单列表
    /// </summary>
    public class YSAJSAWPQDLIST 
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
