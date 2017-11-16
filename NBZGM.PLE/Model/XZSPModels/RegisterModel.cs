using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
    /// <summary>
    /// 定义一个基本信息类(包含各项审批的扩展信息)
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 审批项目标识
        /// </summary>
        public decimal APID { get; set; }

        /// <summary>
        /// 审批项目名称
        /// </summary>
        public string ApprovalProjcetName { get; set; }

        /// <summary>
        /// 申请单位
        /// </summary>
        public string ApplicantUnitName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        ///联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 有效期自
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 有效期止
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 执法大队标识
        /// </summary>
        public string ZFDDID { get; set; }

        /// <summary>
        /// 执法中队标识
        /// </summary>
        public string ZFZDID { get; set; }

        /// <summary>
        /// 申请意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        public string AcceptanceTime { get; set; }

        /// <summary>
        /// 执法中队名称
        /// </summary>
        public string ZFZDName { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string XZSPWSBH { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string DTWZ { get; set; }
    }
}
