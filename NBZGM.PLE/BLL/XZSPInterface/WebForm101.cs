using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;

namespace Taizhou.PLE.BLL.XZSPInterface
{
    public class WebForm101 : WebBaseForm
    { /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

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
        /// 申报内容
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        public string AcceptanceTime { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExpandInfoForm101 { get; set; }

        /// <summary>
        /// 承办人标识
        /// </summary>
        public string CBRID { get; set; }

        /// <summary>
        /// 相关材料
        /// </summary>
        //public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// 执法中队名称
        /// </summary>
        public string ZFZDName { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string DTWZ { get; set; }
    }
}