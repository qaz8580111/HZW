using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMWJGL:WJGL_NONBUILDINGS
    {
        /// <summary>
        /// 片区名字
        /// </summary>
        public string ZoneName { get; set; }

        /// <summary>
        /// 违建时间字符
        /// </summary>
        public string WJTIMEStr { get; set; }

        /// <summary>
        /// 拆除时间字符
        /// </summary>
        public string REMOVETIMEStr { get; set; }

        /// <summary>
        /// 整改时间字符
        /// </summary>
        public string RECTIFICATIONTIMEStr { get; set; }

        /// <summary>
        /// 修改前的图片
        /// </summary>
        public string BeforePic { get; set; }

        /// <summary>
        /// 修改后的图片
        /// </summary>
        public string AfterPic { get; set; }
    }
}
