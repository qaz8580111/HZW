using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 图片
    /// </summary>
    [Serializable]
    public class Picture
    {
        /// <summary>
        /// 图片1
        /// </summary>
        public byte[] picture1 { get; set; }

        /// <summary>
        /// 图片2
        /// </summary>
        public byte[] picture2 { get; set; }

        /// <summary>
        /// 图片3
        /// </summary>
        public byte[] picture3 { get; set; }
    }
}
