using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taizhou.PLE.Model.WebServiceModels
{
    public class PhoneViewModel1
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal UserID { get; set; }

        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }


        /// <summary>
        /// 是否立案
        /// </summary>
        public string SFLA { get; set; }


        /// <summary>
        /// 当事人类型
        /// </summary>
        public string DSRLX { get; set; }

        /// <summary>
        /// 当事人基本信息（单位）
        /// </summary>
        public OrgForm OrgForm { get; set; }

        /// <summary>
        /// 当事人基本信息（个人）
        /// </summary>
        public PersonForm PersonForm { get; set; }


        /// <summary>
        /// 住所地
        /// </summary>
        public string ZSD { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 违法行为信息
        /// </summary>
        public IllegalForm IllegalForm { get; set; }
    }

    /// <summary>
    /// 当事人基本情况(单位)
    /// </summary>
    public class OrgForm
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ZZJGDMZBH { get; set; }

        /// <summary>
        /// 法定代表人姓名
        /// </summary>
        public string FDDBRXM { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }
    }

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

    /// <summary>
    /// 违法行为事项表单
    /// </summary>
    public class IllegalForm
    {
        /// <summary>
        /// 违法行为代码标识
        /// </summary>
        public decimal? ID { get; set; }

        /// <summary>
        /// 违法行为代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 违法行为代码名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 违则
        /// </summary>
        public string WeiZe { get; set; }

        /// <summary>
        /// 罚则
        /// </summary>
        public string FaZe { get; set; }

        /// <summary>
        /// 处罚
        /// </summary>
        public string ChuFa { get; set; }

    }
}