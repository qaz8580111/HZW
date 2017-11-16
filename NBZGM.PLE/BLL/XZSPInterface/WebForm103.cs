using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm103 : WebBaseForm
    {
        /// <summary>
        /// 队员意见
        /// </summary>
        public string DYYJ { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public List<string> PhotoList { get; set; }
    }
}