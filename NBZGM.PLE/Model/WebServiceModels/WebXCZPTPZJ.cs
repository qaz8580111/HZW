using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    public class WebXCZPTPZJ
    {
        /// <summary>
        /// 图片 URL
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 拍摄地点
        /// </summary>
        public string PSDD { get; set; }

        /// <summary>
        /// 拍摄内容
        /// </summary>
        public string PSNR { get; set; }

        /// <summary>
        /// 拍摄绘制时间
        /// </summary>
        public DateTime? PSHZSJ { get; set; }
    }
}
