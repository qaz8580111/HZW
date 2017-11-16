using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{
    public class EventModel
    {
        public string title { get; set; }

        public DateTime? evtStart { get; set; }

        public DateTime? evtEnd { get; set; }

        public decimal ID { get; set; }

        public string type { get; set; }

        public bool draggable { get; set; }
    }
}
