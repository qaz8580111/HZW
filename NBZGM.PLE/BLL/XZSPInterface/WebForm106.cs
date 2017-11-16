using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm106 : WebBaseForm
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 要处理的副大队长标识
        /// </summary>
        public string FDZZID { get; set; }
    }
}