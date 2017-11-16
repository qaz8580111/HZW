using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
  public  class CaseCount
    {
      /// <summary>
      /// 编号
      /// </summary>
      public decimal? UNITID { get; set; }
      /// <summary>
      /// 中队名称
      /// </summary>
      public string UNITNAME { get; set; }
      /// <summary>
      /// 一月数量
      /// </summary>
      public decimal? January { get; set; }
      /// <summary>
      /// 二月数量
      /// </summary>
      public decimal? February { get; set; }
      /// <summary>
      /// 三月数量
      /// </summary>
      public decimal? March { get; set; }
      /// <summary>
      /// 四月数量
      /// </summary>
      public decimal? April { get; set; }
      /// <summary>
      /// 五月数量
      /// </summary>
      public decimal? May { get; set; }
      /// <summary>
      /// 六月数量
      /// </summary>
      public decimal? June { get; set; }
      /// <summary>
      /// 七月数量
      /// </summary>
      public decimal? July { get; set; }
      /// <summary>
      /// 八月数量
      /// </summary>
      public decimal? August { get; set; }
      /// <summary>
      /// 九月数量
      /// </summary>
      public decimal? September { get; set; }
      /// <summary>
      /// 十月数量
      /// </summary>
      public decimal? October { get; set; }
      /// <summary>
      /// 十一月数量
      /// </summary>
      public decimal? November { get; set; }
      /// <summary>
      /// 十二月数量
      /// </summary>
      public decimal? December { get; set; }
      /// <summary>
      /// 中队到此月总数
      /// </summary>
      public decimal? Counts { get; set; }

    }
}
