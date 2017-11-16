using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class IllegalBuildingFileModel
    {
        [Key]
        public decimal FileId { get; set; }
        public string FileName { get; set; }
        public string WJId { get; set; }
        //所属违建
        public string SSWJ { get; set; }
        //违建图片类别（1改造前 2改造后）
        public string Type { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
