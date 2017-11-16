using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class TaskDisposeModel
    {
        [Key]
        public string WFSUID { get; set; }
        public string WFDID { get; set; }
        public string WFDName { get; set; }
        public decimal? UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime? dealTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
