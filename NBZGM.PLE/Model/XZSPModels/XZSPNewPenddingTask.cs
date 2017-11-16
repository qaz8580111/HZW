using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
  public  class XZSPNewPenddingTask
    {
      /// <summary>
      /// ID
      /// </summary>
      public string ID { get; set; }
      /// <summary>
      /// 案件编号
      /// </summary>
      public string  AIID { get; set; }
      /// <summary>
      /// 流程编号
      /// </summary>
      public decimal ADID { get; set; }
      /// <summary>
      /// 流程名称
      /// </summary>
      public string ADName { get; set; }
      /// <summary>
      /// 事件标题
      /// </summary>
      public string EventTitle { get; set; }
      /// <summary>
      /// 事件描述
      /// </summary>
      public string EventDescription { get; set; }
      /// <summary>
      /// 创建时间
      /// </summary>
      public DateTime? CreateTime { get; set; }
      /// <summary>
      /// 派遣人
      /// </summary>
      public string PQR { get; set; }
      /// <summary>
      /// 派遣时间
      /// </summary>
      public DateTime? PQSJ { get; set; }
      /// <summary>
      /// 派遣意见
      /// </summary>
      public string PQYJ { get;set; }
      /// <summary>
      /// 状态ID
      /// </summary>
      public decimal STATUSID { get; set; }
    
    }
}
