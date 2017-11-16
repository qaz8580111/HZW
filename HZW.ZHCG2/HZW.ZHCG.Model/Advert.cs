using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class Advert
    {
        public int ID { get; set; }
        public string IDType { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
        public string AdName { get; set; }
        public string UnitName { get; set; }
        public string UnitPerson { get; set; }
        public string UnitPhone { get; set; }
        public string Producers { get; set; }
        public string Prophone { get; set; }
        public int? State { get; set; }
        public string ExamUnit { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string FileName1 { get; set; }
        public string FileName2 { get; set; }
        public string FileName3 { get; set; }
        public string FilePath1 { get; set; }
        public string FilePath2 { get; set; }
        public string FilePath3 { get; set; }
        public string Grometry { get; set; }
        public double? Volume { get; set; }
        public double? VLong { get; set; }
        public double? VWide { get; set; }
        public double? VHigh { get; set; }
        public string Materials { get; set; }
        public string Curingunit { get; set; }
        public string Superviseunit { get; set; }
        public DateTime? Createtime { get; set; }
        public int? Createuserid { get; set; }
        public int? Status { get; set; }
        public string statusName { get; set; }
        public string Remark { get; set; }
    }
}
