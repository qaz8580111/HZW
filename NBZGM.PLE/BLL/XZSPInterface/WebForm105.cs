using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm105 : WebBaseForm
    {
        /// <summary>
        /// 承办人审查意见
        /// </summary>
        public string SCYJ { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 要处理的综合科科长标识
        /// </summary>
        public string CBJGID { get; set; }

        /// <summary>
        /// 相关材料
        /// </summary>
        public List<WebAttachment> Attachments { get; set; }

        /// <summary>
        /// 大队处理意见
        /// </summary>
        public string ZFDYYJ { get; set; }

    }
}