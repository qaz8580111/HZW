using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.SimpleCaseModels
{
    public class SimpleCase
    {
        /// <summary>
        /// 简易案件标识
        /// </summary>
        public decimal SimpleCaseID { get; set; }

        /// <summary>
        /// 决定书编号
        /// </summary>
        public string JDSBH { get; set; }

        /// <summary>
        /// 当事人类型
        /// </summary>
        public string DSRLX { get; set; }

        /// <summary>
        /// 当事人姓名
        /// </summary>
        public string DSRName { get; set; }

        /// <summary>
        /// 当事人性别
        /// </summary>
        public string DSRGender { get; set; }

        /// <summary>
        /// 当事人身份证号
        /// </summary>
        public string DSRIDNumber { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FZRName { get; set; }

        /// <summary>
        /// 负责人职务
        /// </summary>
        public string FZRPosition { get; set; }

        /// <summary>
        /// 负责人所在地址
        /// </summary>
        public string FZRAddress { get; set; }

        /// <summary>
        /// 案件违法时间
        /// </summary>
        public DateTime? CaseTime { get; set; }

        /// <summary>
        /// 案件违法地点
        /// </summary>
        public string CaseAddress { get; set; }

        /// <summary>
        /// 违法行为标识
        /// </summary>
        public decimal? IllegalItemID { get; set; }

        /// <summary>
        /// 罚款金额
        /// </summary>
        public decimal? FKJE { get; set; }

        /// <summary>
        /// 交款银行
        /// </summary>
        public string JKYH { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 银行户名
        /// </summary>
        public string BankAccountName { get; set; }

        /// <summary>
        /// 执法人姓名
        /// </summary>
        public string ZFRName { get; set; }

        /// <summary>
        /// 执法证号
        /// </summary>
        public string ZFZH { get; set; }

        /// <summary>
        /// 执法时间
        /// </summary>
        public DateTime? ZFSJ { get; set; }

        /// <summary>
        /// GPS定位经度
        /// </summary>
        public decimal? Lon { get; set; }

        /// <summary>
        /// GPS定位纬度
        /// </summary>
        public decimal? Lat { get; set; }

        /// <summary>
        /// 违法行为名称
        /// </summary>
        public string WFXWName { get; set; }

        /// <summary>
        /// 违法行为代码
        /// </summary>
        public string IllegalCode { get; set; }

        /// <summary>
        /// 违则
        /// </summary>
        public string WEIZE { get; set; }

        /// <summary>
        /// 罚则
        /// </summary>
        public string FAZE { get; set; }

        /// <summary>
        /// 地图描点
        /// </summary>
        public string DTMD { get; set; }

        /// <summary>
        /// 创建用户标识
        /// </summary>
        public decimal? UserID { get; set; }

        /// <summary>
        /// 创建用户部门标识
        /// </summary>
        public decimal? UntiID { get; set; }
    }
}
