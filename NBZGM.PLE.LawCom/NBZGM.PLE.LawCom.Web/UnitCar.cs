using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Taizhou.PLE.LawCom.Web
{
    [DataContract]
    public class UnitCar
    {
        [DataMember]
        public decimal? UnitID { get; set; }

        [DataMember]
        public decimal? CarID { get; set; }
    }
}