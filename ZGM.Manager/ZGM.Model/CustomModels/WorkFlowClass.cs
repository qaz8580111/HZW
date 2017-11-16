using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.CustomModels
{
    public class WorkFlowClass
    {
        /// <summary>
        /// 工作流编号
        /// </summary>
        public string WFID { get; set; }

        /// <summary>
        /// 工作流详细编号
        /// </summary>
        public string WFDID { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public string WFSID { get; set; }

        /// <summary>
        /// 活动实例编号
        /// </summary>
        public string WFSAID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 工作流名称
        /// </summary>
        public string currentActivityName { get; set; }

        /// <summary>
        /// 具体表名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 表字段
        /// </summary>
        public string GetTableColumns { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string DEALCONTENT { get; set; }

        /// <summary>
        /// 图片路劲
        /// </summary>
        public string WFPIC { get; set; }

        /// <summary>
        /// 内容路劲
        /// </summary>
        public string contentPath { get; set; }

        /// <summary>
        /// 下一步流程名称
        /// </summary>
        public string nextWFDIDNames { get; set; }

        /// <summary>
        /// 下一步流程用户ID
        /// </summary>
        public string NextWFUSERIDS { get; set; }

        /// <summary>
        /// 下一步流程用户名称
        /// </summary>
        public string NextUserNames { get; set; }

        /// <summary>
        /// 下一步流程详细编号
        /// </summary>
        public string NextWFDID { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public string IsSendMsg { get; set; }

        /// <summary>
        /// 流程创建人
        /// </summary>
        public decimal? WFCreateUserID { get; set; }

        /// <summary>
        /// 文件来源 1事件上报 2协同办公
        /// </summary>
        public decimal? FileSource { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public string FileIds { get; set; }

        /// <summary>
        /// 附件内容集合
        /// </summary>
        public List<FileClass> files { get; set; }

        /// <summary>
        /// 任务附件内容集合
        /// </summary>
        public List<FileUploadClass> fileUpload { get; set; }
        /// <summary>
        /// 返回流程状态 1表示没有该流程 2表示该流程不完整
        /// </summary>
        public int WorkFlowState { get; set; }

        /// <summary>
        /// 备注(执法事件存储派遣时限)
        /// </summary>
        public string Remark { get; set; }
    }


    public class Process {
        public string wfdid { get; set; }
    }
}
