using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Taizhou.PLE.LawCom.Web.Complex
{
    [DataContract]
    public class EventLaw
    {
        [DataMember]
        [Key]
        public string EventID { get; set; }

        [DataMember]
        public string EventTitle { get; set; }

        [DataMember]
        public string EventAddress { get; set; }

        [DataMember]
        public string EventSource { get; set; }

        [DataMember]
        public decimal? UnitID { get; set; }

        [DataMember]
        public string SSDD { get; set; }

        [DataMember]
        public string SSZD { get; set; }

        [DataMember]
        public string Geometry { get; set; }

        [DataMember]
        public DateTime? ReportTime { get; set; }

        [DataMember]
        public string ReportPerson { get; set; }
    }
}