using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJWorkflowModel.Base
{
    /// <summary>
    /// 流程附件表单
    /// </summary>
    public class Attachment
    {
        public string ID { get; set; }

        public int TypeID { get; set; }
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
