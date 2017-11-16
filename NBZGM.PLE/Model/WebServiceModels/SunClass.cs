using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 问题小类
    /// </summary>
    [Serializable]
    public class SunClass
    {
        /// <summary>
        /// 问题小类标识
        /// </summary>
        public string sunClassID { get; set; }

        /// <summary>
        /// 问题小类编码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 问题小类名称
        /// </summary>
        public string name { get; set; }
    }
}
