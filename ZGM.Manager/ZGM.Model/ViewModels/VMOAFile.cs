using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.ViewModels
{
    public class VMOAFile:OA_FILES
    {
        /// <summary>
        /// 接收人标识
        /// </summary>
        public decimal ReciveUserId { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReciveUserIdStr { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReciveUserName { get; set; }

        /// <summary>
        /// 创建人人
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public decimal? IsRead { get; set; }

        /// <summary>
        /// 是否反馈
        /// </summary>
        public decimal? IsResponse { get; set; }

        /// <summary>
        /// 是否办结
        /// </summary>
        public decimal? IsFinish { get; set; }

        /// <summary>
        /// 反馈意见
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 文件标识串
        /// </summary>
        public string AttrachsStr { get; set; }

        /// <summary>
        /// 下步转发文件
        /// </summary>
        public decimal NextFileId { get; set; }

        /// <summary>
        /// 下步转发接收人
        /// </summary>
        public string NextUserName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public decimal? Status { get; set; }
    }
}
