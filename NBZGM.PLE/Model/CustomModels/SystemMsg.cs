using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    /// <summary>
    /// 消息中心左栏系统提示信息
    /// </summary>
    public class SystemMsg
    {
        //系统信息
        public decimal xtxx { get; set; }
        //信息数量
        public decimal xx { get; set; }
        //公告数量
        public decimal gg { get; set; }
        //提示数量
        public decimal ts { get; set; }
        //通知数量
        public decimal tz { get; set; }
    }
}
