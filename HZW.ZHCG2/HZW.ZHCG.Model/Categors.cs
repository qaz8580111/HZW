using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class Categors
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SeqNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> createdTime { get; set; }

        /// <summary>
        /// 是否上线
        /// </summary>
        public int? isonline { get; set; }

        /// <summary>
        /// 大类名称
        /// </summary>
        public string BigName { get; set; }

        /// <summary>
        /// 大类编号
        /// </summary>
        public int? BigID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int Createuserid { get; set; }
    }
}
