using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm107 : WebBaseForm
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 文书材料
        /// </summary>
        public List<WebAttachment> Attachments { get; set; }
    }
}