using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Taizhou.PLE.LawCom.Web.Complex
{
    [DataContract]
    public class Person
    {
        [DataMember]
        [Key]
        public decimal UserID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public decimal? UnitID { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public string SmsNumber { get; set; }

        [DataMember]
        public DateTime? PositionTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public double? Lon { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public double? Lat { get; set; }

    }
}