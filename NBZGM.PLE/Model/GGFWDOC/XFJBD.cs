using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.GGFWDOC
{
    /// <summary>
    /// 信访交办单
    /// </summary>
    public class XFJBD
    {
        /// <summary>
        /// 交办编号
        /// </summary>
        public string JBBH { get; set; }

        /// <summary>
        /// 案件来源
        /// </summary>
        public string AJLY { get; set; }

        /// <summary>
        /// 案件类型
        /// </summary>
        public string AJLX { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? JLSJ { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? QSSJ { get; set; }

        /// <summary>
        /// 应办结时间
        /// </summary>
        public DateTime? YBJSJ { get; set; }

        /// <summary>
        /// 来访人
        /// </summary>
        public string LFR { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string DZ { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string ZT { get; set; }

        /// <summary>
        /// 反应内容
        /// </summary>
        public string FYNR { get; set; }

        ///// <summary>
        ///// 市局领导批示
        ///// </summary>
        //public string SJLDPS { get; set; }

        /// <summary>
        /// 交办意见
        /// </summary>
        public string JBYJ { get; set; }

        /// <summary>
        /// 经办单位
        /// </summary>
        public string JBDW { get; set; }

        /// <summary>
        /// 经办单位签批
        /// </summary>
        public string JBDWQP { get; set; }

        /// <summary>
        /// 办理总结
        /// </summary>
        public string BLJG { get; set; }

        /// <summary>
        /// 投诉反馈意见
        /// </summary>
        public string TSFKYJ { get; set; }

        /// <summary>
        /// 中队长
        /// </summary>
        public string ZDZ { get; set; }

        /// <summary>
        /// 大队长
        /// </summary>
        public string DDZ { get; set; }

        /// <summary>
        /// 副局长
        /// </summary>
        public string FJZ { get; set; }

        /// <summary>
        /// 局长
        /// </summary>
        public string JZ { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string TP { get; set; }
    }
}
