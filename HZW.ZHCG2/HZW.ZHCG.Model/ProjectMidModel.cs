using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class ProjectMidModel
    {
        public string id { get; set; }
        public string projectId { get; set; }
        public int flag { get; set; }
    }

    public class ProjectMid
    {
        public string name { get; set; }

        public int count { get; set; }

        public int bjcount { get; set; }

    }
}
