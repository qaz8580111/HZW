using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.PerceptionDevice
{
    public class Project01
    {
        public string id { get; set; }

        public string msgId { get; set; }

        public string projectId { get; set; }

        public double? waterPressure { get; set; }

        public int? dumpingState { get; set; }

        public int? gatewayVoltageState { get; set; }

        public int? nodeState { get; set; }

        public int? length { get; set; }

        public Nullable<DateTime> formattedCreated { get; set; }

        public Nullable<DateTime> formattedUpdated { get; set; }
    }
}
