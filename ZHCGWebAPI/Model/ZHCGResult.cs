using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot("Result")]
    public class ZHCGResult
    {
        public string ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public string ResultMemo { get; set; }
    }
}
