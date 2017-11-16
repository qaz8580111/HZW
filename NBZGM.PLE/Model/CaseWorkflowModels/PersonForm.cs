using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 当事人基本情况(个人)
    /// </summary>
    public class PersonForm
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public string CSNY { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }
    }
}
