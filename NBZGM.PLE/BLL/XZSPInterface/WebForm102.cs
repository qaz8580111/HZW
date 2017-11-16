using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm102 : WebBaseForm
    { 
        /// <summary>
        /// 派遣队员
        /// </summary>
        public decimal PQDYID { get; set; }

        /// <summary>
        /// 派遣队员2
        /// </summary>
        public decimal PQDYID2 { get; set; }

        /// <summary>
        /// 派遣意见
        /// </summary>
        public string description { get; set; }
    }
}