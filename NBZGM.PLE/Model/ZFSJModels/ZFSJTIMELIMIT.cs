using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJModels
{
   public class ZFSJTIMELIMITLL
    {
       /// <summary>
       /// 事件来源ID
       /// </summary>
       public string ID { get; set; }
       /// <summary>
       /// 事件流程ID
       /// </summary>
       public decimal ADID { get; set; }
       /// <summary>
       /// 事件限制时间
       /// </summary>
       public decimal TIMELIMIT { get; set; }
    }
}
