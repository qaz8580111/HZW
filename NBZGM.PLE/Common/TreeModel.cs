using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common
{
    public class TreeModel
    {
        public string name { get; set; }
        public bool open { get; set; }
        public decimal value { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public List<TreeModel> children { get; set; }
        public bool camera { get; set; }
        public string parameters { get; set; }
    }
}
