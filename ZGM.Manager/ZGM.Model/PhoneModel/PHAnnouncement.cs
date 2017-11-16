using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.PhoneModel
{
    public class PHAnnouncement:OA_NOTICES
    {
        /// <summary>
        /// 队员名字
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 队员头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 时间全格式
        /// </summary>
        public string TimeAllStr { get; set; }

        /// <summary>
        /// 时间日期格式
        /// </summary>
        public string TimeDateStr { get; set; }

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

        /// <summary>
        /// 是否成功
        /// </summary>
        public decimal IsSuccess { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public decimal IsRead { get; set; }

        /// <summary>
        /// 阅读状态
        /// </summary>
        public string ReadStr { get; set; }

        /// <summary>
        /// 该模块下的列表数量
        /// </summary>
        public int AllCount { get; set; }

        /// <summary>
        /// 加载时未读条数
        /// </summary>
        public int NoReadCount { get; set; }

        /// <summary>
        /// 被查询名字
        /// </summary>
        public string QueryName { get; set; }
    }

    public class OA_POSTNOTICES : OA_NOTICES
    {
        /// <summary>
        /// 队员标识
        /// </summary>
        public decimal UserId { get; set; }

        /// <summary>
        /// 标题搜索关键字
        /// </summary>
        public string QueryTitle { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 上传文件1
        /// </summary>
        public string FileStr1 { get; set; }

        /// <summary>
        /// 上传文件1类型
        /// </summary>
        public string FileType1 { get; set; }

        /// <summary>
        /// 上传文件2
        /// </summary>
        public string FileStr2 { get; set; }

        /// <summary>
        /// 上传文件2类型
        /// </summary>
        public string FileType2 { get; set; }

        /// <summary>
        /// 上传文件3
        /// </summary>
        public string FileStr3 { get; set; }

        /// <summary>
        /// 上传文件3类型
        /// </summary>
        public string FileType3 { get; set; }
    }
}
