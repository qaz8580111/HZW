using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class FMWPCLJL
    {
        /// <summary>
        /// 处理机关名称或印章
        /// </summary>
        public string CLJGMCHYZ { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? CLSJ { get; set; }

        /// <summary>
        /// 处理地点
        /// </summary>
        public string CLDD { get; set; }

        /// <summary>
        /// 处理物品执行人
        /// </summary>
        public string CLWPZXR { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string JLR { get; set; }

        /// <summary>
        /// 见证人或监销人
        /// </summary>
        public string JZRHJXR { get; set; }

        /// <summary>
        /// 处理物品原持有人
        /// </summary>
        public string CLWPYCYR { get; set; }

        /// <summary>
        /// 处理物品名称、数量和规格
        /// </summary>
        public string CLWPMCSLJGG { get; set; }

        /// <summary>
        /// 处理物品的原行政处罚决定书及文号
        /// </summary>
        public string CLWPDYXZCFJDSJWH { get; set; }

        /// <summary>
        /// 处理理由及依据
        /// </summary>
        public string CLLYJYJ { get; set; }

        /// <summary>
        /// 处理方式及处理结果
        /// </summary>
        public string CLFSJCLJG { get; set; }

        ///// <summary>
        ///// 执行人员签名
        ///// </summary>
        //public string ZXRYQM { get; set; }

        ///// <summary>
        ///// 执行人员签名日期
        ///// </summary>
        //public DateTime ZXRYQMRQ { get; set; }

        ///// <summary>
        ///// 见证人或监销人员签名
        ///// </summary>
        //public string JZRHJXRQM { get; set; }

        ///// <summary>
        ///// 见证人或监销人员签名日期
        ///// </summary>
        //public DateTime JZRHJXRQMRQ { get; set; }

        ///// <summary>
        ///// 批准机关负责人签名
        ///// </summary>
        //public string PZJGFZRQM { get; set; }

        ///// <summary>
        ///// 批准机关负责人签名日期
        ///// </summary>
        //public DateTime PZJGFZRQMRQ { get; set; }
    }
}
