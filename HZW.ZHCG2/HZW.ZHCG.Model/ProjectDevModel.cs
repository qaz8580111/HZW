using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class ProjectDevModel
    {
        public string id { get; set; }
        public string msgId { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }

        public Nullable<double> waterPressure { get; set; }
        public Nullable<int> dumpingState { get; set; }
        public Nullable<int> gatewayVoltageState { get; set; }
        public Nullable<int> nodeState { get; set; }
        public Nullable<System.DateTime> formattedCreated { get; set; }


        public Nullable<int> data0 { get; set; }


        public Nullable<double> templ { get; set; }
        public Nullable<double> damp { get; set; }
        public Nullable<double> pm25 { get; set; }
        public Nullable<double> x { get; set; }
        public Nullable<double> y { get; set; }

        public int flag { get; set; }

    }
}
