using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class fi_tchumansleft
    {
        public int humanid { get; set; }
        public string humanname { get; set; }
        public string humancode { get; set; }
        public Nullable<int> unitid { get; set; }
        public Nullable<int> regionid { get; set; }
        public Nullable<int> regiontype { get; set; }
        public string telmobile { get; set; }
        public string photourl { get; set; }
        public Nullable<int> validflag { get; set; }
        public Nullable<int> deleteflag { get; set; }
        public Nullable<System.DateTime> deletedate { get; set; }
        public Nullable<int> patrolflag { get; set; }
        public Nullable<System.DateTime> datainsertdate { get; set; }
        public Nullable<System.DateTime> dataupdatedate { get; set; }
        public Nullable<int> displayorder { get; set; }
        public Nullable<int> sex { get; set; }

        public int logid { get; set; }
        public Nullable<int> patrolid { get; set; }
        public Nullable<int> patrolstateid { get; set; }
        public Nullable<System.DateTime> updatetime { get; set; }

        public Nullable<double> coordinatex { get; set; }

        public Nullable<double> coordinatey { get; set; }
    }
}
