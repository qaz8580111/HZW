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
    
    public partial class TRANSLICENS
    {
        public TRANSLICENS()
        {
            this.TRANSLICENSECARCOMPANIES = new HashSet<TRANSLICENSECARCOMPANy>();
            this.TRANSLICENSECARS = new HashSet<TRANSLICENSECAR>();
            this.TRANSLICENSEROADS = new HashSet<TRANSLICENSEROAD>();
        }
    
        public decimal TRANSLICENSEID { get; set; }
        public Nullable<decimal> CONSTRSITEID { get; set; }
        public Nullable<decimal> DUMPINGSITEID { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public string TRANSLINE { get; set; }
        public Nullable<decimal> ZHATU { get; set; }
        public Nullable<decimal> NIJIANG { get; set; }
        public Nullable<decimal> UNITID { get; set; }
        public Nullable<decimal> ISSYNC { get; set; }
    
        public virtual CONSTRSITE CONSTRSITE { get; set; }
        public virtual DUMPINGSITE DUMPINGSITE { get; set; }
        public virtual ICollection<TRANSLICENSECARCOMPANy> TRANSLICENSECARCOMPANIES { get; set; }
        public virtual ICollection<TRANSLICENSECAR> TRANSLICENSECARS { get; set; }
        public virtual ICollection<TRANSLICENSEROAD> TRANSLICENSEROADS { get; set; }
        public virtual UNIT UNIT { get; set; }
    }
}