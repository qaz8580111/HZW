using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.CustomModels
{
    /// <summary>
    /// 公共图片类
    /// </summary>
    public class FileClass
    {
        /// <summary>
        /// 原图路径
        /// </summary>
        public string OriginalPath { get; set; }

        /// <summary>
        /// 原图文件名称
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// 原图文件类型
        /// </summary>
        public string OriginalType { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string FilesPath { get; set; }

        /// <summary>
        /// 缩略图文件名称
        /// </summary>
        public string FilesName { get; set; }

        /// <summary>
        /// 缩略图文件类型
        /// </summary>
        public string FilesType { get; set; }

        /// <summary>
        /// 小图路径
        /// </summary>
        public string SmallPath { get; set; }

        /// <summary>
        /// 小图名称
        /// </summary>
        public string SmallName { get; set; }

        /// <summary>
        /// 小图类型
        /// </summary>
        public string SmallType { get; set; }
    }

    /// <summary>
    /// 公共文件类
    /// </summary>
    public class FileUploadClass
    {
        /// <summary>
        /// 原文件路径
        /// </summary>
        public string OriginalPath { get; set; }
        /// <summary>
        /// 原文件名称
        /// </summary>
        public string OriginalName { get; set; }
        /// <summary>
        /// 原文件类型
        /// </summary>
        public string OriginalType { get; set; }
    }
}
