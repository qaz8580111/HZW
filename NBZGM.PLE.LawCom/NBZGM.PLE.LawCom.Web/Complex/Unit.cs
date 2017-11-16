using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Taizhou.PLE.LawCom.Web.Complex
{
    [DataContract]
    public class Unit
    {
        [DataMember]
        [Key]
        public decimal ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AbbrName { get; set; }

        [DataMember]
        public decimal? ParentID { get; set; }

        [DataMember]
        public decimal? SeqNo { get; set; }

        [DataMember]
        public decimal? UnitTypeId { get; set; }
    }
}