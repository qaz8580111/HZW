using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class UnitModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Nullable<int> UnitTypeID { get; set; }
        public string UnitTypeName { get; set; }
        public Nullable<int> ParentID { get; set; }
    }
}
