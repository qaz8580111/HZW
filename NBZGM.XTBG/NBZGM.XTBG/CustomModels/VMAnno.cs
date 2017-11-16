using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMAnno
    {
        /// <summary>
        /// 1公告标题  以 index中的文本框name的命名，是公告
        /// </summary>
        public string AnnouncementTitle { get; set; }
        /// <summary>
        /// 2公告范围(部门)
        /// </summary>
        public string UnitNames { get; set; }
        /// <summary>
        /// 2公告范围
        /// </summary>
        public string UnitIDs { get; set; }
        /// <summary>
        ///3 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///4 有效时间
        /// </summary>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// 5文件附件就是附件ID列表
        /// </summary>
        public string AnnoouncementAttachmentIDs { get; set; }//FileAttachmentIDs
        /// <summary>
        /// 6公告内容
        /// </summary>
        public string AnnouncementContent { get; set; }

        /// <summary>
        /// 7公告类型  AnnouncementType
        /// </summary>
        public decimal AnnouncementType { get; set; }
        /// <summary>
        /// 公告部门ID
        /// </summary>
        public string AnnScopeIDs { get; set; }

    }
}