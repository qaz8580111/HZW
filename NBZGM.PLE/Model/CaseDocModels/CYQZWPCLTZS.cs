using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 抽样取证物品处理通知书
    /// </summary>
    public class CYQZWPCLTZS
    {
        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }
        /// <summary>
        /// 被抽样取证人
        /// </summary>
        public string BCYQZR { get; set; }

        /// <summary>
        /// 抽样取证通知时间
        /// </summary>
        public DateTime? CYQZTZSJ { get; set; }

        /// <summary>
        /// 编号(后台自动生成)
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string WPMC { get; set; }

        /// <summary>
        /// 法律、法规、规章依据
        /// </summary>
        public string FVFGGZYJ { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string CLJG { get; set; }

        /// <summary>
        /// 抽样取证物品处理清单集合
        /// </summary>
        public List<CYQZWPCLQD> CYQZWPCLQDList { get; set; }

        /// <summary>
        /// 抽样取证物品处理时间
        /// </summary>
        public DateTime? CYQZWPCLSJ { get; set; }

        /// <summary>
        /// 抽样取证通知书标号
        /// </summary>
        public string CYQZSBH { get; set; }
    }

    /// <summary>
    /// 抽样取证物品处理清单
    /// </summary>
    public class CYQZWPCLQD
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
        /// 处理意见
        /// </summary>
        public string CLYJ { get; set; }
    }
}
