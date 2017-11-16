using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class WorkCount
    {
        /// <summary>
        /// 中队名称
        /// </summary>
        public string ZDNAME { get; set; }

        /// <summary>
        /// 执法事件总条数
        /// </summary>
        public int ZFSJCOUNT { get; set; }

        /// <summary>
        /// 一般案件总条数
        /// </summary>
        public int YBAJCOUNT { get; set; }

        /// <summary>
        /// 简易案件总条数
        /// </summary>
        public int JYAJCOUNT { get; set; }

        /// <summary>
        /// 违停案件总条数
        /// </summary>
        public int XZSPCOUNT { get; set; }
    }
}
