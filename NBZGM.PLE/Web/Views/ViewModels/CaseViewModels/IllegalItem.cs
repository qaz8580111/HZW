using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{   
    /// <summary>
    /// 违法行为对象
    /// </summary>
    public class IllegalItem
    {
        public string IllegalCode { get; set; }
        public decimal ssdl { get; set; }
        public decimal ssxl { get; set; }
        public decimal sszl { get; set; }
        public string IllegalItemName { get; set; }
        public string wz { get; set; }
        public string fz { get; set; }
        public string cf { get; set; }
        //public decimal IllegalItemID { get; set; }
    }
}