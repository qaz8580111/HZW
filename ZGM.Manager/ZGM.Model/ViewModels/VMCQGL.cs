using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMCQGL:CQGL_PROJECTS
    {
        /// <summary>
        /// 住宅标识
        /// </summary>
        public decimal HouseId { get; set; }

        /// <summary>
        /// 户主
        /// </summary>
        public string HouseHolder { get; set; }

        /// <summary>
        /// 签协日期
        /// </summary>
        public DateTime? SignTime { get; set; }

        /// <summary>
        /// 状态标识
        /// </summary>
        public decimal StatusId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }

    public class VMCQGLEP : CQGL_PROJECTS
    {
        /// <summary>
        /// 企业标识
        /// </summary>
        public decimal EnterpriseId { get; set; }

        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime? SignTime { get; set; }
    }

    public class VMCQGL_File : CQGL_FILES
    {
        /// <summary>
        /// 文件标识串
        /// </summary>
        public string FileIdStr { get; set; }
    }

    public class VMCQGL_TRANSITIONS : CQGL_TRANSITIONS
    {
        /// <summary>
        /// 文件名字
        /// </summary>
        public string FILENAME { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FILEPATH { get; set; }
    }

    public class VMCQGL_EPMoney : CQGL_ENTERPRISEMONEYS
    {
        /// <summary>
        /// 支付时间
        /// </summary>
        public string PayTime { get; set; }
    }

    //public class VMCQGL_DRAWHOUSE : CQGL_DRAWHOUSE
    //{

    //}

    //public class VMCQGL_CHECKOUT : CQGL_CHECKOUT
    //{
    //    /// <summary>
    //    /// 文件名字
    //    /// </summary>
    //    public string FILENAME { get; set; }
    //}
}
