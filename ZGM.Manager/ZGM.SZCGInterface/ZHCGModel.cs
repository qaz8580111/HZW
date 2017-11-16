using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.SZCGInterface
{
   public class ZHCGModel
    {
       public List<ZHCGClass> resul { get; set; }
    }

   public  class ZHCGClass {
       public decimal TASKID { get; set; }
       public string TASKNUM { get; set; }
       public Nullable<System.DateTime> FINDTIME { get; set; }
       public string EVENTSOURCE { get; set; }
       public string EVENTTYPE { get; set; }
       public string MAINTYPE { get; set; }
       public string SUBTYPE { get; set; }
       public string DISTRICTCODE { get; set; }
       public string DISTRICTNAME { get; set; }
       public string STREETCODE { get; set; }
       public string STREETNAME { get; set; }
       public string COMMUNITYCODE { get; set; }
       public string COMMUNITYNAME { get; set; }
       public Nullable<decimal> COORDINATEX { get; set; }
       public Nullable<decimal> COORDINATEY { get; set; }
       public string EVENTADDRESS { get; set; }
       public string EVENTDESCRIPTION { get; set; }
       public string EVENTPOSITIONMAP { get; set; }
       public Nullable<System.DateTime> SENDTIME { get; set; }
       public Nullable<System.DateTime> DEALENDTIME { get; set; }
       public string SENDMEMO { get; set; }
       public Nullable<decimal> DEALTIMELIMIT { get; set; }
       public string DEALUNIT { get; set; }
       public Nullable<System.DateTime> CRATETIME { get; set; }
       public string STATE { get; set; }
       public string DISPOSEID { get; set; }
       public string DISPOSENAME { get; set; }
       public Nullable<System.DateTime> DISPOSEDATE { get; set; }
       public string DISPOSEMEMO { get; set; }
       public Nullable<decimal> LATESTDEALTIMELIMIT { get; set; }
       public Nullable<System.DateTime> LATESTDEALENDTIME { get; set; }
       public Nullable<decimal> WORKLOAD { get; set; }
       public Nullable<decimal> COST { get; set; }
       public Nullable<decimal> ISVALUATION { get; set; }
       public string NOTE { get; set; }
   }

}
