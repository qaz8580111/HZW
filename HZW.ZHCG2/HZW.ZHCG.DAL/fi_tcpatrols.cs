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
    
    public partial class fi_tcpatrols
    {
        public int patrolid { get; set; }
        public string patrolname { get; set; }
        public Nullable<int> patroltypeid { get; set; }
        public string patrolcode { get; set; }
        public string cardid { get; set; }
        public Nullable<System.DateTime> datainsertdate { get; set; }
        public Nullable<System.DateTime> dataupdatedate { get; set; }
        public Nullable<sbyte> is_online { get; set; }
    }
}
