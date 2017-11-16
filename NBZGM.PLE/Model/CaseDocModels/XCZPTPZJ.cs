using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 现场照片(图片)证据
    /// </summary>
    public class XCZPTPZJ
    {
        /// <summary>
        /// 图片 URL
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 拍摄地点
        /// </summary>
        public string PSDD { get; set; }

        /// <summary>
        /// 拍摄内容
        /// </summary>
        public string PSNR { get; set; }

        /// <summary>
        /// 执法人员1
        /// </summary>
        public string ZFRY1 { get; set; }

        /// <summary>
        /// 执法人员2
        /// </summary>
        public string ZFRY2 { get; set; }

        /// <summary>
        /// 拍摄绘制时间
        /// </summary>
        public DateTime? PSHZSJ { get; set; }
    }
}
