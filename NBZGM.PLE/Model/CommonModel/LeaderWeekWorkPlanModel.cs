using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class LeaderWeekWorkPlanModel
    {
        public decimal PLANID { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public string ONDUTYLEADER_ { get; set; }
        public string ONDUTYDEPT { get; set; }
        public string MODIFYUSERNAME { get; set; }
        public Nullable<System.DateTime> MODIFYTIME { get; set; }
        public string PLANUSERNAME { get; set; }
        public Nullable<System.DateTime> PLANTIME { get; set; }
    }
}
