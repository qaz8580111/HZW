using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ViewModels
{
    public class VMZNBJ:XTGL_ZNBJSJS
    {
        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 开始事件
        /// </summary>
        public string HAPPENTIMEStr { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string ENDTIMEStr { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string DEALTIMEStr { get; set; }
    }

}
