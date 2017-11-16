using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
   public class TorecsModel
    {
        public int recid { get; set; }
        public string tasknum { get; set; }
        public Nullable<int> eventsrcid { get; set; }
        public string eventsrcname { get; set; }
        public Nullable<int> eventtypeid { get; set; }
        public string eventtypename { get; set; }
        public Nullable<int> maintypeid { get; set; }
        public string maintypename { get; set; }
        public Nullable<int> subtypeid { get; set; }
        public string subtypename { get; set; }
        public string eventdesc { get; set; }
        public string address { get; set; }
        public Nullable<int> districtid { get; set; }
        public string districtname { get; set; }
        public Nullable<int> streetid { get; set; }
        public string streetname { get; set; }
        public Nullable<int> communityid { get; set; }
        public string communityname { get; set; }
        public Nullable<double> coordinatex { get; set; }
        public Nullable<double> coordinatey { get; set; }
        public Nullable<int> patrolid { get; set; }
        public string patrolname { get; set; }
        public Nullable<int> patrolunitid { get; set; }
        public string patrolunitname { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<System.DateTime> datainsertdate { get; set; }
        public Nullable<System.DateTime> dataupdatedate { get; set; }
        public Nullable<int> deleteflag { get; set; }
        public Nullable<System.DateTime> deletedate { get; set; }
        public Nullable<int> actpropertyid { get; set; }
        public Nullable<int> emergencyflag { get; set; }
        public Nullable<int> istypical { get; set; }
        public string remark { get; set; }


        public List<TorecmediasModel> listMedias { get; set; }
    }
}
