using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm
{
    public class WebAttachment
    {
        public string ID { get; set; }

        public int TypeID { get; set; }

        public string TypeName { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachName { get; set; }

        public string OriginalPath { get; set; }

        /// <summary>
        /// 下载路径
        /// </summary>
        public string Path { get; set; }
    }
}