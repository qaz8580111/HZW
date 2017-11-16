using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    public class UserFunction
    {
        public decimal ID { get; set; }

        public decimal? ParentID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }
    }
}
