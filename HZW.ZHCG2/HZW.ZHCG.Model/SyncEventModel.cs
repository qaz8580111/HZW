using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class SyncEventModel
    {
        public int mapelementcategoryid { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string avatar { get; set; }
        public Nullable<int> regionid { get; set; }
        public Nullable<int> unitid { get; set; }
        public Nullable<int> mapelementbiztypeid { get; set; }
        public Nullable<int> mapelementdevicetypeid { get; set; }
        public string staticproperties { get; set; }
        public string dynamicproperties { get; set; }
        public Nullable<int> isonline { get; set; }
        public Nullable<int> mapelementstatusid { get; set; }
        public string reservedfield1 { get; set; }
        public string reservedfield2 { get; set; }
        public string reservedfield3 { get; set; }
        public string reservedfield4 { get; set; }
        public string reservedfield5 { get; set; }
        public string reservedfield6 { get; set; }
        public Nullable<int> reservedfield7 { get; set; }
        public Nullable<int> reservedfield8 { get; set; }
        public Nullable<System.DateTime> reservedfield9 { get; set; }
        public Nullable<System.DateTime> reservedfield10 { get; set; }
        public Nullable<System.DateTime> createdtime { get; set; }

        public Nullable<decimal> x { get; set; }
        public Nullable<decimal> y { get; set; }

    }
}
