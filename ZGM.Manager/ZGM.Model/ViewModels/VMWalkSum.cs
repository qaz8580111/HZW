using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMWalkSum:TJ_PERSONWALK_HISTORY
    {
        //队员路程总数
        public decimal? WalkCount { get; set; }
    }
}
