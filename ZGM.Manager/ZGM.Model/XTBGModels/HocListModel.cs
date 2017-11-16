using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.XTBGModels
{
   public class HocListModel
    {
       /// <summary>
       /// 会议标题
       /// </summary>
       public string MEETINGTITLE { get; set; }
       /// <summary>
       /// 会议开始时间
       /// </summary>
       public DateTime? STIME { get; set; }
       /// <summary>
       /// 会议结束时间
       /// </summary>
       public DateTime? ETIME { get; set; }
       /// <summary>
       /// 会议创建人姓名
       /// </summary>
       public string USERNAME { get; set; }
       /// <summary>
       /// 地址名称
       /// </summary>
       public string ADDRESSNAME { get; set; }
       /// <summary>
       /// 会议地址ID
       /// </summary>
       public decimal MeetingAddressesid { get; set; }
    }
}
