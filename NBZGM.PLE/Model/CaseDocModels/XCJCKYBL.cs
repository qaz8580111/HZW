using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 现场检查(勘验)笔录
    /// </summary>
    public class XCJCKYBL
    {

        /// <summary>
        /// 开始勘察时间年月日
        /// </summary>
        public string StartTimeYMD { get; set; }

        /// <summary>
        /// 开始勘查时间
        /// </summary>
        public string StartKCSJ { get; set; }

        /// <summary>
        /// 结束勘查时间
        /// </summary>
        public string EndKCSJ { get; set; }

        /// <summary>
        /// 检查（勘验）地点
        /// </summary>
        public string JCKYDD { get; set; }

        /// <summary>
        /// 被检查（勘验）人名称
        /// </summary>
        public string BJCKYRMC { get; set; }

        /// <summary>
        /// 法定代表人（负责人）
        /// </summary>
        public string FDDBRFZR { get; set; }

        /// <summary>
        /// 被检查（勘验）人姓名
        /// </summary>
        public string BJCKYRXM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string SFZHM { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW1 { get; set; }

        /// <summary>
        /// 职务或职业
        /// </summary>
        public string ZWHZY { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string ZZ { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string YB { get; set; }

        /// <summary>
        /// 现场负责人
        /// </summary>
        public string XCFZR { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 本案关系
        /// </summary>
        public string BAGX { get; set; }

        /// <summary>
        /// 其他见证人
        /// </summary>
        public string QTJZR { get; set; }

        /// <summary>
        /// 单位或住址
        /// </summary>
        public string DWHZZ { get; set; }

        /// <summary>
        /// 检查(勘验)人1
        /// </summary>
        public string JCKYR1 { get; set; }
        /// <summary>
        /// 检查(勘验)人2
        /// </summary>
        public string JCKYR2 { get; set; }
        /// <summary>
        /// 执法证编号1
        /// </summary>
        public string ZFZBH1 { get; set; }
        /// <summary>
        /// 执法证编号2
        /// </summary>
        public string ZFZBH2 { get; set; }
        /// <summary>
        /// 检查(勘验)人1的ID
        /// </summary>
        public int? JCKYRID1 { get; set; }
        /// <summary>
        /// 检查(勘验)人二ID
        /// </summary>
        public int? JCKYRID2 { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public string JLR { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW2 { get; set; }

        /// <summary>
        /// 勘测内容正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 被审查类型
        /// </summary>
        public string BJCDXTyle { get; set; }
    }
}
