using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.XTBGModels
{
    public class ConferenceList : OA_MEETINGS
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public decimal? USERID { get; set; }
        /// <summary>
        /// 是否参加会议
        /// </summary>
        public decimal? ISPARTIN { get; set; }
        /// <summary>
        /// 批准意见
        /// </summary>
        public string APPROVECONTENT { get; set; }
        /// <summary>
        /// 是否已阅
        /// </summary>
        public decimal? ISREAD { get; set; }
        /// <summary>
        /// 是否请假
        /// </summary>
        public decimal? ISLEAVE { get; set; }
        /// <summary>
        /// 是否批准
        /// </summary>
        public decimal? ISAPPROVE { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 请假内容
        /// </summary>
        public string LEAVECONTENT { get; set; }
        /// <summary>
        /// 请假时间
        /// </summary>
        public Nullable<System.DateTime> LEAVETIME { get; set; }
        /// <summary>
        /// 请假事件STRING类型
        /// </summary>
        public string STRINGLEAVETIME { get; set; }
        /// <summary>
        /// 批准时间
        /// </summary>
        public Nullable<System.DateTime> APPROVETIME { get; set; }
        /// <summary>
        ///  参会人员人数
        /// </summary>
        public decimal? USERNUM { get; set; }
        /// <summary>
        /// 参会人员姓名
        /// </summary>
        public string LEAVEAUDITUSERNAME { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CREATEUSERNAME { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string ATTRACHNAME { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string ATTRACHPATH { get; set; }
        /// <summary>
        /// 会议地点名称
        /// </summary>
        public string ADDRESSNAME { get; set; }
        /// <summary>
        /// 上传文件1
        /// </summary>
        public string FileStr1 { get; set; }

        /// <summary>
        /// 上传文件2
        /// </summary>
        public string FileStr2 { get; set; }

        /// <summary>
        /// 上传文件3
        /// </summary>
        public string FileStr3 { get; set; }

        /// <summary>
        /// 上传文件1
        /// </summary>
        public string FileLJStr1 { get; set; }

        /// <summary>
        /// 上传文件2
        /// </summary>
        public string FileLJStr2 { get; set; }

        /// <summary>
        /// 上传文件3
        /// </summary>
        public string FileLJStr3 { get; set; }

    }
}
