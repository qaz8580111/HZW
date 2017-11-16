using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMZFSJTJ:TJ_PERSONEVENT_HISTORY
    {
        //时间段内事件上报总数
        public decimal? PCount { get; set; }

        //时间段内事件完结总数
        public decimal? PCCount { get; set; }
    }
}
