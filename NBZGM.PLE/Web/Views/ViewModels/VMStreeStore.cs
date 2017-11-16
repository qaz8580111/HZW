using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMStreeStore
    {
        /// <summary>
        /// 沿街店家编号
        /// </summary>
         public decimal  STREESTOREID{ get; set; }
         /// <summary>
         /// 地址
         /// </summary>
         public string SHOPNAME { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
         public string ADDRESS { get; set; }
        /// <summary>
         /// 法人代表
        /// </summary>
         public string SHOPUSERNAME { get; set; }
       
        /// <summary>
         /// 联系方式
        /// </summary>
         public string SHOPPHONE{ get; set; }
         /// <summary>
         /// 从事行业
         /// </summary>
         public string STREESTORETYPEID { get; set; }
        /// <summary>
        /// 门头招牌是否审批
        /// </summary>
         public int  ISMTZP{ get; set; }
        /// <summary>
        /// 有无工商、卫生许可证
        /// </summary>
         public int ISGSWSXKZ { get; set; }
        /// <summary>
        /// 有无排水许可证
        /// </summary>
         public int ISPSXKZ{ get; set; }
        /// <summary>
        /// 有无环评
        /// </summary>
         public int ISHJPL { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
         public string PICTUREURLS { get; set; }
        /// <summary>
        ///地理位置
        /// </summary>
         public string GEOMETRY { get; set; }
    }
}