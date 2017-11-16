using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
    public class AttachmentModel
    {
        /// <summary>
        /// 材料类型标识(1:图片 2:word)
        /// </summary>
        public decimal MaterialTypeID { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 材料原始路径
        /// </summary>
        public string SFilePath { get; set; }

        /// <summary>
        /// 材料目标路径
        /// </summary>
        public string DFilePath { get; set; }
    }
}
