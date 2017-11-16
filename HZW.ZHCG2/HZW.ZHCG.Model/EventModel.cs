using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class EventModel
    {

        public string event_id { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> reporttime { get; set; }
        public string reportperson { get; set; }
        public string source { get; set; }
        public string contact { get; set; }
        public string content { get; set; }
        public string photo1 { get; set; }
        public string photo2 { get; set; }
        public string photo3 { get; set; }
        public string photofile1 { get; set; }
        public string photofile2 { get; set; }
        public string photofile3 { get; set; }
        public string grometry { get; set; }

        public Nullable<int> isexamine { get; set; }
        public Nullable<int> inputperson { get; set; }

        public string inputpersonname { get; set; }

        public Nullable<System.DateTime> inputtime { get; set; }
        public string inputcontent { get; set; }
        public Nullable<int> invalisperson { get; set; }

        public string invalispersonname { get; set; }

        public Nullable<System.DateTime> invalistime { get; set; }
        public string invaliscontent { get; set; }
        public Nullable<int> ispush { get; set; }

        public string yzm { get; set; }
    }
}
