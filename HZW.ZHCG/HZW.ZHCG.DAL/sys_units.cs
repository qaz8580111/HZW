//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HZW.ZHCG.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class sys_units
    {
        public sys_units()
        {
            this.sys_users = new HashSet<sys_users>();
        }
    
        public int UNITID { get; set; }
        public string UNITNAME { get; set; }
        public string PATH { get; set; }
        public Nullable<int> PARENTID { get; set; }
        public Nullable<int> UNITTYPEID { get; set; }
        public Nullable<int> STATUSID { get; set; }
        public string DESCRIPTION { get; set; }
        public string ABBREVIATION { get; set; }
        public Nullable<int> SEQNUM { get; set; }
    
        public virtual sys_unittypes sys_unittypes { get; set; }
        public virtual ICollection<sys_users> sys_users { get; set; }
    }
}
