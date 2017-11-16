using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{
    public class AreaClassModel
    {
        public decimal AREAID { get; set; }
        public string AREANAME { get; set; }
        public string AREADESCRIPTION { get; set; }
        public string GEOMETRY { get; set; }
        public string COLOR { get; set; }
        public Nullable<decimal> AREAOWNERTYPE { get; set; }
        public Nullable<decimal> CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATETIME { get; set; }
        public Nullable<decimal> STATE { get; set; }
        public Nullable<decimal> SQENUM { get; set; }

        public List<AreaRest> list { get; set; }
    }
    public class AreaRest
    {
        public decimal REID { get; set; }
        public Nullable<decimal> RESTID { get; set; }
        public Nullable<decimal> AREAID { get; set; }
    }
}
