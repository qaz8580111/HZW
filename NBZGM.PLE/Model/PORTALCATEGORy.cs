//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taizhou.PLE.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PORTALCATEGORy
    {
        public PORTALCATEGORy()
        {
            this.ARTICLES = new HashSet<ARTICLE>();
        }
    
        public decimal CATEGORYID { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> SEQNO { get; set; }
        public Nullable<System.DateTime> CREATEDTIME { get; set; }
        public Nullable<decimal> PARENTID { get; set; }
    
        public virtual ICollection<ARTICLE> ARTICLES { get; set; }
    }
}