using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.GGFWDOC
{
    /// <summary>
    /// 信访派遣单
    /// </summary>
    public class XFPQD
    {
        /// <summary>
        /// 投诉人
        /// </summary>
        public string TSR { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public string SJLY { get; set; }

        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime? FXSJ { get; set; }

        /// <summary>
        /// 事件标题
        /// </summary>
        public string SJBT { get; set; }

        /// <summary>
        /// 事件地址
        /// </summary>
        public string SJDZ { get; set; }

        /// <summary>
        /// 问题大类
        /// </summary>
        public string WTDL { get; set; }

        /// <summary>
        /// 问题小类
        /// </summary>
        public string WTXL { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string SJNR { get; set; }

        /// <summary>
        /// 指派意见
        /// </summary>
        public string ZPYJ { get; set; }

        /// <summary>
        /// 指派时间
        /// </summary>
        public DateTime? ZPSJ { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CZR { get; set; }

        /// <summary>
        /// 所属区局
        /// </summary>
        public string SSQJ { get; set; }

        /// <summary>
        /// 所属中队
        /// </summary>
        public string SSZD { get; set; }

        /// <summary>
        /// 派遣意见
        /// </summary>
        public string PQYJ { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string TP { get; set; }
    }
}
