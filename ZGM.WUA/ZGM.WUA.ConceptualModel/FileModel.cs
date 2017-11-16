using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class FileModel
    {
        [Key]
        public string FileId { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 附件原地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 附件缩小地址
        /// </summary>
        public string FilePathUriSmall { get; set; }
        /// <summary>
        /// 附件压缩地址
        /// </summary>
        public string FilePathUri { get; set; }
        /// <summary>
        /// 附件原图地址
        /// </summary>
        public string FilePathUriOriginal { get; set; }
    }
}
