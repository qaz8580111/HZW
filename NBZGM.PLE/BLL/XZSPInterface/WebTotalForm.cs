using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebTotalForm
    {
        /// <summary>
        /// 流程当前处理的表单
        /// </summary>
        public WebBaseForm WebCurrentForm { get; set; }

        public WebForm101 WebForm101 { get; set; }

        public WebForm102 WebForm102 { get; set; }

        public WebForm103 WebForm103 { get; set; }

        public WebForm104 WebForm104 { get; set; }

        public WebForm105 WebForm105 { get; set; }

        public WebForm106 WebForm106 { get; set; }

        public WebForm107 WebForm107 { get; set; }

        public WebForm108 WebForm108 { get; set; }

        public WebFeedBackForm WebFeedBackForm { get; set; }

    }
}