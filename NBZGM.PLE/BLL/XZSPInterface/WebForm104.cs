using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm104 : WebBaseForm
    {
        /// <summary>
        /// 中队意见
        /// </summary>
        public string ZDYJ { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public List<string> PhotoList { get; set; }
    }
}