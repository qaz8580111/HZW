using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMProject:CQGL_PROJECTS
    {
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string ProjectUserName { get; set; }

        /// <summary>
        /// 显示启动时间
        /// </summary>
        public string StartTimeStr { get; set; }

        /// <summary>
        /// 显示完结时间
        /// </summary>
        public string EndTimeStr { get; set; }

        /// <summary>
        /// 文件标识串
        /// </summary>
        public string FileIdStr { get; set; }

    }
}
