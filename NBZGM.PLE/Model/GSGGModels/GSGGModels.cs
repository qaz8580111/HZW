using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.GSGGModels
{
  public  class GSGGPendModels
    {
      /// <summary>
      /// 用户
      /// </summary>
      public string User_Name { get; set; }
      /// <summary>
      /// 编号
      /// </summary>
      public string NotifyID { get; set; }
      /// <summary>
      /// 标题
      /// </summary>

      public string SubJect { get; set; }
      /// <summary>
      /// 提交时间
      /// </summary>
      public DateTime? SendTime { get; set; }
      /// <summary>
      /// 内容
      /// </summary>
      public string Content { get; set; }
      /// <summary>
      /// 发布部门
      /// </summary>
      public string User_Priv_Name { get; set; }
    }
}
