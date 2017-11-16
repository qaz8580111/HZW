using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Taizhou.PLE.LawCom.Web.Complex
{
    [DataContract]
    public class Car
    {
        [DataMember]
        [Key]
        public decimal ID { get; set; }

        [DataMember]
        public string CarNumber { get; set; }

        [DataMember]
        public decimal? UnitID { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public double? X { get; set; }

        [DataMember]
        public double? Y { get; set; }

        [DataMember]
        public DateTime? PositionDateTime { get; set; }
    }
}