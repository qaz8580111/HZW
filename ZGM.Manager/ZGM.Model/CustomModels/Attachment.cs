using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{ /// <summary>
    /// 流程附件表单
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        public string FILEID { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string FILETYPE { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FILENAME { get; set; }
        
        /// <summary>
        /// 下载路径
        /// </summary>
        public string FILEPATH { get; set; }

    }
}
