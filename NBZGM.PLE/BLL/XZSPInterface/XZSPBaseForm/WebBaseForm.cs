using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Model.XZSPModels;

namespace Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm
{
    public class WebBaseForm
    {
        public string WIID { get; set; }
        /// <summary>
        /// 活动定义标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 活动标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 所属审批事项标识
        /// </summary>
        public string APID { get; set; }




    }
}