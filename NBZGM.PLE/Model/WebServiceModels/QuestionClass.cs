using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 问题大小类
    /// </summary>
    [Serializable]
    public class QuestionClass
    {
        /// <summary>
        /// 大小类标识
        /// </summary>
        public string classID { get; set; }

        /// <summary>
        /// 1：表示大类；2：表示小类
        /// </summary>
        public int classTypeID { get; set; }

        /// <summary>
        /// 所属父类标识
        /// </summary>
        public int parentID { get; set; }

        /// <summary>
        /// 大小类名称
        /// </summary>
        public string className { get; set; }

        /// <summary>
        /// 大小类编号
        /// </summary>
        public int classCode { get; set; }

        /// <summary>
        /// 所属路径
        /// </summary>
        public string path { get; set; }
    }
}
