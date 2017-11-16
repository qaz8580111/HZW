using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 手机提交文书数据(json格式)
    /// </summary>
    public class PhoneDoc
    {
        /// <summary>
        /// 文书类型标识
        /// </summary>
        public decimal TypeID { get; set; }

        /// <summary>
        /// 文书内容集合（json格式）
        /// </summary>
        public string DocStr { get; set; }
    }
}
