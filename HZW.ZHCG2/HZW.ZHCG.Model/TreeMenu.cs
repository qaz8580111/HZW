using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class TreeMenu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ParentID { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        public bool leaf { get; set; }
        public string icon { get; set; }
        public List<TreeMenu> children { get; set; }
    }
}
