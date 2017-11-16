using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class TorecmediasModel
    {
        public int recid { get; set; }
        public int msgid { get; set; }
        public int mediaid { get; set; }
        public string mediatype { get; set; }
        public string medianame { get; set; }
        public string mediausage { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public string mediaurl { get; set; }
        public Nullable<System.DateTime> datainsertdate { get; set; }
        public Nullable<System.DateTime> dataupdatedate { get; set; }
    }
}
