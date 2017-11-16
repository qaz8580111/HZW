using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class DCXWTZSGH
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 违法行为
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 调查询问时间(需在文书表单页面填写)
        /// </summary>
        public DateTime? DCXWSJ { get; set; }

        /// <summary>
        /// 调查询问地点(需在文书表单页面填写)
        /// </summary>
        public string DCXWDD { get; set; }

        /// <summary>
        /// 联系人(需在文书表单页面填写)
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 地址(需在文书表单页面填写)
        /// </summary>
        public string DZ { get; set; }

        /// <summary>
        /// 电话(需在文书表单页面填写)
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 发出日期(需在文书表单页面填写)
        /// </summary>
        public DateTime? FCRQ { get; set; }

        /// <summary>
        /// 当事人收件签章
        /// </summary>
        public string DSRSJQZ { get; set; }

        /// <summary>
        /// 当事人电话
        /// </summary>
        public string DSRDH { get; set; }

        /// <summary>
        /// 当事人收件日期
        /// </summary>
        public DateTime? DSRSJRQ { get; set; }

        /// <summary>
        /// 当事人身份证明
        /// </summary>
        public bool DSRSFZM { get; set; }

        /// <summary>
        /// 委托他人办理的...
        /// </summary>
        public bool WTTRBLD { get; set; }

        /// <summary>
        /// 建设项目批准文件
        /// </summary>
        public bool JSXMPZWJ { get; set; }

        /// <summary>
        /// 建设用地规划许可证
        /// </summary>
        public bool JSYDGHXKZ { get; set; }

        /// <summary>
        /// 建设工程规划许可证
        /// </summary>
        public bool JSGCGHXKZ { get; set; }

        /// <summary>
        /// 建设用地许可证或建设用地批准书
        /// </summary>
        public bool JSYDXKZORJSYDPZS { get; set; }

        /// <summary>
        /// 土地使用证复印件
        /// </summary>
        public bool TDSYZFYJ { get; set; }

        /// <summary>
        /// 竣工复核图原件
        /// </summary>
        public bool JGFHTYJ { get; set; }

        /// <summary>
        /// 房产测绘成果原件
        /// </summary>
        public bool FCCHCGYJ { get; set; }

        /// <summary>
        /// 相关部门意见原件
        /// </summary>
        public bool XGBMYJYJ { get; set; }

        /// <summary>
        /// 有关协议和合同
        /// </summary>
        public bool YGXYHHT { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public bool QT { get; set; }
    }
}
