using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMSimpleEn:GCGL_SIMPLES
    {
        /// <summary>
        /// 审核人标识
        /// </summary>
        public decimal UserId { get; set; }

        /// <summary>
        /// 审核人标识
        /// </summary>
        public string UserIdStr { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 审核人部门
        /// </summary>
        public decimal UnitId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string ETime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public decimal? Status { get; set; }

        /// <summary>
        /// 文件名字
        /// </summary>
        public string FILENAME { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FILEPATH { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public decimal ISAGREE { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string OPINION { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AUDITTIME { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string ExamineTime { get; set; }

        /// <summary>
        /// 文件列表
        /// </summary>
        public IEnumerable<GCGL_SIMPLEFILES> FILE { get; set; }
    }

    public class SimpleGCListModel
    {
        public string wfsid { get; set; }
        public string wfsname { get; set; }
        public DateTime? STARTTIME { get; set; }
        public DateTime? ENDTIME { get; set; }
        public DateTime? CREATETIME { get; set; }
        public decimal status { get; set; }
        public string wfid { get; set; }
        public string wfname { get; set; }
        public decimal userid { get; set; }
        public string username { get; set; }
        public string wfdid { get; set; }
        public string wfdname { get; set; }
        public string wfsaid { get; set; }
        public string GCNAME { get; set; }
        public DateTime? FINISHTIME { get; set; }
        public string GCNUMBER { get; set; }
        public string SIMPLEGCID { get; set; }
        public decimal nextuserid { get; set; }
        // public string USERNAME { get; set; }
    }
}
