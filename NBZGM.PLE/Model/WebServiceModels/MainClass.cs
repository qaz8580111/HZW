using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 问题大类
    /// </summary>
    [Serializable]
    public class MainClass
    {
        /// <summary>
        /// 问题大类标识
        /// </summary>
        public string mainClassID { get; set; }

        /// <summary>
        /// 问题大类编码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 问题大类名称
        /// </summary>
        public string name { get; set; }
    }
}
