using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{
    public class TreeModel
    {

        public string name { get; set; }
        public bool open { get; set; }
        public string value { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public string smsNumber { get; set; }
        public bool @checked { get; set; }
        public string pId { get; set; }
        public string id { get; set; }
        public string unitId { get; set; }

        public List<TreeModel> children = new List<TreeModel>();

    }
}
