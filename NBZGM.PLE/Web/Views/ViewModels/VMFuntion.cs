using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMFuntion
    {
        public decimal FunctionID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> ParentID { get; set; }
        public string URL { get; set; }
        public Nullable<decimal> StatusID { get; set; }
        public string IconPath { get; set; }
        public Nullable<decimal> SeqNO { get; set; }
    }
}