using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMNotice:OA_NOTICES
    {
        /// <summary>
        /// 文件标识串
        /// </summary>
        public string AttrachsStr { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public decimal? IsRead { get; set; }
    }
}
