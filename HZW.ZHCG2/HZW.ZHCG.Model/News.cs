using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class News
    {
        /// <summary>
        /// 文章标识
        /// </summary>
        public int articleID { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int createUserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createUserName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> createdTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int? isDelete { get; set; }

        /// <summary>
        /// 小类别名称
        /// </summary>
        public string categorySName { get; set; }

        /// <summary>
        /// 大类别名称
        /// </summary>
        public string categoryBName { get; set; }

        /// <summary>
        /// 小类ID
        /// </summary>
        public int? categoryID { get; set; }

        /// <summary>
        /// 大类ID
        /// </summary>
        public int? categoryid_bid { get; set; }

        /// <summary>
        /// 是否上线
        /// </summary>
        public int? isonline { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        ///图片路径
        /// </summary>
        public string filePath { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string refilePath { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string refileName { get; set; }
        /// <summary>
        /// 备用字段：创建时间
        /// </summary>
        public string reCreatime { get; set; }
    }
}
