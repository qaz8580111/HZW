using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.GSGGModels
{
    /// <summary>
    /// 公文反馈意见
    /// </summary>
    public class GWFeedBack
    {
        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime Edit_Time { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Dept_Name { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 反馈步骤
        /// </summary>
        public decimal Flow_Prcs { get; set; }
    }
}
