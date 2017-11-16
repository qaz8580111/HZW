using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 应用版本
    /// </summary>
    [Serializable]
    public class AppVersion
    {
        /// <summary>
        /// 版本编号
        /// </summary>
        public int versionCode { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string versionName { get; set; }

        /// <summary>
        /// 版本URL
        /// </summary>
        public string versionURL { get; set; }
    }
}