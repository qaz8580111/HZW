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
    
    public partial class CARALARM
    {
        public decimal CARID { get; set; }
        public decimal CARALARMTYPEID { get; set; }
        public System.DateTime STARTTIME { get; set; }
        public System.DateTime ENDTIME { get; set; }
        public Nullable<decimal> TRANSLICENSEID { get; set; }
        public string MESSAGE { get; set; }
        public Nullable<decimal> PROCESSTYPEID { get; set; }
        public string PROCESSCOMMENTS { get; set; }
        public Nullable<System.DateTime> PROCESSTIME { get; set; }
        public Nullable<decimal> PROCESSUSERID { get; set; }
    
        public virtual CARALARMTYPE CARALARMTYPE { get; set; }
        public virtual CAR CAR { get; set; }
        public virtual USER USER { get; set; }
    }
}