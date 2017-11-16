using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class EventModel
    {
        public string title { get; set; }

        public DateTime? evtStart { get; set; }

        public DateTime? evtEnd { get; set; }

        public decimal ID { get; set; }

        public decimal? type { get; set; }

        public bool draggable { get; set; }
    }
}
