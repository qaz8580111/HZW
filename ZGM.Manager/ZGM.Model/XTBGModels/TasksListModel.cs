using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.XTBGModels
{
    public class TasksListModel
    {
        public string wfsid { get; set; }
        public string wfsname { get; set; }
        public DateTime? createtime { get; set; }
        public decimal status { get; set; }
        public string wfid { get; set; }
        public string wfname { get; set; }
        public decimal userid { get; set; }
        public string username { get; set; }
        public string wfdid { get; set; }
        public string wfdname { get; set; }
        public string wfsaid { get; set; }
        public string TASKTITLE { get; set; }
        public DateTime? FINISHTIME { get; set; }
        public string TASKCONTENT { get; set; }
        public decimal IMPORTANT { get; set; }
        public string TASKID { get; set; }
        public decimal nextuserid { get; set; }
       // public string USERNAME { get; set; }
     }
}
