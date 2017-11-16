using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    /// <summary>
    /// 添加文书按钮实体类
    /// </summary>
    public class DocButtonInfo
    {
        /// <summary>
        /// 文书定义标识
        /// </summary>
        public decimal DDID { get; set; }

        /// <summary>
        /// 文书定义名称
        /// </summary>
        public string DDName { get; set; }

        /// <summary>
        /// 是否为唯一文书
        /// </summary>
        public decimal? IsUnique { get; set; }

        /// <summary>
        /// 是否为必须文书
        /// </summary>
        public decimal? IsRequired { get; set; }

        /// <summary>
        /// 已添加该文书的数量
        /// </summary>
        public string Count { get; set; }
    }
}
